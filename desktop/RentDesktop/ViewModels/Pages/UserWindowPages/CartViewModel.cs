using Avalonia.Controls;
using ReactiveUI;
using RentDesktop.Infrastructure.Helpers;
using RentDesktop.Infrastructure.Exceptions;
using RentDesktop.Infrastructure.Services.DatabaseServices;
using RentDesktop.Models;
using RentDesktop.Models.Messaging;
using RentDesktop.Models.Informing;
using RentDesktop.ViewModels.Base;
using RentDesktop.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;

namespace RentDesktop.ViewModels.Pages.UserWindowPages
{
    public class CartViewModel : BaseViewModel
    {
        public CartViewModel(IUser user, ObservableCollection<OrderModel> ordersCollection)
        {
            _user = user;
            _ordersCollection = ordersCollection;

            PaymentMethods = PaymentMethodsSupportedCollection();
            Cart = new ObservableCollection<ProductRentModel>();

            Cart.CollectionChanged += (s, e) =>
            {
                PriceCalculation();
                IsCartEmptyCheck();
            };

            UpdateUserInfo();

            PlacingOfTheOrderOpenCommand = ReactiveCommand.Create(OrderPlacingPageOpen);
            CartRestoreCommand = ReactiveCommand.Create(CartEmpty);
            CartRemoveItemCommand = ReactiveCommand.Create<ProductRentModel>(CartRm);
            ProductSelectCommand = ReactiveCommand.Create<ProductRentModel>(ProductSelect);
            PriceRefreshCommand = ReactiveCommand.Create<NumericUpDownValueChangedEventArgs>(PriceRefresh);
            InfoOnRelatedProductsRefreshCommand = ReactiveCommand.Create<NumericUpDownValueChangedEventArgs>(ProductsRelationsCheck);

            TheCartOpenCommand = ReactiveCommand.Create(CartPageOpen);
            ThePaymentOpenCommand = ReactiveCommand.Create(OrderPaymentPageOpen);
            ThePaymentCloseCommand = ReactiveCommand.Create(PaymentClose);
            DoOrderPaymentCommand = ReactiveCommand.Create(OrderMakePay);
            SaveTheReceiptCommand = ReactiveCommand.Create(DownloadCheque);
            SaveTheInvoiceCommand = ReactiveCommand.Create(DownloadInvoice);
        }

        #region Private Methods

        private void ProductSelect(ProductRentModel transportRent)
        {
            SelectedTransportRent = transportRent;
        }

        private void IsCartEmptyCheck()
        {
            IsCartNotEmpty = Cart.Count != 0;
        }

        private void PriceRefresh(NumericUpDownValueChangedEventArgs e)
        {
            if (SelectedTransportRent != null)
                SelectedTransportRent.Days = (int)e.NewValue;

            PriceCalculation();
        }

        private void CartPageOpen()
        {
            PagesClose();
            IsCartPageVisible = true;
        }

        private void OrderPlacingPageOpen()
        {
            PagesClose();
            IsOrderPlacingPageVisible = true;
        }

        private void ProductsRelationsCheck(NumericUpDownValueChangedEventArgs e)
        {
            if (SelectedTransportRent is null)
                return;

            System.Collections.Generic.IEnumerable<ProductRentModel> relatedProducts = Cart.Where(t => t.Transport.ID == SelectedTransportRent.Transport.ID);

            foreach (ProductRentModel? i in relatedProducts)
            {
                i.Days = (int)e.NewValue;
            }

            PriceCalculation();
        }

        private void PriceCalculation()
        {
            TotalPrice = Cart.Sum(t => t.TotalPrice);
        }

        private void PagesClose()
        {
            IsCartPageVisible = false;
            IsOrderPlacingPageVisible = false;
            IsOrderPaymentPageVisible = false;
        }

        private void OrderPaymentPageOpen()
        {
            PagesClose();
            IsOrderPaymentPageVisible = true;
        }

        private async void DownloadCheque()
        {
            try
            {
                using MemoryStream cheque = FileDownloadService.DownloadCheque(_ordersCollection[^1]);
                await AsyncSaveFile(cheque);

                MsgBox.InfoMsg("Чек успешно загружен.").Dialog(typeof(UserWindow));
            }
            catch (Exception ex)
            {
                string message = "Не удалось загрузить чек.";
#if DEBUG
                message += $" Причина: {ex.Message}";
#endif
                MsgBox.ErrorMsg(message).Dialog(typeof(UserWindow));
            }
        }

