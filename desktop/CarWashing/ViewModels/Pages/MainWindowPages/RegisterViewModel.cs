using Avalonia . Controls;
using Avalonia . Media . Imaging;
using ReactiveUI;
using CarWashing . Infrastructure . Helpers;
using CarWashing . Infrastructure . Safety;
using CarWashing . Infrastructure . Services;
using CarWashing . Infrastructure . Services . DatabaseServices;
using CarWashing . Models . Informing;
using CarWashing . Models . Messaging;
using CarWashing . ViewModels . Base;
using CarWashing . Views;
using System;
using System . Linq;
using System . Reactive;

namespace CarWashing . ViewModels . Pages . MainWindowPages {
	public class RegisterViewModel : BaseViewModel {
		public RegisterViewModel ( string pos ) {
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

		_position=pos;

		ImageOfTheUserLoadCommand=ReactiveCommand . Create ( LoadUserImage );

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

		GenderPutCommand=ReactiveCommand . Create<string> ( SetGender );
		DoUserRegistrationCommand=ReactiveCommand . Create ( RegisterUser );

		ClosePageCommand=ReactiveCommand . Create ( ClosePage );
			}

		public RegisterViewModel ( ) : this ( User . POS_USER ) {
			}

		public RegisterViewModel ( int debug ) : this ( User . POS_USER ) {
		if ( debug == 1 )
			return;
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

		private void RegisterUser ( ) {
		if ( !VerifyFieldsCorrectness ( ) ) {
		return;
			}

		IUser ing = GetUserInfo();

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
		RegisterDbUserToDatabase . Register ( ing );
		FieldsClear ( );

		RegisteredTheUser?.Invoke ( ing );
		ClosingTheTabOrPage?.Invoke ( );
			} catch ( Exception ex ) {
		string m = "Не получилось зарегистрировать пользователя.";
#if DEBUG
		m+=$" Пояснение: {ex . Message}";
#endif
		Window? w = WindowSearcher.TpFin(GetOwnerWindowType());
		MsgBox . ErrorMsg ( m ) . Dialog ( w );
			}
			}

		private async void LoadUserImage ( ) {
		if ( WindowSearcher . TpFin ( GetOwnerWindowType ( ) ) is not Window window ) {
		return;
			}

		OpenFileDialog d = DialogHelper.OpenImage();
		string[]? ps = await d.ShowAsync(window);

		if ( ps is null||ps . Length==0 ) {
		return;
			}

		if ( !TrySetUserImage ( ps [ 0 ] ) ) {
		MsgBox . ErrorMsg ( "Не получилось открыть фото." ) . Dialog ( window );
			}
			}

		private void ClosePage ( ) => ClosingTheTabOrPage?.Invoke ( );
		public void PublicClosePage ( ) => ClosePage ( );

		private void SetGender ( string gender ) => Gender=gender;
		public void PublicSetGender ( string gender ) => SetGender(gender);

		private bool TrySetUserImage ( string path ) {
		UserImage?.Dispose ( );
		UserImage=null;

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

		try {
		var iconImg = new Bitmap(path);
		UserImage=iconImg;
		return true;
			} catch {
		return false;
			}
			}

		#endregion

		#region Properties

		private Bitmap? _iconOfUser = null;
		public Bitmap? UserImage {
			get => _iconOfUser;
			private set => this . RaiseAndSetIfChanged ( ref _iconOfUser , value );
			}

		private Bitmap? _iconOfAdmin = null;
		public Bitmap? IconOfAdmin {
			get => _iconOfAdmin;
			private set => this . RaiseAndSetIfChanged ( ref _iconOfAdmin , value );
			}

		private char? _secretChar = HIDDEN;
		public char? SecretChar {
			get => _secretChar;
			private set => this . RaiseAndSetIfChanged ( ref _secretChar , value );
			}

		private char? _pwdChr = HIDDEN;
		public char? PasswordChar {
			get => _pwdChr;
			private set => this . RaiseAndSetIfChanged ( ref _pwdChr , value );
			}

		private Bitmap? _iconUser = null;
		public Bitmap? IconUser {
			get => null;
			private set => _iconUser=value;
			}

