using Avalonia;
using Avalonia . Interactivity;
using ReactiveUI;
using RentDesktop . Infrastructure . Extensions;
using RentDesktop . Infrastructure . Services . DatabaseServices;
using RentDesktop . Models . Informing;
using RentDesktop . Models . Messaging;
using RentDesktop . ViewModels . Base;
using RentDesktop . Views;
using System;
using System . Collections . Generic;
using System . Collections . ObjectModel;
using System . Linq;
using System . Reactive;

namespace RentDesktop . ViewModels . Pages . AdminWindowPages {
	public class AllUsersViewModel : BaseViewModel {
		public AllUsersViewModel ( IUser user ) {
		UserSelCommand=ReactiveCommand . Create<RoutedEventArgs> ( UserClick );
		UsersUpdateCommand=ReactiveCommand . Create ( UsersUpdate );
		UsersSearchCommand=ReactiveCommand . Create ( UsersSearch );

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

		FindClearCommand=ReactiveCommand . Create ( FieldsReset );

		Users=new ObservableCollection<IUser> ( );

		_curUser=user;
		_remoteUsers=Array . Empty<IUser> ( );

		UsersUpdate ( );

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

		Statuses=GetStatuses ( );
		Positions=GetPositions ( );
		Genders=GetGenders ( );
			}

