using Avalonia . Threading;
using ReactiveUI;
using RentDesktop . Infrastructure . Helpers;
using RentDesktop . Infrastructure . Services;
using RentDesktop . Infrastructure . Services . DatabaseServices;
using RentDesktop . Models . Informing;
using RentDesktop . Models . Messaging;
using RentDesktop . Models . Security;
using RentDesktop . ViewModels . Base;
using RentDesktop . Views;
using System;
using System . Reactive;
using System . Threading . Tasks;

namespace RentDesktop . ViewModels . Pages . MainWindowPages {
	public class LoginViewModel : BaseViewModel {
		public LoginViewModel ( ) {
		RegisterTabOpenCommand=ReactiveCommand . Create ( RegisterOpen );
		AppExitCommand=ReactiveCommand . Create ( ProgramExit );

		LoginLoadCommand=ReactiveCommand . Create ( LoginLoadInformation );
		CaptchaRefreshCommand=ReactiveCommand . Create ( CaptchaRefresh );

		LogToSystemCommand=ReactiveCommand . Create ( LogToSystem );
			}

		#region Private Methods

		private void CaptchaRefresh ( ) => Captcha . UpdateText ( );


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

		private static void ClearInfo ( ) => UserInfoSaveService . TryEmpty ( );

		private void LogToSystem ( ) {
		if ( !CorrectnessCheck ( ) ) {
		return;
			}

		IUser userInfo;

		try {
		userInfo=LoginToDatabase . TryLogin ( Login , Password );
			} catch ( Exception ex ) {
		string message = "Не удалось войти в систему.";
#if DEBUG
		message+=$" Причина: {ex . Message}";
#endif
		MsgBox . ErrorMsg ( message ) . Dialog ( typeof ( MainWindow ) );
		return;
			}

		if ( RememberUser ) {
		SaveInfo ( );
			} else {
		ClearInfo ( );
			}

		FieldsClear ( RememberUser );

		if ( userInfo . IsTheAdmin ( ) ) {
		var viewModel = new AdminWindowViewModel(userInfo);
		var adminWindow = new AdminWindow() { DataContext = viewModel };

		adminWindow . Show ( );
			} else {
		var viewModel = new UserWindowViewModel(userInfo);
		var userWindow = new UserWindow() { DataContext = viewModel };

		userWindow . Show ( );
			}

		_=Task . Delay ( MILLISECONDS_FOR_HIDE_MAIN_WINDOW ) . ContinueWith ( t => {
		Dispatcher . UIThread . Post ( WindowInteraction . HideMainWindow );
		} );
			}

		private void ProgramExit ( ) => WindowInteraction . CloseMainWindow ( );

		private void LoginLoadInformation ( ) {
		if ( UserInfoSaveService . TryLoad ( out (string Login, string Password) info ) ) {
		Login=info . Login;
		Password=info . Password;
			}
			}

		private bool CorrectnessCheck ( ) {
		Avalonia.Controls.Window? ownerWindow = WindowSearcher.Main();

		if ( string . IsNullOrEmpty ( Password ) ) {
		MsgBox . InfoMsg ( "Введите пароль." ) . Dialog ( ownerWindow );
		return false;
			}
		if ( string . IsNullOrEmpty ( Login ) ) {
		MsgBox . InfoMsg ( "Введите логин." ) . Dialog ( ownerWindow );
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

		public ISecret Captcha { get; } = new Secret ( );

		private string _login = string.Empty;
		public string Login {
			get => _login;
			set => this . RaiseAndSetIfChanged ( ref _login , value );
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

		private string _userCaptchaText = string.Empty;
		public string UserCaptchaText {
			get => _userCaptchaText;
			set => this . RaiseAndSetIfChanged ( ref _userCaptchaText , value );
			}

		#endregion

		#region Events

		public delegate void OpenRegisterEventHandler ( );
		public event OpenRegisterEventHandler? OpenRegisterEvent;

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> LoginLoadCommand { get; }
		public ReactiveCommand<Unit , Unit> ResetLoginInfoCommand { get; }
		public ReactiveCommand<Unit , Unit> CaptchaRefreshCommand { get; }
		public ReactiveCommand<Unit , Unit> ClearLoginInfoCommand { get; }
		public ReactiveCommand<Unit , Unit> LogToSystemCommand { get; }
		public ReactiveCommand<Unit , Unit> RegisterTabOpenCommand { get; }
		public ReactiveCommand<Unit , Unit> FindLoginInfoCommand { get; }
		public ReactiveCommand<Unit , Unit> AppExitCommand { get; }

		#endregion

		#region Private fields

		private const char HIDDEN = '*';
		private const int MILLISECONDS_FOR_HIDE_MAIN_WINDOW = 10;

		#endregion
		}
	}
