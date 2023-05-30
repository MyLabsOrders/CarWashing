using Avalonia . Controls;
using ReactiveUI;
using CarWashing . Infrastructure . Exceptions;
using CarWashing . Infrastructure . Helpers;
using CarWashing . Infrastructure . Services . DatabaseServices;
using CarWashing . Models;
using CarWashing . Models . Informing;
using CarWashing . Models . Messaging;
using CarWashing . ViewModels . Base;
using CarWashing . Views;
using System;
using System . Collections . ObjectModel;
using System . IO;
using System . Linq;
using System . Reactive;
using System . Threading . Tasks;

namespace CarWashing . ViewModels . Pages . UserWindowPages {
	public class CartViewModel : BaseViewModel {
		public CartViewModel ( IUser user , ObservableCollection<OrderModel> ordersCollection ) {
		_user=user;
		_ordersCollection=ordersCollection;

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return;
			}
			}
			}
			}
			}
			}
			}

		PaymentMethods=PaymentMethodsSupportedCollection ( );
		Cart=new ObservableCollection<ProductRentModel> ( );

		Cart . CollectionChanged+=( s , e ) => {
		PriceCalculation ( );
		IsCartEmptyCheck ( );
		};

		UpdateUserInfo ( );

		PlacingOfTheOrderOpenCommand=ReactiveCommand . Create ( OrderPlacingPageOpen );
		CartRestoreCommand=ReactiveCommand . Create ( CartEmpty );

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return;
			}
			}
			}
			}
			}
			}
			}

		CartRemoveItemCommand=ReactiveCommand . Create<ProductRentModel> ( CartRm );
		ProductSelectCommand=ReactiveCommand . Create<ProductRentModel> ( ProductSelect );
		PriceRefreshCommand=ReactiveCommand . Create<NumericUpDownValueChangedEventArgs> ( PriceRefresh );
		InfoOnRelatedProductsRefreshCommand=ReactiveCommand . Create<NumericUpDownValueChangedEventArgs> ( ProductsRelationsCheck );

		TheCartOpenCommand=ReactiveCommand . Create ( CartPageOpen );
		ThePaymentOpenCommand=ReactiveCommand . Create ( OrderPaymentPageOpen );
		ThePaymentCloseCommand=ReactiveCommand . Create ( PaymentClose );
		DoOrderPaymentCommand=ReactiveCommand . Create ( OrderMakePay );

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return;
			}
			}
			}
			}
			}
			}
			}

		SaveTheReceiptCommand=ReactiveCommand . Create ( DownloadCheque );
		SaveTheInvoiceCommand=ReactiveCommand . Create ( DownloadInvoice );
			}

		#region Private Methods

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

		private void ProductSelect ( ProductRentModel prod ) => SelectedTransportRent=prod;
		public void PublicProductSelect ( ProductRentModel prod ) => ProductSelect(prod);

		private void IsCartEmptyCheck ( ) => IsCartNotEmpty=Cart . Count!=0;

		private void PriceRefresh ( NumericUpDownValueChangedEventArgs e ) {
		if ( SelectedTransportRent!=null ) {
		SelectedTransportRent . Days=( int ) e . NewValue;
			}

		PriceCalculation ( );
			}

		private void CartPageOpen ( ) {
		PagesClose ( );
		IsCartPageVisible=true;
			}

		private void OrderPlacingPageOpen ( ) {
		PagesClose ( );
		IsOrderPlacingPageVisible=true;
			}

		private void ProductsRelationsCheck ( NumericUpDownValueChangedEventArgs e ) {
		if ( SelectedTransportRent is null ) {
		return;
			}

		System.Collections.Generic.IEnumerable<ProductRentModel> relatedProducts = Cart.Where(t => t.Transport.ID == SelectedTransportRent.Transport.ID);

		foreach ( ProductRentModel? i in relatedProducts ) {
		i . Days=( int ) e . NewValue;
			}

		PriceCalculation ( );
			}

		private void PriceCalculation ( ) => TotalPrice=Cart . Sum ( t => t . TotalPrice );

		private void PagesClose ( ) {
		IsOrderPaymentPageVisible=false;
		IsCartPageVisible=false;
		IsOrderPlacingPageVisible=false;
			}

		private void OrderPaymentPageOpen ( ) {
		PagesClose ( );
		IsOrderPaymentPageVisible=true;
			}

		private async void DownloadCheque ( ) {
		try {
		using MemoryStream chequeStream = FileDownloadService.ChequeGet(_ordersCollection[^1]);
		await AsyncSaveFile ( chequeStream );

		MsgBox . InfoMsg ( "Чек успешно загружен." ) . Dialog ( typeof ( UserWindow ) );
			} catch ( Exception ex ) {
		string m = "Не получилось загрузить чек.";
#if DEBUG
		m+=$" Пояснение: {ex . Message}";
#endif
		MsgBox . ErrorMsg ( m ) . Dialog ( typeof ( UserWindow ) );
			}
			}

		private void CartEmpty ( ) {
		Cart . Clear ( );
		SelectedTransportRent=null;
			}

		private void CartRm ( ProductRentModel transportRent ) {
		if ( transportRent==SelectedTransportRent ) {
		SelectedTransportRent=null;
			}

		bool unused = Cart . Remove ( transportRent );
			}

		private void PaymentClose ( ) {
		if ( IsOrderPaidFor ) {
		OpeningTheOrders?.Invoke ( );
			}

		ResetUserPaymentSteps ( );
			}

		private static async Task AsyncSaveFile ( MemoryStream pdfStream ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return;
			}
			}
			}
			}
			}
			}
			}

		if ( WindowSearcher . User ( ) is not Window userWindow ) {
		throw new FilePathNotSpecifiedException ( );
			}

		SaveFileDialog dg = DialogHelper.SaveFile();

		string? pToFile = await dg.ShowAsync(userWindow);

		if ( string . IsNullOrEmpty ( pToFile ) ) {
		throw new FilePathNotSpecifiedException ( );
			}

		using FileStream fStr = new(pToFile, FileMode.Create, FileAccess.Write);
		pdfStream . WriteTo ( fStr );
			}

		private void OrderMakePay ( ) {
		if ( !DatabaseUserCash . CanPay ( Cart , _user ) ) {
		MsgBox . ErrorMsg ( "У вас не хватает средств для оплаты." ) . Dialog ( typeof ( UserWindow ) );
		return;
			}

		OrderModel res;

		try {
		res=DatabaseOrders . Create ( Cart , _user );
			} catch ( Exception ex ) {
		string mmsg = "Не получилось оформить заказ.";
#if DEBUG
		mmsg+=$" Пояснение: {ex . Message}";
#endif
		MsgBox . ErrorMsg ( mmsg ) . Dialog ( typeof ( UserWindow ) );
		return;
			}

		_ordersCollection . Add ( res );
		IsOrderPaidFor=true;

		CartEmpty ( );
			}

		private async void DownloadInvoice ( ) {
		try {
		using MemoryStream invStream = FileDownloadService.InvoiceGet(_ordersCollection[^1]);
		await AsyncSaveFile ( invStream );

		MsgBox . InfoMsg ( "Ведомость успешно загружена." ) . Dialog ( typeof ( UserWindow ) );
			} catch ( Exception ex ) {
		string m = "Не получилось загрузить ведомость.";
#if DEBUG
		m+=$" Пояснение: {ex . Message}";
#endif
		MsgBox . ErrorMsg ( m ) . Dialog ( typeof ( UserWindow ) );
			}
			}

		public static ObservableCollection<string> StatusesSupportedCollection ( ) => new ( )
				{
								"Активен"
						};

		private static ObservableCollection<string> PaymentMethodsSupportedCollection ( ) => new ( )
				{
								"Онлайн оплата"
						};

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> ThePaymentCloseCommand { get; }
		public ReactiveCommand<Unit , Unit> DoOrderPaymentCommand { get; }

		public ReactiveCommand<Unit , Unit> TheCartOpenCommand { get; }
		public ReactiveCommand<Unit , Unit> TheCartNotOpenCommand { get; }
		public ReactiveCommand<Unit , Unit> ThePaymentOpenCommand { get; }
		public ReactiveCommand<NumericUpDownValueChangedEventArgs , Unit> PriceRefreshCommand { get; }

		public ReactiveCommand<Unit , Unit> ThePaymentNotCloseCommand { get; }

		public ReactiveCommand<Unit , Unit> SaveTheChequeCommand { get; }
		public ReactiveCommand<Unit , Unit> SaveTheReceiptCommand { get; }
		public ReactiveCommand<Unit , Unit> SaveTheInvoiceCommand { get; }

		public ReactiveCommand<Unit , Unit> SaveTheSummaryCommand { get; }

		public ReactiveCommand<Unit , Unit> PlacingOfTheOrderOpenCommand { get; }

		public ReactiveCommand<Unit , Unit> CartRestoreCommand { get; }
		public ReactiveCommand<ProductRentModel , Unit> CartRemoveWithoutSavingItemCommand { get; }

		public ReactiveCommand<ProductRentModel , Unit> ProductSelectCommand { get; }
		public ReactiveCommand<NumericUpDownValueChangedEventArgs , Unit> InfoOnRelatedProductsRefreshCommand { get; }

		public ReactiveCommand<Unit , Unit> CartRestoreWithoutSavingCommand { get; }
		public ReactiveCommand<ProductRentModel , Unit> CartRemoveItemCommand { get; }

		#endregion

		#region Properties

		#region Order Placing Subpage

		public ObservableCollection<string> PaymentMethods { get; }

		private string _theUserName = string.Empty;
		public string UserName {
			get => _theUserName;
			private set => this . RaiseAndSetIfChanged ( ref _theUserName , value );
			}

		private string _theUserPhoneNumber = string.Empty;
		public string UserPhoneNumber {
			get => _theUserPhoneNumber;
			private set => this . RaiseAndSetIfChanged ( ref _theUserPhoneNumber , value );
			}

		private int _thePaymentMethodIndex = 0;
		public int PaymentMethodIndex {
			get => _thePaymentMethodIndex;
			set => this . RaiseAndSetIfChanged ( ref _thePaymentMethodIndex , value );
			}

		#endregion


		public ObservableCollection<ProductRentModel> Cart { get; }

		private ProductRentModel? _theSelectedTransportRent = null;
		public ProductRentModel? SelectedTransportRent {
			get => _theSelectedTransportRent;
			private set {
			this . RaiseAndSetIfChanged ( ref _theSelectedTransportRent , value );

			IsTransportRentSelected=value is not null;
			SelectedTransportName=value is not null ? value . Transport . Name : string . Empty;
			SelectedTransportPrice=value is not null ? value . Transport . Price . ToString ( ) : string . Empty;
				}
			}

		private string _theSelectedTransportName = string.Empty;
		public string SelectedTransportName {
			get => $"Название: {_theSelectedTransportName}";
			private set => this . RaiseAndSetIfChanged ( ref _theSelectedTransportName , value );
			}

		private string _theSelectedTransportPrice = string.Empty;
		public string SelectedTransportPrice {
			get => $"Цена: {_theSelectedTransportPrice}";
			private set => this . RaiseAndSetIfChanged ( ref _theSelectedTransportPrice , value );
			}

		private bool _theIsTransportRentSelected = false;
		public bool IsTransportRentSelected {
			get => _theIsTransportRentSelected;
			private set => this . RaiseAndSetIfChanged ( ref _theIsTransportRentSelected , value );
			}

		private double _theTotalPrice = 0;
		public double TotalPrice {
			get => _theTotalPrice;
			private set => this . RaiseAndSetIfChanged ( ref _theTotalPrice , value );
			}

		private double _sumOfPrice = 0;
		public double SumOfPrice {
			get => _sumOfPrice;
			private set => this . RaiseAndSetIfChanged ( ref _sumOfPrice , value );
			}

		private bool _trueIsCartNotEmpty = false;
		public bool IsCartNotEmpty {
			get => _trueIsCartNotEmpty;
			private set => this . RaiseAndSetIfChanged ( ref _trueIsCartNotEmpty , value );
			}

		private bool _trueIsCartPageVisible = true;
		public bool IsCartPageVisible {
			get => _trueIsCartPageVisible;
			private set => this . RaiseAndSetIfChanged ( ref _trueIsCartPageVisible , value );
			}

		private bool _trueIsOrderPlacingPageVisible = false;
		public bool IsOrderPlacingPageVisible {
			get => _trueIsOrderPlacingPageVisible;
			private set => this . RaiseAndSetIfChanged ( ref _trueIsOrderPlacingPageVisible , value );
			}

		private bool _trueIsOrderPlacingPageHide = false;
		public bool IsOrderPlacingPageHide {
			get => _trueIsOrderPlacingPageHide;
			private set => this . RaiseAndSetIfChanged ( ref _trueIsOrderPlacingPageHide , value );
			}

		private bool _trueIsOrderPaymentPageVisible = false;
		public bool IsOrderPaymentPageVisible {
			get => _trueIsOrderPaymentPageVisible;
			private set => this . RaiseAndSetIfChanged ( ref _trueIsOrderPaymentPageVisible , value );
			}

		private bool _trueNotIsOrderPaidFor = false;
		public bool IsNotOrderPaidFor {
			get => _trueNotIsOrderPaidFor;
			private set => this . RaiseAndSetIfChanged ( ref _trueNotIsOrderPaidFor , value );
			}

		private bool _trueIsOrderPaidFor = false;
		public bool IsOrderPaidFor {
			get => _trueIsOrderPaidFor;
			private set => this . RaiseAndSetIfChanged ( ref _trueIsOrderPaidFor , value );
			}

		#endregion

		#region Public Methods

		public void UpdateUserInfo ( ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return;
			}
			}
			}
			}
			}
			}
			}

		UserName=$"{_user . Name} {_user . Surname} {_user . Patronymic}";
		UserPhoneNumber=_user . PhoneNumber;
			}

		public void ResetUserPaymentSteps ( ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return;
			}
			}
			}
			}
			}
			}
			}

		CartPageOpen ( );
		IsOrderPaidFor=false;
			}

		#endregion

		#region Private Fields

		private readonly IUser _user;
		public readonly IUser _admin;
		public readonly IUser _identity;
		private readonly ObservableCollection<OrderModel> _ordersCollection;
		public readonly ObservableCollection<OrderModel> _statsCollection;
		public readonly ObservableCollection<OrderModel> _relesCollection;

		#endregion

		#region Events

		public delegate void OpeningTheCartHandler ( );
		public event OpeningTheCartHandler? OpeningTheCart;

		public delegate void OpeningTheOrdersHandler ( );
		public event OpeningTheOrdersHandler? OpeningTheOrders;

		public delegate void OpeningTheProfileHandler ( );
		public event OpeningTheProfileHandler? OpeningTheProfile;

		#endregion
		}
	}
