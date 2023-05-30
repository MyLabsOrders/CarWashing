using Avalonia . Threading;
using ReactiveUI;
using CarWashing . Infrastructure . Helpers;
using CarWashing . Infrastructure . Services;
using CarWashing . Infrastructure . Services . DatabaseServices;
using CarWashing . Models . Informing;
using CarWashing . Models . Messaging;
using CarWashing . Models . Security;
using CarWashing . ViewModels . Base;
using CarWashing . Views;
using System;
using System . Reactive;
using System . Threading . Tasks;

namespace CarWashing . ViewModels . Pages . MainWindowPages {
	public class LoginViewModel : BaseViewModel {
		public LoginViewModel ( ) {
		RegisterTabOpenCommand=ReactiveCommand . Create ( RegisterOpen );
		AppExitCommand=ReactiveCommand . Create ( ProgramExit );

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

		LoginLoadCommand=ReactiveCommand . Create ( LoginLoadInformation );
		CaptchaRefreshCommand=ReactiveCommand . Create ( CaptchaRefresh );

		LogToSystemCommand=ReactiveCommand . Create ( LogToSystem );
			}

		public LoginViewModel (int debug ) {
		debug+=1;
			}

		#region Private Methods

		private void CaptchaRefresh ( ) => Captcha . UpdateText ( );
		public void RefreshCaptchaRefresh ( ) => Captcha . UpdateText ( );


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

		private void SaveInfo ( ) => UserInfoSaveService . TrySave ( Login , Password );
		public void SavingInfo ( ) => SaveInfo();

		private static void ClearInfo ( ) => UserInfoSaveService . TryEmpty ( );
		public static void EmptyInfo ( ) => UserInfoSaveService . TryEmpty ( );

		private void LogToSystem ( ) {
		if ( !CorrectnessCheck ( ) ) {
		return;
			}

		IUser ing;

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
		ing=LoginToDatabase . TryLogin ( Login , Password );
			} catch ( Exception ex ) {
		string m = "Не получилось войти в систему.";
#if DEBUG
		m+=$" Пояснение: {ex . Message}";
#endif
		MsgBox . ErrorMsg ( m ) . Dialog ( typeof ( MainWindow ) );
		return;
			}

		if ( RememberUser ) {
		SaveInfo ( );
			} else {
		ClearInfo ( );
			}

		FieldsClear ( RememberUser );

		if ( ing . IsTheAdmin ( ) ) {
		var vm = new AdminWindowViewModel(ing);
		var aW = new AdminWindow() { DataContext = vm };

		aW . Show ( );
			} else {
		var vmU = new UserWindowViewModel(ing);
		var uW = new UserWindow() { DataContext = vmU };

		uW . Show ( );
			}

		Task . Delay ( MILLISECONDS_FOR_HIDE_MAIN_WINDOW ) . ContinueWith ( t => {
		Dispatcher . UIThread . Post ( WindowInteraction . MainHide );
		} );
			}

		private void ProgramExit ( ) => WindowInteraction . MainClose ( );
		public void PublicProgramExit ( ) => WindowInteraction . MainClose ( );
		internal void InternalProgramExit ( ) => WindowInteraction . MainClose ( );

		private void LoginLoadInformation ( ) {
		if ( UserInfoSaveService . TryLoad ( out (string Login, string Password) info ) ) {
		Login=info . Login;
		Password=info . Password;
			}
			}

