using Avalonia . Controls;
using Avalonia . Media . Imaging;
using MessageBox . Avalonia;
using MessageBox . Avalonia . DTO;
using MessageBox . Avalonia . Enums;
using MessageBox . Avalonia . Models;
using ReactiveUI;
using RentDesktop . Infrastructure . Helpers;
using RentDesktop . Infrastructure . Services;
using RentDesktop . Infrastructure . Services . DatabaseServices;
using RentDesktop . Models . Informing;
using RentDesktop . Models . Messaging;
using RentDesktop . ViewModels . Base;
using RentDesktop . Views;
using System;
using System . Linq;
using System . Reactive;

namespace RentDesktop . ViewModels . Pages . UserWindowPages {
	public class UserProfileViewModel : BaseViewModel {
		public UserProfileViewModel ( IUser user ) {
		ImageSwitchCommand=ReactiveCommand . Create ( ImageChange );
		ImagePutCommand=ReactiveCommand . Create ( ImageChange );
		ImageChangeCommand=ReactiveCommand . Create ( ImageChange );
		BalanceChangeCommand=ReactiveCommand . Create ( BalanceChange );
		ModeChangeCommand=ReactiveCommand . Create ( SwapEditMode );
		GenderPutCommand=ReactiveCommand . Create<string> ( GenderPut );
		InfoSavingCommand=ReactiveCommand . Create ( InformationSave );

		_user=user;
		InformationSave ( user );
			}

		public UserProfileViewModel ( IUser user, int debug ) {
		if ( debug==1 )
			user=new User ( );
			}

		#region Private Methods 

		public void PublicSwapEditMode ( ) => SwapEditMode ( );
		private void SwapEditMode ( ) => IsEditModeEnabled=!IsEditModeEnabled;

		private void InformationSave ( ) {
		if ( !TryCorrectnessCheck ( ) ) {
		return;
			}

		IUser newIng = InformationAboutUserGet();

		try {
		EditDbUser . Edit ( _user , newIng );
			} catch ( Exception ex ) {
		string m = "Не получилось сохранить изменения.";
#if DEBUG
		m+=$"Пояснение: {ex . Message}";
#endif
		Window? w = WindowSearcher.TpFin(WindowGetType());
		MsgBox . ErrorMsg ( m ) . Dialog ( w );
		return;
			}

		newIng . CopyToOtherUser ( _user );
		UpdatedTheInformation?.Invoke ( );

		IsEditModeEnabled=false;
			}

		private async void ImageChange ( ) {
		if ( WindowSearcher . TpFin ( WindowGetType ( ) ) is not Window window ) {
		return;
			}

		OpenFileDialog dg = DialogHelper.OpenImage();
		string[]? ps = await dg.ShowAsync(window);

		if ( ps is null||ps . Length==0 ) {
		return;
			}

		if ( !TryImagePut ( ps [ 0 ] ) ) {
		MsgBox . ErrorMsg ( "Не получилось открыть фото." ) . Dialog ( window );
			}
			}

		private async void BalanceChange ( ) {
		var topUpBalanceButton = new ButtonDefinition()
						{
			IsCancel = false,
			IsDefault = true,
			Name = "Пополнить"
			};

		var cancelButton = new ButtonDefinition()
						{
			IsCancel = true,
			IsDefault = false,
			Name = "Отмена"
			};

		MessageBox.Avalonia.BaseWindows.Base.IMsBoxWindow<MessageWindowResultDTO> win = MessageBoxManager.GetMessageBoxInputWindow(new MessageBoxInputParams()
						{
			MinWidth = 350,
			ContentTitle = "Пополнить баланс",
			ContentMessage = "Пополнить баланс",

			CanResize = false,
			ShowInCenter = true,
			Markdown = true,

			InputDefaultValue = "1000",
			ContentHeader = $"Текущий баланс: {_user.Money}",

			ButtonDefinitions = new[] { topUpBalanceButton, cancelButton },
			Icon = Icon.Plus,
			});

		Window? uW = WindowSearcher.User();

		if ( uW is null ) {
		return;
			}

		MessageWindowResultDTO r = await win.ShowDialog(uW);

		string pr = r.Button;
		string mn = r.Message;

		if ( pr!=topUpBalanceButton . Name ) {
		return;
			}

		if ( !double . TryParse ( mn , out double money ) ) {
		MsgBox . ErrorMsg ( "Вы ввели некорректное значение." ) . Dialog ( uW );
		return;
			}

		try {
		DatabaseUserCash . IncreaseMoney ( _user , money );
		_user . Money=DatabaseUserCash . CheckBalace ( _user );
			} catch ( Exception ex ) {
		string message = "Не получилось пополнить баланс.";
#if DEBUG
		message+=$" Пояснение: {ex . Message}";
#endif
		MsgBox . ErrorMsg ( message ) . Dialog ( uW );
			}
			}