        private void CartEmpty()
        {
            Cart.Clear();
            SelectedTransportRent = null;
        }

        private void CartRm(ProductRentModel transportRent)
        {
            if (transportRent == SelectedTransportRent)
                SelectedTransportRent = null;

            Cart.Remove(transportRent);
        }

        private void PaymentClose()
        {
            if (IsOrderPaidFor)
                OpeningTheOrders?.Invoke();

            ResetUserPaymentSteps();
        }

        private static async Task AsyncSaveFile(MemoryStream pdfStream)
        {
            if (WindowSearcher.User() is not Window userWindow)
                throw new FilePathNotSpecifiedException();

            SaveFileDialog dialog = DialogHelper.SaveFile();

            string? path = await dialog.ShowAsync(userWindow);

            if (string.IsNullOrEmpty(path))
                throw new FilePathNotSpecifiedException();

            using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write);
            pdfStream.WriteTo(fileStream);
        }

        private void OrderMakePay()
        {
            if (!UserCashService.CanPayOrder(Cart, _user))
            {
                MsgBox.ErrorMsg("У вас не хватает средств для оплаты.").Dialog(typeof(UserWindow));
                return;
            }

            OrderModel order;

            try
            {
                order = OrdersService.CreateOrder(Cart, _user);
            }
            catch (Exception ex)
            {
                string message = "Не удалось оформить заказ.";
#if DEBUG
                message += $" Причина: {ex.Message}";
#endif
                MsgBox.ErrorMsg(message).Dialog(typeof(UserWindow));
                return;
            }

            _ordersCollection.Add(order);
            IsOrderPaidFor = true;

            CartEmpty();
        }

        private async void DownloadInvoice()
        {
            try
            {
                using MemoryStream invoice = FileDownloadService.DownloadInvoice(_ordersCollection[^1]);
                await AsyncSaveFile(invoice);

                MsgBox.InfoMsg("Ведомость успешно загружена.").Dialog(typeof(UserWindow));
            }
            catch (Exception ex)
            {
                string message = "Не удалось загрузить ведомость.";
#if DEBUG
                message += $" Причина: {ex.Message}";
#endif
                MsgBox.ErrorMsg(message).Dialog(typeof(UserWindow));
            }
        }

        private static ObservableCollection<string> PaymentMethodsSupportedCollection()
        {
            return new ObservableCollection<string>()
            {
                "Онлайн оплата"
            };
        }

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> ThePaymentCloseCommand { get; }
        public ReactiveCommand<Unit, Unit> DoOrderPaymentCommand { get; }

        public ReactiveCommand<Unit, Unit> TheCartOpenCommand { get; }
        public ReactiveCommand<Unit, Unit> TheCartNotOpenCommand { get; }
        public ReactiveCommand<Unit, Unit> ThePaymentOpenCommand { get; }
        public ReactiveCommand<NumericUpDownValueChangedEventArgs, Unit> PriceRefreshCommand { get; }

        public ReactiveCommand<Unit, Unit> ThePaymentNotCloseCommand { get; }

        public ReactiveCommand<Unit, Unit> SaveTheChequeCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveTheReceiptCommand { get; }
        public ReactiveCommand<Unit, Unit> SaveTheInvoiceCommand { get; }

        public ReactiveCommand<Unit, Unit> SaveTheSummaryCommand { get; }

        public ReactiveCommand<Unit, Unit> PlacingOfTheOrderOpenCommand { get; }

        public ReactiveCommand<Unit, Unit> CartRestoreCommand { get; }
        public ReactiveCommand<ProductRentModel, Unit> CartRemoveWithoutSavingItemCommand { get; }

        public ReactiveCommand<ProductRentModel, Unit> ProductSelectCommand { get; }
        public ReactiveCommand<NumericUpDownValueChangedEventArgs, Unit> InfoOnRelatedProductsRefreshCommand { get; }

        public ReactiveCommand<Unit, Unit> CartRestoreWithoutSavingCommand { get; }
        public ReactiveCommand<ProductRentModel, Unit> CartRemoveItemCommand { get; }

        #endregion

        #region Properties

        #region Order Placing Subpage

