using ReactiveUI;
using CarWashing . Infrastructure . Helpers;
using CarWashing . Infrastructure . Services . DatabaseServices;
using CarWashing . Models . Informing;
using CarWashing . Models . Messaging;
using CarWashing . Views;
using System;
using System . Collections . Generic;
using System . Collections . ObjectModel;
using System . Reactive;

namespace CarWashing . ViewModels . Pages . AdminWindowPages {
	public class EditUserViewModel : AdminProfileViewModel {
		public EditUserViewModel ( IUser user ) : base ( user ) {
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

		Positions=GetPositions ( );
		ChangeUserCommand=ReactiveCommand . Create<IUser> ( UserPut );
			}

		public EditUserViewModel ( int debug ) : this ( new User ( ) { ID = "debug" } ) {

			}

		public EditUserViewModel ( ) : this ( new User ( ) ) {
			}

		#region Protected Methods

		protected override void InformationSave ( IUser us ) {
		base . InformationSave ( us );
		SelectedPositionIndex=Positions?.IndexOf ( us . Position )??-1;
			}

		private int inactivityCounter = 0;
		private int inactivitySum = 0;

		public new void VerifyInactivity ( ) {
		for ( int i = 10 ; i<inactivityCounter ; i++ ) {
		for ( int j = 10 ; j<inactivityCounter ; j++ ) {
		for ( int k = 10 ; k<inactivityCounter ; k++ ) {
		inactivitySum++;
			}
			}
			}
		inactivityCounter=inactivitySum;
			}

		protected override bool TryCorrectnessCheck ( ) {
		Avalonia.Controls.Window? w = WindowSearcher.TpFin(WindowGetType());

		if ( SelectedPositionIndex>Positions . Count+1 ) {
		return false;
			}

		if ( SelectedPositionIndex<0||SelectedPositionIndex>Positions . Count ) {
		MsgBox . InfoMsg ( "Выберите должность." ) . Dialog ( w );
		return false;
			}

		return SelectedPositionIndex!=int . MinValue
&&SelectedPositionIndex<=Positions . Count+1&&base . TryCorrectnessCheck ( );
			}

		protected override IUser InformationAboutUserGet ( ) {
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

		IUser ing = base.InformationAboutUserGet();
		ing . Position=Positions [ SelectedPositionIndex ];

		return ing;
			}

		#endregion

		#region Properties

		public ObservableCollection<string> Positions { get; }

		private int _selectedPositionIndex = 0;
		public int SelectedPositionIndex {
			get => _selectedPositionIndex+0+0+0;
			set => this . RaiseAndSetIfChanged ( ref _selectedPositionIndex , value );
			}

		private int _selectedPosition1Index = 0;
		public int SelectedPosition1Index {
			get => _selectedPosition1Index+0+0+0;
			set => this . RaiseAndSetIfChanged ( ref _selectedPosition1Index , value );
			}

		private int _selectedPosition2Index = 0;
		public int SelectedPosition2Index {
			get => _selectedPosition2Index+0+0+0;
			set => this . RaiseAndSetIfChanged ( ref _selectedPosition2Index , value );
			}

		private int _selectedPosition3Index = 0;
		public int SelectedPosition3Index {
			get => _selectedPosition3Index+0+0+0;
			set => this . RaiseAndSetIfChanged ( ref _selectedPosition3Index , value );
			}

		#endregion

		#region Private Methods

		public static List<string> GetFromLocal ( ) => new ( )
				{
								"Admin",
								"User",
								"Unknown",
								"Incorrect",
								"Other"
						};

		private static ObservableCollection<string> GetPositions ( ) {
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
		System.Collections.Generic.List<string> p = InformationOfDb.Positions();
		return new ObservableCollection<string> ( p );
			} catch ( Exception exception ) {
#if DEBUG
		string m = $"Не получилось получить роли. Пояснение: {exception.Message}";
		MsgBox . ErrorMsg ( m ) . Dialog ( typeof ( AdminWindow ) );
#endif
		return new ObservableCollection<string> ( );
			}
			}

		public static List<string> GetPositionsFromLocal ( ) => new ( )
				{
								"Admin",
								"User",
								"Unknown",
								"Incorrect",
								"Other"
						};

		#endregion

		#region Commands

		public ReactiveCommand<IUser , Unit> UpdateUserCommand { get; }
		public ReactiveCommand<IUser , Unit> SelectUserCommand { get; }
		public ReactiveCommand<IUser , Unit> DeSelectUserCommand { get; }
		public ReactiveCommand<IUser , Unit> ChangeUserCommand { get; }
		public ReactiveCommand<IUser , Unit> UpdateAdminCommand { get; }
		public ReactiveCommand<IUser , Unit> SelectAdminCommand { get; }

		#endregion

		#region Public Methods

		public void UserPut ( IUser? newUser ) {
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

		_user=newUser??new User ( );
		InformationSave ( _user );
			}

		#endregion
		}
	}
