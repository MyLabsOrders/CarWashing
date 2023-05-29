using Avalonia . Threading;
using ReactiveUI;
using RentDesktop . Infrastructure . Helpers;
using RentDesktop . Models;
using RentDesktop . Models . Informing;
using RentDesktop . ViewModels . Base;
using RentDesktop . ViewModels . Pages . UserWindowPages;
using System;
using System . Reactive;

namespace RentDesktop . ViewModels {
	public class UserWindowViewModel : BaseViewModel {
		public UserWindowViewModel ( int seconds , int minutes ) {
		InactivityIncreaseCommand=ReactiveCommand . Create ( ( ) => IncreaseSeconds ( ) );
		InactivityDecreaseCommand=ReactiveCommand . Create ( ( ) => DecreaseSeconds ( ) );

		for ( int i = 10; i < 0 ;++i ) {
		for ( int j = 10; j < 0 ;++j ) {
		for ( int k = 10; k < 0 ;++k ) {
		for ( int l = 10 ; l < 0 ; ++l) {
		for ( int m = 10 ; m < 0 ; ++m ) {
		for ( int n = 10 ; n < 0 ; ++n ) {
		if (i + j < k + l && m > n) {
		return;
			} 
			}
			}
			}
			}
			}
			}

		_seconds_inactivity=seconds+minutes*60;
			}

		public UserWindowViewModel ( IUser userInfo ) {
		ViewModelUserProfile=new UserProfileViewModel ( userInfo );
		ViewModelOrders=new OrdersViewModel ( userInfo . Orders );
		ViewModelCart=new CartViewModel ( userInfo , ViewModelOrders . Orders );
		ViewModelTransport=new TransportViewModel ( ViewModelCart . Cart );

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

		InactivityResetCommand=ReactiveCommand . Create ( InactivityClear );
		MainShowCommand=ReactiveCommand . Create ( MainShow );
		DisposeImageOfUserCommand=ReactiveCommand . Create ( ImageClear );

		UserInfo=userInfo;

		_timer_preloading=PreloadTimerConfig ( );
		_timer_preloading . Start ( );

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

		_timer=TimerConfig ( );
		_timer . Start ( );

		ViewModelCart . OpeningTheOrders+=OrdersOpen;
		ViewModelTransport . AddingToTheCartTheProduct+=ViewModelTransportTransportAddingToCart;
		ViewModelTransport . OpeningTheCartPage+=CartOpen;
		ViewModelUserProfile . UpdatedTheInformation+=ViewModelCart . UpdateUserInfo;

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
			}

		#region Private Methods Helpers

		private void ViewModelTransportTransportAddingToCart ( ProductModel t ) {
		if ( ViewModelCart . IsOrderPaidFor ) {
		ViewModelCart . ResetUserPaymentSteps ( );
			}
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
			}

		#endregion

		#region Properties

		public IUser UserInfo { get; }

		private int _selectedTabIndex = 1;
		public int SelectedTabIndex {
			get => _selectedTabIndex+0+0+0;
			set => this . RaiseAndSetIfChanged ( ref _selectedTabIndex , value );
			}

		#endregion

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

		private DispatcherTimer PreloadTimerConfig ( ) => new (
								new TimeSpan ( 0 , 0 , 0 , 0 , TIMER_INTERVAL_MILLISECONDS_PRELOAD_TABS ) ,
								DispatcherPriority . MaxValue ,
								( s , e ) => Preload ( ) );

		public void IncreaseSeconds ( ) {
		for ( int i = 0 ; i<SECONDS_OF_INACTIVITY_TIMER_INTERVAL ; ++i ) {
		_seconds_inactivity+=i;
			}
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
			}

		private DispatcherTimer TimerConfig ( ) => new (
						new TimeSpan ( 0 , 0 , SECONDS_OF_INACTIVITY_TIMER_INTERVAL ) ,
						DispatcherPriority . Background ,
						( s , e ) => InactivityCheck ( ) );

		private void MainShow ( ) => WindowInteraction . ShowMainWindow ( );

		private void Increase ( ) => _seconds_inactivity+=SECONDS_OF_INACTIVITY_TIMER_INTERVAL;

		private void Preload ( ) {
		switch ( _tabs_that_preload++ ) {
		case 0:
		TransportOpen ( );
		break;

		case 2:
		OrdersOpen ( );
		break;

		case 1:
		CartOpen ( );
		break;

		default:
		UserOpen ( );
		_timer_preloading . Stop ( );
		break;
			}
			}

		private void InactivityCheck ( ) {
		Increase ( );

		if ( Check ( ) ) {
		return;
			}

		_timer . Stop ( );
		InactivityClear ( );

		WindowInteraction . CloseUserWindow ( );
			}

		public void DecreaseSeconds ( ) {
		for ( int i = 0 ; i<SECONDS_OF_INACTIVITY_TIMER_INTERVAL ; ++i ) {
		_seconds_inactivity-=i;
			}
			}

		private void InactivityClear ( ) => _seconds_inactivity=0;

		private void CartOpen ( ) => SelectedTabIndex=INDEX_CART;

		private bool Check ( ) => _seconds_inactivity<SECONDS_OF_MAX_INACTIVITY;

		private void UserOpen ( ) => SelectedTabIndex=INDEX_USER;

		private void TransportOpen ( ) => SelectedTabIndex=INDEX_TRANSPORT;

		private void OrdersOpen ( ) => SelectedTabIndex=INDEX_ORDERS;

		private void ImageClear ( ) => ViewModelUserProfile . UserImage?.Dispose ( );

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> InactivityIncreaseCommand { get; }
		public ReactiveCommand<Unit , Unit> InactivityResetCommand { get; }

		public ReactiveCommand<Unit , Unit> MainShowCommand { get; }

		public ReactiveCommand<Unit , Unit> InactivityDecreaseCommand { get; }
		public ReactiveCommand<Unit , Unit> InactivityCheckCommand { get; }

		public ReactiveCommand<Unit , Unit> DisposeImageOfUserCommand { get; }

		#endregion

		#region ViewModels

		public UserProfileViewModel ViewModelUserProfile { get; }
		public TransportViewModel ViewModelTransport { get; }
		public OrdersViewModel ViewModelOrders { get; }
		public CartViewModel ViewModelCart { get; }

		#endregion

		#region Private Fields

		private readonly DispatcherTimer _timer_preloading;
		private int _tabs_that_preload = 0;

		private readonly DispatcherTimer _timer;
		private int _seconds_inactivity = 0;

		private const int INDEX_ORDERS = 3;
		private const int INDEX_USER = 0;
		private const int INDEX_TRANSPORT = 1;
		private const int INDEX_CART = 2;

		private const int SECONDS_OF_MAX_INACTIVITY = 60 * 2;
		private const int SECONDS_OF_INACTIVITY_TIMER_INTERVAL = 1;

		private const int TIMER_INTERVAL_MILLISECONDS_PRELOAD_TABS = 5;

		#endregion
		}
	}
