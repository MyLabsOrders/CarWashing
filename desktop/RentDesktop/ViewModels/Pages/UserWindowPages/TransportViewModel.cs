using ReactiveUI;
using RentDesktop.Infrastructure.Services.DB;
using RentDesktop.Models;
using RentDesktop.Models.Communication;
using RentDesktop.ViewModels.Base;
using RentDesktop.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace RentDesktop.ViewModels.Pages.UserWindowPages
{
    public class TransportViewModel : BaseViewModel
    {
        public TransportViewModel(ObservableCollection<TransportRent> cart)
        {
            ProductAddToTheCartCommand = ReactiveCommand.Create<Transport>(ToTheCartAddProduct);
            CartOpenCommand = ReactiveCommand.Create(CartOpen);
            ProductSelectCommand = ReactiveCommand.Create<Transport>(ProductSelect);

            Transports = ProductsGetFromDatabase();
            _cartWithProducts = cart;
        }

        #region Private Methods

        private static ObservableCollection<Transport> ProductsGetFromDatabase()
        {
            try
            {
                System.Collections.Generic.List<Transport> transport = ShopService.GetTransports();
                return new ObservableCollection<Transport>(transport);
            }
            catch (Exception ex)
            {
                string message = "Не удалось получить коллекцию доступных товаров.";
#if DEBUG
                message += $" Причина: {ex.Message}";
#endif
                QuickMessage.Error(message).ShowDialog(typeof(UserWindow));
                return new ObservableCollection<Transport>();
            }
        }

        private void ToTheCartAddProduct(Transport transport)
        {
            AddingToTheCartTheProduct?.Invoke(transport);

            TransportRent? existingCartItem = _cartWithProducts.FirstOrDefault(t => t.Transport.ID == transport.ID);
            int days = existingCartItem is null ? 1 : existingCartItem.Days;

            var transportRent = new TransportRent(transport.Copy(), days);
            _cartWithProducts.Add(transportRent);

            SelectedTransport = null;
        }

        private void CartOpen()
        {
            OpeningTheCartPage?.Invoke();
        }

        private void ProductSelect(Transport transport)
        {
            SelectedTransport = transport;
        }

        #endregion

        #region Properties

        public ObservableCollection<Transport> Transports { get; }

        private string _selectedProductName = string.Empty;
        public string SelectedTransportName
        {
            get => $"Название: {_selectedProductName}";
            private set => this.RaiseAndSetIfChanged(ref _selectedProductName, value);
        }

        private string _selectedProductCompany = string.Empty;
        public string SelectedTransportCompany
        {
            get => $"Компания: {_selectedProductCompany}";
            private set => this.RaiseAndSetIfChanged(ref _selectedProductCompany, value);
        }

        private Transport? _selectedProduct = null;
        public Transport? SelectedTransport
        {
            get => _selectedProduct;
            private set
            {
                _ = this.RaiseAndSetIfChanged(ref _selectedProduct, value);

                IsTransportSelected = value is not null;
                SelectedTransportName = value is not null ? value.Name : string.Empty;
                SelectedTransportCompany = value is not null ? value.Company : string.Empty;
                SelectedTransportPrice = value is not null ? value.Price.ToString() : string.Empty;
            }
        }

        private bool _trueIsProductSelected = false;
        public bool IsTransportSelected
        {
            get => _trueIsProductSelected;
            private set => this.RaiseAndSetIfChanged(ref _trueIsProductSelected, value);
        }

        private string _selectedProductPrice = string.Empty;
        public string SelectedTransportPrice
        {
            get => $"Цена: {_selectedProductPrice}";
            private set => this.RaiseAndSetIfChanged(ref _selectedProductPrice, value);
        }

        #endregion

        #region Commands

        public ReactiveCommand<Transport, Unit> ProductSelectCommand { get; }
        public ReactiveCommand<Transport, Unit> ProductAddToTheCartCommand { get; }
        public ReactiveCommand<Unit, Unit> CartOpenCommand { get; }

        #endregion

        #region Events

        public delegate void AddingToTheCartTheProductHandler(Transport transport);
        public event AddingToTheCartTheProductHandler? AddingToTheCartTheProduct;

        public delegate void OpeningTheCartPageHandler();
        public event OpeningTheCartPageHandler? OpeningTheCartPage;

        #endregion

        #region Private Fields

        private readonly ObservableCollection<TransportRent> _cartWithProducts;

        #endregion
    }
}