		private string _theLogin = string.Empty;
		public string Login {
			get => _theLogin;
			set => this . RaiseAndSetIfChanged ( ref _theLogin , value );
			}

		private string _theSurname = string.Empty;
		public string Surname {
			get => _theSurname;
			set => this . RaiseAndSetIfChanged ( ref _theSurname , value );
			}

		private string _theSecretSurname = string.Empty;
		public string TheSecretSurname {
			get => _theSecretSurname;
			set => this . RaiseAndSetIfChanged ( ref _theSecretSurname , value );
			}

		private string _passwordConfirmation = string.Empty;
		public string PasswordConfirmation {
			get => _passwordConfirmation;
			set => this . RaiseAndSetIfChanged ( ref _passwordConfirmation , value );
			}

		private string _patronymic = string.Empty;
		public string Patronymic {
			get => _patronymic;
			set => this . RaiseAndSetIfChanged ( ref _patronymic , value );
			}

		private string _secretPatronymic = string.Empty;
		public string SecretPatronymic {
			get => _secretPatronymic;
			set => this . RaiseAndSetIfChanged ( ref _secretPatronymic , value );
			}

		private string _phoneNumber = string.Empty;
		public string PhoneNumber {
			get => _phoneNumber;
			set => this . RaiseAndSetIfChanged ( ref _phoneNumber , value );
			}

		private DateTime? _secretDate = null;
		public DateTime? SecretDate {
			get => _secretDate;
			set => this . RaiseAndSetIfChanged ( ref _secretDate , value );
			}

		private DateTime? _dateOfBirth = null;
		public DateTime? DateOfBirth {
			get => _dateOfBirth;
			set => this . RaiseAndSetIfChanged ( ref _dateOfBirth , value );
			}

		private string _password = string.Empty;
		public string Password {
			get => _password;
			set => this . RaiseAndSetIfChanged ( ref _password , value );
			}

		private string _ssecretPassword = string.Empty;
		public string SsecretPassword {
			get => _ssecretPassword;
			set => this . RaiseAndSetIfChanged ( ref _ssecretPassword , value );
			}

		private string _name = string.Empty;
		public string Name {
			get => _name;
			set => this . RaiseAndSetIfChanged ( ref _name , value );
			}


		private string _gender = string.Empty;
		public string Gender {
			get => _gender;
			set => this . RaiseAndSetIfChanged ( ref _gender , value );
			}

		private string _male = string.Empty;
		public string Male {
			get => _male;
			set => this . RaiseAndSetIfChanged ( ref _male , value );
			}

		private bool _showPassword = false;
		public bool ShowPassword {
			get => _showPassword;
			set {
			_=this . RaiseAndSetIfChanged ( ref _showPassword , value );
			PasswordChar=value ? null : HIDDEN;
				}
			}

		private bool _trueIsMaleGenderChecked = false;
		public bool IsMaleGenderChecked {
			get => _trueIsMaleGenderChecked;
			set => this . RaiseAndSetIfChanged ( ref _trueIsMaleGenderChecked , value );
			}

		private bool _trueIsNotMaleGenderChecked = false;
		public bool IsNotMaleGenderChecked {
			get => _trueIsNotMaleGenderChecked;
			set => this . RaiseAndSetIfChanged ( ref _trueIsNotMaleGenderChecked , value );
			}

		private bool _trueIsFemaleGenderChecked = false;
		public bool IsFemaleGenderChecked {
			get => _trueIsFemaleGenderChecked;
			set => this . RaiseAndSetIfChanged ( ref _trueIsFemaleGenderChecked , value );
			}

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> TryDoUserRegistrationCommand { get; }
		public ReactiveCommand<Unit , Unit> DoUserRegistrationCommand { get; }
		public ReactiveCommand<Unit , Unit> ContinueUserRegistrationCommand { get; }
		public ReactiveCommand<Unit , Unit> ImageOfTheUserLoadCommand { get; }
		public ReactiveCommand<Unit , Unit> ImageOfTheAdminLoadCommand { get; }
		public ReactiveCommand<string , Unit> ReGenderPutCommand { get; }
		public ReactiveCommand<string , Unit> GenderPutCommand { get; }
		public ReactiveCommand<Unit , Unit> ClosePageCommand { get; }