		private bool TryImagePut ( string path ) {
		UserImage?.Dispose ( );
		UserImage=null;

		try {
		var im = new Bitmap(path);
		UserImage=im;
		return true;
			} catch {
		return false;
			}
			}

		public void PublicGenderPut ( string gender ) => GenderPut(gender);
		private void GenderPut ( string gender ) => Gender=gender;

		#endregion

		#region Properties

		private bool _trueIsEditModeEnabled = false;
		public bool IsEditModeEnabled {
			get => _trueIsEditModeEnabled;
			private set => this . RaiseAndSetIfChanged ( ref _trueIsEditModeEnabled , value );
			}

		private bool _trueIsNotEditModeEnabled = false;
		public bool IsNotEditModeEnabled {
			get => _trueIsNotEditModeEnabled;
			private set => this . RaiseAndSetIfChanged ( ref _trueIsNotEditModeEnabled , value );
			}

		private string _theName = string.Empty;
		public string Name {
			get => _theName;
			set => this . RaiseAndSetIfChanged ( ref _theName , value );
			}

		private string _theSurname = string.Empty;
		public string Surname {
			get => _theSurname;
			set => this . RaiseAndSetIfChanged ( ref _theSurname , value );
			}

		private string _thePassword = string.Empty;
		public string Password {
			get => _thePassword;
			set => this . RaiseAndSetIfChanged ( ref _thePassword , value );
			}

		private string _theSecretPatronymic = string.Empty;
		public string SecretPatronymic {
			get => _theSecretPatronymic;
			set => this . RaiseAndSetIfChanged ( ref _theSecretPatronymic , value );
			}

		private string _thePatronymic = string.Empty;
		public string Patronymic {
			get => _thePatronymic;
			set => this . RaiseAndSetIfChanged ( ref _thePatronymic , value );
			}

		private DateTime? _theDateOfBirth = null;
		public DateTime? DateOfBirth {
			get => _theDateOfBirth;
			set => this . RaiseAndSetIfChanged ( ref _theDateOfBirth , value );
			}

		private DateTime? _theHiddenDateOfBirth = null;
		public DateTime? HiddenDateOfBirth {
			get => _theHiddenDateOfBirth;
			set => this . RaiseAndSetIfChanged ( ref _theHiddenDateOfBirth , value );
			}

		private string _theGender = string.Empty;
		public string Gender {
			get => _theGender;
			set => this . RaiseAndSetIfChanged ( ref _theGender , value );
			}

		private string _thePhoneNumber = string.Empty;
		public string PhoneNumber {
			get => _thePhoneNumber;
			set => this . RaiseAndSetIfChanged ( ref _thePhoneNumber , value );
			}

		private string _theHiddenPhoneNumber = string.Empty;
		public string TheHiddenPhoneNumber {
			get => _theHiddenPhoneNumber;
			set => this . RaiseAndSetIfChanged ( ref _theHiddenPhoneNumber , value );
			}

		private string _thePosition = string.Empty;
		public string Position {
			get => _thePosition;
			private set => this . RaiseAndSetIfChanged ( ref _thePosition , value );
			}

		private bool _trueIsFemaleGenderChecked = false;
		public bool IsFemaleGenderChecked {
			get => _trueIsFemaleGenderChecked;
			set => this . RaiseAndSetIfChanged ( ref _trueIsFemaleGenderChecked , value );
			}

		private Bitmap? _theUserImage = null;
		public Bitmap? UserImage {
			get => _theUserImage;
			private set => this . RaiseAndSetIfChanged ( ref _theUserImage , value );
			}

		private Bitmap? _theUserImageCopy = null;
		public Bitmap? TheUserImageCopy {
			get => _theUserImageCopy;
			private set => this . RaiseAndSetIfChanged ( ref _theUserImageCopy , value );
			}

		private bool _trueIsMaleGenderChecked = false;
		public bool IsMaleGenderChecked {
			get => _trueIsMaleGenderChecked;
			set => this . RaiseAndSetIfChanged ( ref _trueIsMaleGenderChecked , value );
			}


		private string _theLogin = string.Empty;
		public string Login {
			get => _theLogin;
			set => this . RaiseAndSetIfChanged ( ref _theLogin , value );
			}

		private string _theSecretLogin = string.Empty;
		public string SecretLogin {
			get => _theSecretLogin;
			set => this . RaiseAndSetIfChanged ( ref _theSecretLogin , value );
			}