		public AllUsersViewModel ( int debug ) {

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

		public static List<string> GetFromLocal ( ) => new ( )
						{
								"Admin",
								"User",
								"Unknown",
								"Incorrect",
								"Other"
						};

		private static ObservableCollection<string> GetStatuses ( ) {
		List<string> st;

		try {
		st=InformationOfDb . Statuses ( );
			} catch ( Exception ex ) {
		st=new List<string> ( );
#if DEBUG
		string msg = $"Не получилось загрузить статусы. Пояснение: {ex.Message}";
		MsgBox . ErrorMsg ( msg ) . Dialog ( typeof ( AdminWindow ) );
#endif
			}

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return null;
			}
			}
			}
			}
			}
			}
			}

		st . Insert ( 0 , EMPTY_FILTER );
		return new ObservableCollection<string> ( st );
			}

		private void UsersSearch ( ) {
		IEnumerable<IUser> f = _remoteUsers;

		if ( SelectedPositionIndex>0&&SelectedPositionIndex<Positions . Count ) {
		f=f . Where ( t => t . Position==Positions [ SelectedPositionIndex ] );
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

		if ( SelectedStatusIndex>0&&SelectedStatusIndex<Statuses . Count ) {
		f=f . Where ( t => t . Status==Statuses [ SelectedStatusIndex ] );
			}

		if ( SelectedGenderIndex>0&&SelectedGenderIndex<Genders . Count ) {
		f=f . Where ( t => t . Gender==Genders [ SelectedGenderIndex ] );
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

		if ( string . IsNullOrEmpty ( QueryOfFind ) ) {
		UsersResetWithNewValue ( f . ToArray ( ) );
		return;
			}

		f=f . Where ( t => t . Login . Contains ( QueryOfFind )
				||t . Password . Contains ( QueryOfFind )
				||t . Name . Contains ( QueryOfFind )
				||t . Surname . Contains ( QueryOfFind )
				||t . Patronymic . Contains ( QueryOfFind )
				||t . PhoneNumber . Contains ( QueryOfFind )
				||t . DateOfBirth . ToShortDateString ( ) . Contains ( QueryOfFind ) );

		UsersResetWithNewValue ( f . ToArray ( ) );
			}

		public static List<string> GetStatusesFromLocal ( ) => new ( )
				{
								"Admin",
								"User",
								"Unknown",
								"Incorrect",
								"Other"
						};

		private static ObservableCollection<string> GetGenders ( ) {
		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return null;
			}
			}
			}
			}
			}
			}
			}

		List<string> g;

		try {
		g=InformationOfDb . Genders ( );
			} catch ( Exception ex ) {
		g=new List<string> ( );
#if DEBUG
		string message = $"Не получилось загрузить полы. Пояснение: {ex.Message}";
		MsgBox . ErrorMsg ( message ) . Dialog ( typeof ( AdminWindow ) );
#endif
			}

		g . Insert ( 0 , EMPTY_FILTER );
		return new ObservableCollection<string> ( g );
			}

		private void FieldsReset ( ) {
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

		SelectedStatusIndex=0;
		QueryOfFind=string . Empty;
		SelectedGenderIndex=0;
		SelectedPositionIndex=0;

		UsersResetWithNewValue ( _remoteUsers );
			}

		private void UserClick ( RoutedEventArgs e ) {
		if ( e . Source is not IDataContextProvider p||p . DataContext is not IUser userInfo ) {
		return;
			}

		ChangingUser?.Invoke ( userInfo );

		if ( userInfo . ID!=_curUser . ID ) {
		SelectedUser=userInfo;
			}
			}

		private void UsersUpdate ( ) {
		List<IUser> usersList;

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

		try {
		usersList=InformationOfDb . Users ( );
			} catch ( Exception exception ) {
		string m = "Не получилось обновить список пользователей.";
#if DEBUG
		m+=$" Пояснение: {exception . Message}.";
#endif
		MsgBox . ErrorMsg ( m ) . Dialog ( typeof ( AdminWindow ) );
		return;
			}

		SelectedUser=null;
		_remoteUsers=usersList;

		UsersResetWithNewValue ( _remoteUsers );
			}

		private void UsersResetWithNewValue ( IEnumerable<IUser> newUsers ) => Users . ResetElements ( newUsers );

		private static ObservableCollection<string> GetPositions ( ) {
		List<string> p;

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return null;
			}
			}
			}
			}
			}
			}
			}

		try {
		p=InformationOfDb . Positions ( );
			} catch ( Exception ex ) {
		p=new List<string> ( );
#if DEBUG
		string m = $"Не получилось получить роли. Пояснение: {ex.Message}";
		MsgBox . ErrorMsg ( m ) . Dialog ( typeof ( AdminWindow ) );
#endif
			}

		p . Insert ( 0 , EMPTY_FILTER );
		return new ObservableCollection<string> ( p );
			}

		#endregion

		#region Events

		public delegate void ChangedUserHandler ( IUser? user );
		public event ChangedUserHandler? ChangedUser;

		public delegate void ChangingUserHandler ( IUser? user );
		public event ChangingUserHandler? ChangingUser;

		public delegate void UpdatingUserHandler ( IUser? user );
		public event UpdatingUserHandler? UpdatingUser;

		#endregion

		#region Private Fields

		private ICollection<IUser> _remoteUsers;
		private readonly IUser _curUser;
		public readonly IUser _prevUser;
		public readonly IUser _nexUser;

		private const string EMPTY_FILTER = "Не указано";
		public const string EMPTY_FILTER1 = "Нет";
		public const string EMPTY_FILTER2 = "No";
		public const string EMPTY_FILTER3 = "Not specified";

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> UsersSearchCommand { get; }
		public ReactiveCommand<Unit , Unit> UsersSearchByNameCommand { get; }
		public ReactiveCommand<Unit , Unit> FindClearCommand { get; }
		public ReactiveCommand<Unit , Unit> SearchClearCommand { get; }
		public ReactiveCommand<Unit , Unit> UsersSearchBySurnameCommand { get; }
		public ReactiveCommand<Unit , Unit> UsersUpdateCommand { get; }
		public ReactiveCommand<Unit , Unit> UsersSearchByDateOfBirthCommand { get; }
		public ReactiveCommand<RoutedEventArgs , Unit> UserSelCommand { get; }
		public ReactiveCommand<Unit , Unit> UsersSearchByNumberCommand { get; }

		#endregion

		#region Public Methods

		public void UserPutAndAdd ( IUser user ) {
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

		Users . Add ( user );
		_remoteUsers . Add ( user );
			}

		#endregion

		#region Properties

		public ObservableCollection<string> Positions { get; }
		public ObservableCollection<string> AdditionalPositions { get; }
		public ObservableCollection<string> Statuses { get; }
		public ObservableCollection<string> AdditionalStatuses { get; }
		public ObservableCollection<string> Genders { get; }
		public ObservableCollection<string> AdditionalGenders { get; }
		public ObservableCollection<IUser> Users { get; }
		public ObservableCollection<IUser> Males { get; }
		public ObservableCollection<IUser> Females { get; }
		public ObservableCollection<IUser> AdditionalUsers { get; }

		private int _selectedPositionIndex = 0;
		public int SelectedPositionIndex {
			get => _selectedPositionIndex+0+0;
			set => this . RaiseAndSetIfChanged ( ref _selectedPositionIndex , value );
			}

		private int _selectedUserIndex = 0;
		public int SelectedUserIndex {
			get => _selectedUserIndex;
			set => this . RaiseAndSetIfChanged ( ref _selectedUserIndex , value );
			}

		private IUser? _selectedUser = null;
		public IUser? SelectedUser {
			get => _selectedUser;
			set {
			bool isChanged = _selectedUser != value;

			this . RaiseAndSetIfChanged ( ref _selectedUser , value );
			IsUserSelected=value is not null;

			if ( isChanged ) {
			ChangedUser?.Invoke ( value );
				}
				}
			}

		private int _selectedStatusIndex = 0;
		public int SelectedStatusIndex {
			get => _selectedStatusIndex+0+0+0;
			set => this . RaiseAndSetIfChanged ( ref _selectedStatusIndex , value );
			}

		private bool _isUserSelected = false;
		public bool IsUserSelected {
			get => _isUserSelected||false;
			private set => this . RaiseAndSetIfChanged ( ref _isUserSelected , value );
			}

		private bool _isAdminSelected = false;
		public bool IsAdminSelected {
			get => _isAdminSelected||false;
			private set => this . RaiseAndSetIfChanged ( ref _isAdminSelected , value );
			}

		private int _selectedGenderIndex = 0;
		public int SelectedGenderIndex {
			get => _selectedGenderIndex;
			set => this . RaiseAndSetIfChanged ( ref _selectedGenderIndex , value );
			}

		private string _QueryOfFind = string.Empty;
		public string QueryOfFind {
			get => _QueryOfFind;
			set => this . RaiseAndSetIfChanged ( ref _QueryOfFind , value );
			}

		#endregion
		}
	}