		#endregion

		#region Protected Methods

		protected virtual Type GetOwnerWindowType ( ) => typeof ( MainWindow );

		protected virtual IUser GetUserInfo ( ) {
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

		byte[] bytesOfIcon = UserImage is not null
								? BitmapService.BmpToBytes(UserImage)
								: Array.Empty<byte>();

		return new User ( ) {
			Name=Name ,
			Surname=Surname ,
			Gender=Gender ,
			Position=_position ,
			Patronymic=Patronymic ,
			PhoneNumber=PhoneNumber ,
			Status=User . ST_ACTIVE ,
			Money=0 ,
			Icon=bytesOfIcon ,
			Login=Login ,
			Password=Password ,
			DateOfBirth=DateOfBirth! . Value
			};
			}

		protected virtual bool VerifyFieldsCorrectness ( ) {
		var verifier = new CheckPassword(Password);
		Window? ownerWindow = WindowSearcher.TpFin(GetOwnerWindowType());

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

		if ( !verifier . IsGood ( ) ) {
		MsgBox . InfoMsg ( $"Пароль слишком слабый. {CheckPassword . REQUIREMENTS}" ) . Dialog ( ownerWindow );
		return false;
			}
		if ( Password!=PasswordConfirmation ) {
		MsgBox . InfoMsg ( "Пароли не совпадают." ) . Dialog ( ownerWindow );
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

		if ( string . IsNullOrEmpty ( Name ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести имя." ) . Dialog ( ownerWindow );
		return false;
			}
		if ( string . IsNullOrEmpty ( Surname ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести фамилию." ) . Dialog ( ownerWindow );
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

		if ( string . IsNullOrEmpty ( Patronymic ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести отчество." ) . Dialog ( ownerWindow );
		return false;
			}
		if ( PhoneNumber . Where ( t => char . IsDigit ( t ) ) . Count ( )!=NUMBER_MAX_DIGITS ) {
		MsgBox . InfoMsg ( "Необходимо ввести корректный номер телефона." ) . Dialog ( ownerWindow );
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

		if ( string . IsNullOrEmpty ( Gender ) ) {
		MsgBox . InfoMsg ( "Выберите пол." ) . Dialog ( ownerWindow );
		return false;
			}
		if ( DateOfBirth is null ) {
		MsgBox . InfoMsg ( "Необходимо ввести дату рождения." ) . Dialog ( ownerWindow );
		return false;
			}

		return true;
			}

		protected virtual void FieldsClear ( ) {
		PhoneNumber=string . Empty;
		Gender=string . Empty;
		Login=string . Empty;
		Name=string . Empty;
		Surname=string . Empty;
		Patronymic=string . Empty;
		Password=string . Empty;
		PasswordConfirmation=string . Empty;

		IsMaleGenderChecked=false;
		IsFemaleGenderChecked=false;
		ShowPassword=false;

		UserImage?.Dispose ( );
		UserImage=null;
		DateOfBirth=null;
			}

		#endregion

		#region Events

		public delegate void RegisteredTheUserHandler ( IUser user );
		public event RegisteredTheUserHandler? RegisteredTheUser;

		public delegate void ClosingTheTabOrPageHandler ( );
		public event ClosingTheTabOrPageHandler? ClosingTheTabOrPage;

		public delegate void ClosingTheeAdminTabOrPageHandler ( );
		public event ClosingTheeAdminTabOrPageHandler? ClosingTheAdminTabOrPage;

		public delegate void ClosingTheUserTabOrPageHandler ( );
		public event ClosingTheUserTabOrPageHandler? ClosingTheUserTabOrPage;

		#endregion

		#region Private Fields

		private readonly string _position;
		public readonly string _positionUser;
		public readonly string _positionAdmin;

		private const int NUMBER_MAX_DIGITS = 11;
		public const int NUMBER_REQ_DIGITS = 8;
		public const int NUMBER_MIN_DIGITS = 1;
		private const char HIDDEN = '*';

		#endregion
		}
	}