		#endregion

		#region Protected Fields

		protected IUser _user;
		protected IUser _admin;
		protected IUser _identity;
		protected const int MAX_DIGITS_FOR_NUMBER = 11;
		protected const int MIN_DIGITS_FOR_NUMBER = 1;
		protected const int REQ_DIGITS_FOR_NUMBER = 8;

		#endregion

		#region Commands

		public ReactiveCommand<Unit , Unit> ModeSwitchCommand { get; }
		public ReactiveCommand<Unit , Unit> ModeChangeCommand { get; }
		public ReactiveCommand<Unit , Unit> ImagePutCommand { get; }
		public ReactiveCommand<Unit , Unit> ImageChangeCommand { get; }
		public ReactiveCommand<Unit , Unit> ImageSwitchCommand { get; }
		public ReactiveCommand<Unit , Unit> BalanceChangeCommand { get; }
		public ReactiveCommand<Unit , Unit> InfoSavingCommand { get; }
		public ReactiveCommand<Unit , Unit> InfoResettingCommand { get; }
		public ReactiveCommand<string , Unit> GenderPutCommand { get; }
		public ReactiveCommand<string , Unit> GenderResetCommand { get; }

		#endregion

		#region Protected Methods

		protected virtual bool TryCorrectnessCheck ( ) {
		Window? w = WindowSearcher.TpFin(WindowGetType());

		if ( string . IsNullOrEmpty ( Login ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести логин." ) . Dialog ( w );
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

		if ( string . IsNullOrEmpty ( Password ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести пароль." ) . Dialog ( w );
		return false;
			}
		if ( string . IsNullOrEmpty ( Name ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести имя." ) . Dialog ( w );
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

		if ( string . IsNullOrEmpty ( Surname ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести фамилию." ) . Dialog ( w );
		return false;
			}
		if ( string . IsNullOrEmpty ( Patronymic ) ) {
		MsgBox . InfoMsg ( "Необходимо ввести отчество." ) . Dialog ( w );
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

		if ( PhoneNumber . Where ( t => char . IsDigit ( t ) ) . Count ( )!=MAX_DIGITS_FOR_NUMBER ) {
		MsgBox . InfoMsg ( "Необходимо ввести корректный номер телефона." ) . Dialog ( w );
		return false;
			}
		if ( string . IsNullOrEmpty ( Gender ) ) {
		MsgBox . InfoMsg ( "Выберите пол." ) . Dialog ( w );
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

		if ( DateOfBirth is null ) {
		MsgBox . InfoMsg ( "Необходимо ввести дату рождения." ) . Dialog ( w );
		return false;
			}

		return true;
			}

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

		protected virtual Type WindowGetType ( ) => typeof ( UserWindow );

		protected virtual void InformationSave ( IUser userInfo ) {
		Login=userInfo . Login;
		Name=userInfo . Name;
		Surname=userInfo . Surname;
		Position=userInfo . Position;
		DateOfBirth=userInfo . DateOfBirth;
		Password=userInfo . Password;
		Patronymic=userInfo . Patronymic;
		PhoneNumber=userInfo . PhoneNumber;
		Gender=userInfo . Gender;

		UserImage?.Dispose ( );

		UserImage=userInfo . Icon . Length>0
				? BitmapService . BytesToBmp ( userInfo . Icon )
				: null;

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


		if ( Gender==User . MALE ) {
		IsMaleGenderChecked=true;
			} else if ( Gender==User . FEMALE ) {
		IsFemaleGenderChecked=true;
			}
			}

		protected virtual IUser InformationAboutUserGet ( ) {
		byte[] userImageBytes = UserImage is not null
							 ? BitmapService.BmpToBytes(UserImage)
							 : Array.Empty<byte>();

		var ing = new User();
		_user . CopyToOtherUser ( ing );

		ing . Password=Password;
		ing . PhoneNumber=PhoneNumber;
		ing . Gender=Gender;
		ing . Position=Position;
		ing . Name=Name;
		ing . Surname=Surname;
		ing . Patronymic=Patronymic;
		ing . Icon=userImageBytes;
		ing . DateOfBirth=DateOfBirth! . Value;
		ing . Login=Login;


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

		return ing;
			}

		#endregion

		#region Events

		public delegate void UpdatedTheAdminInformationHandler ( );
		public event UpdatedTheAdminInformationHandler? UpdatedTheAdminInformation;

		public delegate void UpdatedTheInformationHandler ( );
		public event UpdatedTheInformationHandler? UpdatedTheInformation;

		#endregion
		}
	}