		private bool CorrectnessCheck ( ) {
		Avalonia.Controls.Window? ownerWindow = WindowSearcher.Main();

		if ( string . IsNullOrEmpty ( Password ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести пароль." ) . Dialog ( ownerWindow );
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

		if ( string . IsNullOrEmpty ( Login ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести логин." ) . Dialog ( ownerWindow );
		return false;
			}
		if ( UserCaptchaText!=Captcha . Text ) {
		MsgBox . InfoMsg ( "Текст с картинки введен неверно." ) . Dialog ( ownerWindow );

		CaptchaRefresh ( );
		UserCaptchaText=string . Empty;

		return false;
			}

		return true;
			}

		private void RegisterOpen ( ) => OpenRegisterEvent?.Invoke ( );
		public void PublicRegisterOpen ( ) => RegisterOpen ( );
		internal void InternalRegisterOpen ( ) => RegisterOpen ( );

		private void FieldsClear ( bool leaveLoginInfo = false ) {
		if ( !leaveLoginInfo ) {
		RememberUser=true;
		Login=string . Empty;
		Password=string . Empty;
			}

		ShowPassword=false;
		UserCaptchaText=string . Empty;

		CaptchaRefresh ( );
			}

		#endregion

		#region Properties

		private bool _rememberUser = true;
		public bool RememberUser {
			get => _rememberUser;
			set => this . RaiseAndSetIfChanged ( ref _rememberUser , value );
			}

		private bool _notrememberUser = true;
		public bool NotRememberUser {
			get => _notrememberUser;
			set => this . RaiseAndSetIfChanged ( ref _notrememberUser , value );
			}

		public ISecret Captcha { get; } = new Secret ( );

		private string _secretLogin = string.Empty;
		public string SecretLogin {
			get => _secretLogin;
			set => this . RaiseAndSetIfChanged ( ref _secretLogin , value );
			}

		private string _login = string.Empty;
		public string Login {
			get => _login;
			set => this . RaiseAndSetIfChanged ( ref _login , value );
			}

		private string _savedThelogin = string.Empty;
		public string SavedTheLogin {
			get => _savedThelogin;
			set => this . RaiseAndSetIfChanged ( ref _savedThelogin , value );
			}

		private char? _passwordChar = HIDDEN;
		public char? PasswordChar {
			get => _passwordChar;
			private set => this . RaiseAndSetIfChanged ( ref _passwordChar , value );
			}

		private bool _showPassword = false;
		public bool ShowPassword {
			get => _showPassword;
			set {
			_=this . RaiseAndSetIfChanged ( ref _showPassword , value );
			PasswordChar=value ? null : HIDDEN;
				}
			}

		private string _password = string.Empty;
		public string Password {
			get => _password;
			set => this . RaiseAndSetIfChanged ( ref _password , value );
			}

		private string _secretPpassword = string.Empty;
		public string SecretPpassword {
			get => _secretPpassword;
			set => this . RaiseAndSetIfChanged ( ref _secretPpassword , value );
			}

		private string _userCaptchaText = string.Empty;
		public string UserCaptchaText {
			get => _userCaptchaText;
			set => this . RaiseAndSetIfChanged ( ref _userCaptchaText , value );
			}

		#endregion

		#region Events

		public delegate void OpenRegisterEventHandler ( );
		public event OpenRegisterEventHandler? OpenRegisterEvent;

		public delegate void OpenCartEventHandler ( );
		public event OpenCartEventHandler? OpenCartEvent;

		public delegate void OpenMainEventHandler ( );
		public event OpenMainEventHandler? OpenMainEvent;

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> LoginLoadCommand { get; }
		public ReactiveCommand<Unit , Unit> LoginClearCommand { get; }
		public ReactiveCommand<Unit , Unit> ResetLoginInfoCommand { get; }
		public ReactiveCommand<Unit , Unit> CaptchaRefreshCommand { get; }
		public ReactiveCommand<Unit , Unit> ClearLoginInfoCommand { get; }
		public ReactiveCommand<Unit , Unit> EmptyLoginInfoCommand { get; }
		public ReactiveCommand<Unit , Unit> LogToSystemCommand { get; }
		public ReactiveCommand<Unit , Unit> LogToSystemIncognitoCommand { get; }
		public ReactiveCommand<Unit , Unit> RegisterTabOpenCommand { get; }
		public ReactiveCommand<Unit , Unit> FindLoginInfoCommand { get; }
		public ReactiveCommand<Unit , Unit> AppExitCommand { get; }

		#endregion

		#region Private fields

		private const char HIDDEN = '*';
		private const int MILLISECONDS_FOR_HIDE_MAIN_WINDOW = 10;
		public const int MILLISECONDS_FOR_HIDE_USER_WINDOW = 20;
		public const int MILLISECONDS_FOR_HIDE_ADMIN_WINDOW = 30;
		public const int MILLISECONDS_FOR_HIDE_SECRET_WINDOW = 40;

		#endregion
		}
	}
