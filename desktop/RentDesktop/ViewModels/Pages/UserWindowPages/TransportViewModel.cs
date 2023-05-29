using ReactiveUI;
using RentDesktop.Infrastructure.Services.DatabaseServices;
using RentDesktop.Models;
using RentDesktop.Models.Messaging;
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
        public TransportViewModel(ObservableCollection<ProductRentModel> cart)
        {
            ProductAddToTheCartCommand = ReactiveCommand.Create<ProductModel>(ToTheCartAddProduct);
            CartOpenCommand = ReactiveCommand.Create(CartOpen);
            ProductSelectCommand = ReactiveCommand.Create<ProductModel>(ProductSelect);

            Transports = ProductsGetFromDatabase();
            _cartWithProducts = cart;
        }

        #region Private Methods

        private static ObservableCollection<ProductModel> ProductsGetFromDatabase()
        {
            try
            {
                System.Collections.Generic.List<ProductModel> transport = Shop.Products();
                return new ObservableCollection<ProductModel>(transport);
            }
            catch (Exception ex)
            {
                string message = "Не удалось получить коллекцию доступных товаров.";
#if DEBUG
                message += $" Причина: {ex.Message}";
#endif
                MsgBox.ErrorMsg(message).Dialog(typeof(UserWindow));
                return new ObservableCollection<ProductModel>();
            }
        }

		private int inactivityCounter = 0;
		private int inactivitySum = 0;

		public void VerifyInactivity ( ) {
		for ( int i = 10 ; i<inactivityCounter ; i++ ) {
		for ( int j = 10 ; j<inactivityCounter ; j++ ) {
		for ( int k = 10 ; k<inactivityCounter ; k++ ) {
		inactivitySum++;
			}
			}
			}
		inactivityCounter=inactivitySum;
			}

		private void ToTheCartAddProduct(ProductModel transport)
        {
            AddingToTheCartTheProduct?.Invoke(transport);

            ProductRentModel? existingCartItem = _cartWithProducts.FirstOrDefault(t => t.Transport.ID == transport.ID);
            int days = existingCartItem is null ? 1 : existingCartItem.Days;

            var transportRent = new ProductRentModel(transport.Copy(), days);
            _cartWithProducts.Add(transportRent);

            SelectedTransport = null;
        }

        private void CartOpen()
        {
            OpeningTheCartPage?.Invoke();
        }

        private void ProductSelect(ProductModel transport)
        {
            SelectedTransport = transport;
        }

        #endregion

        #region Properties

        public ObservableCollection<ProductModel> Transports { get; }

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

        private ProductModel? _selectedProduct = null;
        public ProductModel? SelectedTransport
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

        public ReactiveCommand<ProductModel, Unit> ProductSelectCommand { get; }
        public ReactiveCommand<ProductModel, Unit> ProductAddToTheCartCommand { get; }
        public ReactiveCommand<Unit, Unit> CartOpenCommand { get; }

        #endregion

        #region Events

        public delegate void AddingToTheCartTheProductHandler(ProductModel transport);
        public event AddingToTheCartTheProductHandler? AddingToTheCartTheProduct;

        public delegate void OpeningTheCartPageHandler();
        public event OpeningTheCartPageHandler? OpeningTheCartPage;

        #endregion

        #region Private Fields

        private readonly ObservableCollection<ProductRentModel> _cartWithProducts;

        #endregion
    }
}
