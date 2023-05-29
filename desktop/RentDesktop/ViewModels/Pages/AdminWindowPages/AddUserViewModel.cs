using Avalonia . Controls;
using ReactiveUI;
using RentDesktop . Infrastructure . Helpers;
using RentDesktop . Infrastructure . Services . DatabaseServices;
using RentDesktop . Models . Informing;
using RentDesktop . Models . Messaging;
using RentDesktop . ViewModels . Pages . MainWindowPages;
using RentDesktop . Views;
using System;
using System . Collections . Generic;
using System . Collections . ObjectModel;
using System . Reactive;

namespace RentDesktop . ViewModels . Pages . AdminWindowPages {
	public class AddUserViewModel : RegisterViewModel {
		public AddUserViewModel ( ) : base ( ) {
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
		string message = $"Не удалось получить роли. Причина: {ex.Message}";
		MsgBox . ErrorMsg ( message ) . Dialog ( typeof ( AdminWindow ) );
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
		Window? window = WindowSearcher.FindWindowByType(GetOwnerWindowType());

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
		MsgBox . InfoMsg ( "Выберите должность." ) . Dialog ( window );
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
		IUser userInfo = base.GetUserInfo();
		userInfo . Position=Positions [ SelectedPositionIndex ];

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

		return userInfo;
			}

		#endregion

		#region Properties

		public ObservableCollection<string> Positions { get; }

		private int _selectedPositionIndex = 0;
		public int SelectedPositionIndex {
			get => _selectedPositionIndex;
			set => this . RaiseAndSetIfChanged ( ref _selectedPositionIndex , value );
			}

		#endregion
		}
	}
