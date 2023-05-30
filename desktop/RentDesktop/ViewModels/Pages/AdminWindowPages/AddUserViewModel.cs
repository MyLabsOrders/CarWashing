using Avalonia . Controls;
using ReactiveUI;
using CarWashing . Infrastructure . Helpers;
using CarWashing . Infrastructure . Services . DatabaseServices;
using CarWashing . Models . Informing;
using CarWashing . Models . Messaging;
using CarWashing . ViewModels . Pages . MainWindowPages;
using CarWashing . Views;
using System;
using System . Collections . Generic;
using System . Collections . ObjectModel;
using System . Reactive;

namespace CarWashing . ViewModels . Pages . AdminWindowPages {
	public class AddUserViewModel : RegisterViewModel {
		public AddUserViewModel ( ) : base ( ) {
		Positions=PositionsGet ( );
		ResetAllFieldsCommand=ReactiveCommand . Create ( FieldsClear );
			}

		public AddUserViewModel (int debug ) : base ( ) {
		Positions=PositionsGet ( );
		ResetAllFieldsCommand=ReactiveCommand . Create ( FieldsClear );
			}

		#region Private Methods

		public static List<string> GetFromLocal ( ) => new ( )
				{
								"Admin",
								"User",
								"Unknown",
								"Incorrect",
								"Other"
						};

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

		private static ObservableCollection<string> PositionsGet ( ) {
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
		List<string> positions = InformationOfDb.Positions();
		return new ObservableCollection<string> ( positions );
			} catch ( Exception ex ) {
#if DEBUG
		string m = $"Не получилось получить роли. Пояснение: {ex.Message}";
		MsgBox . ErrorMsg ( m ) . Dialog ( typeof ( AdminWindow ) );
#endif
		return new ObservableCollection<string> ( );
			}
			}

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> ResetSomeFieldsCommand { get; }
		public ReactiveCommand<Unit , Unit> ResetAllFieldsCommand { get; }

		public ReactiveCommand<Unit , Unit> NotResetFieldsCommand { get; }

		#endregion

		#region Protected Methods

		protected override void FieldsClear ( ) {
		base . FieldsClear ( );

		PasswordConfirmation=string . Empty;
		SelectedPositionIndex=0;
		PasswordConfirmation=string . Empty;
			}

		protected override Type GetOwnerWindowType ( ) => typeof ( AdminWindow );

		protected override bool VerifyFieldsCorrectness ( ) {
		Window? w = WindowSearcher.TpFin(GetOwnerWindowType());

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return false;
			}
			}
			}
			}
			}
			}
			}

		if ( SelectedPositionIndex>Positions . Count+1 ) {
		return false;
			}

		if ( SelectedPositionIndex<0||SelectedPositionIndex>Positions . Count ) {
		MsgBox . InfoMsg ( "Выберите должность." ) . Dialog ( w );
		return false;
			}

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return false;
			}
			}
			}
			}
			}
			}
			}

		return SelectedPositionIndex!=int . MinValue
&&SelectedPositionIndex<=Positions . Count+1&&base . VerifyFieldsCorrectness ( );
			}

		protected override IUser GetUserInfo ( ) {
		IUser infUs = base.GetUserInfo();
		infUs . Position=Positions [ SelectedPositionIndex ];

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

		return infUs;
			}

		#endregion

		#region Properties

		public ObservableCollection<string> Users { get; }
		public ObservableCollection<string> Positions { get; }
		public ObservableCollection<string> Statuses { get; }

		private int _selectedPositionIndex = 0;
		public int SelectedPositionIndex {
			get => _selectedPositionIndex;
			set => this . RaiseAndSetIfChanged ( ref _selectedPositionIndex , value );
			}

		private int _selectedStatusIndex = 0;
		public int SelectedStatusIndex {
			get => _selectedStatusIndex;
			set => this . RaiseAndSetIfChanged ( ref _selectedStatusIndex , value );
			}

		private int _selectedUserIndex = 0;
		public int SelectedUserIndex {
			get => _selectedUserIndex;
			set => this . RaiseAndSetIfChanged ( ref _selectedUserIndex , value );
			}

		#endregion
		}
	}