        public ObservableCollection<string> PaymentMethods { get; }

        private string _theUserName = string.Empty;
        public string UserName
        {
            get => _theUserName;
            private set => this.RaiseAndSetIfChanged(ref _theUserName, value);
        }

        private string _theUserPhoneNumber = string.Empty;
        public string UserPhoneNumber
        {
            get => _theUserPhoneNumber;
            private set => this.RaiseAndSetIfChanged(ref _theUserPhoneNumber, value);
        }

        private int _thePaymentMethodIndex = 0;
        public int PaymentMethodIndex
        {
            get => _thePaymentMethodIndex;
            set => this.RaiseAndSetIfChanged(ref _thePaymentMethodIndex, value);
        }

        #endregion

        #region Cart Subpage

        public ObservableCollection<ProductRentModel> Cart { get; }

        private ProductRentModel? _theSelectedTransportRent = null;
        public ProductRentModel? SelectedTransportRent
        {
            get => _theSelectedTransportRent;
            private set
            {
                _ = this.RaiseAndSetIfChanged(ref _theSelectedTransportRent, value);

                IsTransportRentSelected = value is not null;
                SelectedTransportName = value is not null ? value.Transport.Name : string.Empty;
                SelectedTransportPrice = value is not null ? value.Transport.Price.ToString() : string.Empty;
            }
        }

        private string _theSelectedTransportName = string.Empty;
        public string SelectedTransportName
        {
            get => $"Название: {_theSelectedTransportName}";
            private set => this.RaiseAndSetIfChanged(ref _theSelectedTransportName, value);
        }

        private string _theSelectedTransportPrice = string.Empty;
        public string SelectedTransportPrice
        {
            get => $"Цена: {_theSelectedTransportPrice}";
            private set => this.RaiseAndSetIfChanged(ref _theSelectedTransportPrice, value);
        }

        private bool _theIsTransportRentSelected = false;
        public bool IsTransportRentSelected
        {
            get => _theIsTransportRentSelected;
            private set => this.RaiseAndSetIfChanged(ref _theIsTransportRentSelected, value);
        }

        private double _theTotalPrice = 0;
        public double TotalPrice
        {
            get => _theTotalPrice;
            private set => this.RaiseAndSetIfChanged(ref _theTotalPrice, value);
        }

        private bool _trueIsCartNotEmpty = false;
        public bool IsCartNotEmpty
        {
            get => _trueIsCartNotEmpty;
            private set => this.RaiseAndSetIfChanged(ref _trueIsCartNotEmpty, value);
        }

        private bool _trueIsCartPageVisible = true;
        public bool IsCartPageVisible
        {
            get => _trueIsCartPageVisible;
            private set => this.RaiseAndSetIfChanged(ref _trueIsCartPageVisible, value);
        }

        private bool _trueIsOrderPlacingPageVisible = false;
        public bool IsOrderPlacingPageVisible
        {
            get => _trueIsOrderPlacingPageVisible;
            private set => this.RaiseAndSetIfChanged(ref _trueIsOrderPlacingPageVisible, value);
        }

        private bool _trueIsOrderPaymentPageVisible = false;
        public bool IsOrderPaymentPageVisible
        {
            get => _trueIsOrderPaymentPageVisible;
            private set => this.RaiseAndSetIfChanged(ref _trueIsOrderPaymentPageVisible, value);
        }

        #endregion

        #region Order Payment Subpage

        private bool _trueIsOrderPaidFor = false;
        public bool IsOrderPaidFor
        {
            get => _trueIsOrderPaidFor;
            private set => this.RaiseAndSetIfChanged(ref _trueIsOrderPaidFor, value);
        }

        #endregion

        #endregion

        #region Public Methods

        public void UpdateUserInfo()
        {
            UserName = $"{_user.Name} {_user.Surname} {_user.Patronymic}";
            UserPhoneNumber = _user.PhoneNumber;
        }

        public void ResetUserPaymentSteps()
        {
            CartPageOpen();
            IsOrderPaidFor = false;
        }

        #endregion

        #region Private Fields

        private readonly IUser _user;
        private readonly ObservableCollection<OrderModel> _ordersCollection;

        #endregion

        #region Events

        public delegate void OpeningTheOrdersHandler();
        public event OpeningTheOrdersHandler? OpeningTheOrders;

        #endregion
    }
}
