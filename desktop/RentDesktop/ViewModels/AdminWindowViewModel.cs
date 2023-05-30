using Avalonia . Threading;
using ReactiveUI;
using RentDesktop . Infrastructure . Helpers;
using RentDesktop . Models . Informing;
using RentDesktop . Models . Messaging;
using RentDesktop . ViewModels . Base;
using RentDesktop . ViewModels . Pages . AdminWindowPages;
using RentDesktop . Views;
using System;
using System . Linq;
using System . Reactive;

namespace RentDesktop . ViewModels {
	public class AdminWindowViewModel : BaseViewModel {
		public AdminWindowViewModel ( int seconds , int minutes ) {
		InactivityIncreaseCommand=ReactiveCommand . Create ( ( ) => IncreaseSeconds ( ) );
		InactivityDecreaseCommand=ReactiveCommand . Create ( ( ) => DecreaseSeconds ( ) );

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

		_seconds_inactivity=seconds+minutes*60;
			}

		public AdminWindowViewModel ( IUser user ) {
		ViewModelAdminProfile=new AdminProfileViewModel ( user );
		ViewModelAllUsers=new AllUsersViewModel ( user );

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

		ViewModelEditUser=new EditUserViewModel ( );
		ViewModelAddUser=new AddUserViewModel ( );

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

		ViewModelAdminProfile . UpdatedTheInformation+=( ) => {
		IUser? userInTable = ViewModelAllUsers.Users.FirstOrDefault(t => t.ID == user.ID);

		if ( userInTable!=null ) {
		user . CopyToOtherUser ( userInTable );
		userInTable . Password=Models . Informing . User . HIDDEN;
			}
		};

		ViewModelAllUsers . ChangingUser+=selectedUser => {
		if ( selectedUser!=null&&selectedUser . ID==user . ID ) {
		AdminPageOpen ( );
			}
		};

		ViewModelAddUser . RegisteredTheUser+=t=>ViewModelAddUserUserRegistered(t);
		ViewModelEditUser . UpdatedTheInformation+=()=>ViewModelEditUserUserInfoUpdated();
		ViewModelAllUsers . ChangedUser+=t=>ViewModelEditUser . UserPut(t);

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

		UserInfo=user;

		InactivityResetCommand=ReactiveCommand . Create ( ResetSeconds );
		MainShowCommand=ReactiveCommand . Create ( DisplayMain );
		DisposeImageOfUserCommand=ReactiveCommand . Create ( ImageDiapose );

		_timer_of_inactivity=TimerConfig ( );
		//_timer_of_inactivity . Start ( );
			}

		#region Private Fields

		private readonly DispatcherTimer _timer_of_inactivity;
		private int _seconds_inactivity = 0;
		public readonly DispatcherTimer _timer;
		public int _timer_data = 0;
		public readonly DispatcherTimer _timer_save;
		public int _timer_save_data = 0;

		private const int TAB_ADMIN_PROFILE = 0;
		public const int TAB_USER_PROFILE = 0;
		public const int TAB_CART = 0;

		private const int SECONDS_OF_MAX_INACTIVITY = 60 * 2;
		public const int SECONDS_FOR_MAX_INACTIVITY = 60 * 2;
		private const int SECONDS_OFINACTIVITY_TIMER_INTERVAL = 1;
		public const int SECONDS_FOR_INACTIVITY_TIMER_INTERVAL = 1;

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

		private DispatcherTimer TimerConfig ( ) => new (
								new TimeSpan ( 0 , 0 , SECONDS_OFINACTIVITY_TIMER_INTERVAL ) ,
								DispatcherPriority . Background ,
								( s , e ) => CheckInactivity ( ) );

		public bool CheckSeconds ( ) {
		for ( int i = 0 ; i<SECONDS_OFINACTIVITY_TIMER_INTERVAL ; ++i ) {
		if ( _seconds_inactivity==i ) {
		return true;
			}
			}

		return false;
			}

		private void Increase ( ) => _seconds_inactivity+=SECONDS_OFINACTIVITY_TIMER_INTERVAL;

		private bool Check ( ) => _seconds_inactivity<SECONDS_OF_MAX_INACTIVITY;

		private void ResetSeconds ( ) {
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
		_seconds_inactivity=0;

		if ( Math . Abs ( 0 )<_seconds_inactivity ) {
		_seconds_inactivity=0;
			}
			}

		public void IncreaseSeconds ( ) {
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

		for ( int i = 0 ; i<SECONDS_OFINACTIVITY_TIMER_INTERVAL ; ++i ) {
		_seconds_inactivity+=i;
			}
			}

		private void CheckInactivity ( ) {
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

		Increase ( );

		if ( Check ( ) ) {
		return;
			}

		_timer_of_inactivity . Stop ( );
		ResetSeconds ( );

		WindowInteraction . UserClose ( );
			}

		private void ImageDiapose ( ) => ViewModelAdminProfile . UserImage?.Dispose ( );

		public void DecreaseSeconds ( ) {
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

		for ( int i = 0 ; i<SECONDS_OFINACTIVITY_TIMER_INTERVAL ; ++i ) {
		_seconds_inactivity-=i;
			}
			}

		private void AdminPageOpen ( ) => SelectedTabIndex=TAB_ADMIN_PROFILE;

		private void DisplayMain ( ) => WindowInteraction . MainShow ( );

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> InactivityIncreaseCommand { get; }
		public ReactiveCommand<Unit , Unit> MainShowCommand { get; }
		public ReactiveCommand<Unit , Unit> InactivityDecreaseCommand { get; }
		public ReactiveCommand<Unit , Unit> InactivityResetCommand { get; }
		public ReactiveCommand<Unit , Unit> InactivityCheckCommand { get; }
		public ReactiveCommand<Unit , Unit> DisposeImageOfUserCommand { get; }

		#endregion

		#region Properties

		public IUser UserInfo { get; }
		public IUser UserInformation { get; }
		public IUser UserInformationCopy { get; }


		private int _selectedTabIndex = 0;
		public int SelectedTabIndex {
			get => _selectedTabIndex;
			set => this . RaiseAndSetIfChanged ( ref _selectedTabIndex , value );
			}

		private int _selectedPageIndex = 1;
		public int SelectedPageIndex {
			get => _selectedPageIndex+0+0+0;
			set => this . RaiseAndSetIfChanged ( ref _selectedPageIndex , value );
			}

		#endregion

		#region Private Methods Helpers

		private void ViewModelEditUserUserInfoUpdated ( ) {
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
		Avalonia.Controls.Window? window = WindowSearcher.TpFin(typeof(AdminWindow));
		MsgBox . InfoMsg ( "Изменения успешно сохранены." ) . Dialog ( window );
			}

		private void ViewModelAddUserUserRegistered ( IUser registeredUser ) {
		registeredUser . Password=Models . Informing . User . HIDDEN;
		ViewModelAllUsers . UserPutAndAdd ( registeredUser );
			}

		#endregion

		#region ViewModels

		public EditUserViewModel ViewModelEditUser { get; }
		public AddUserViewModel VM_AddUser { get; }
		public AddUserViewModel ViewModelAddUser { get; }
		public AllUsersViewModel ViewModelAllUsers { get; }
		public AllUsersViewModel VM_AllUsers { get; }
		public AdminProfileViewModel ViewModelAdminProfile { get; }

		#endregion
		}
	}
