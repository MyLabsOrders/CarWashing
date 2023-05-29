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

		#region Private Methods 

		private void SwapEditMode ( ) => IsEditModeEnabled=!IsEditModeEnabled;

		private void InformationSave ( ) {
		if ( !TryCorrectnessCheck ( ) ) {
		return;
			}

		IUser newUserInfo = InformationAboutUserGet();

		try {
		EditDbUser . Edit ( _user , newUserInfo );
			} catch ( Exception ex ) {
		string message = "Не удалось сохранить изменения.";
#if DEBUG
		message+=$"Причина: {ex . Message}";
#endif
		Window? window = WindowSearcher.FindWindowByType(WindowGetType());
		MsgBox . ErrorMsg ( message ) . Dialog ( window );
		return;
			}

		newUserInfo . CopyToOtherUser ( _user );
		UpdatedTheInformation?.Invoke ( );

		IsEditModeEnabled=false;
			}

		private async void ImageChange ( ) {
		if ( WindowSearcher . FindWindowByType ( WindowGetType ( ) ) is not Window window ) {
		return;
			}

		OpenFileDialog dialog = DialogHelper.OpenImage();
		string[]? paths = await dialog.ShowAsync(window);

		if ( paths is null||paths . Length==0 ) {
		return;
			}

		if ( !TryImagePut ( paths [ 0 ] ) ) {
		MsgBox . ErrorMsg ( "Не удалось открыть фото." ) . Dialog ( window );
			}
			}

		private async void BalanceChange ( ) {
		var topUpBalanceButton = new ButtonDefinition()
						{
			IsCancel = false,
			IsDefault = true,
			Name = "Поплнить"
			};

		var cancelButton = new ButtonDefinition()
						{
			IsCancel = true,
			IsDefault = false,
			Name = "Отмена"
			};

		MessageBox.Avalonia.BaseWindows.Base.IMsBoxWindow<MessageWindowResultDTO> inputWindow = MessageBoxManager.GetMessageBoxInputWindow(new MessageBoxInputParams()
						{
			MinWidth = 350,
			Icon = Icon.Plus,

			CanResize = false,
			ShowInCenter = true,
			Markdown = true,

			InputDefaultValue = "1000",
			ContentTitle = "Пополнить баланс",
			ContentMessage = "Пополнить баланс",
			ContentHeader = $"Текущий баланс: {_user.Money}",

			ButtonDefinitions = new[] { topUpBalanceButton, cancelButton },
			});

		Window? userWindow = WindowSearcher.User();

		if ( userWindow is null ) {
		return;
			}

		MessageWindowResultDTO result = await inputWindow.ShowDialog(userWindow);

		string pressedButton = result.Button;
		string moneyPresenter = result.Message;

		if ( pressedButton!=topUpBalanceButton . Name ) {
		return;
			}

		if ( !double . TryParse ( moneyPresenter , out double money ) ) {
		MsgBox . ErrorMsg ( "Вы ввели некорректное значение." ) . Dialog ( userWindow );
		return;
			}

		try {
		DatabaseUserCash . IncreaseMoney ( _user , money );
		_user . Money=DatabaseUserCash . CheckBalace ( _user );
			} catch ( Exception ex ) {
		string message = "Не удалось пополнить баланс.";
#if DEBUG
		message+=$" Причина: {ex . Message}";
#endif
		MsgBox . ErrorMsg ( message ) . Dialog ( userWindow );
			}
			}

		private bool TryImagePut ( string path ) {
		UserImage?.Dispose ( );
		UserImage=null;

		try {
		var image = new Bitmap(path);
		UserImage=image;
		return true;
			} catch {
		return false;
			}
			}

		private void GenderPut ( string gender ) => Gender=gender;

		#endregion

		#region Properties

		private bool _trueIsEditModeEnabled = false;
		public bool IsEditModeEnabled {
			get => _trueIsEditModeEnabled;
			private set => this . RaiseAndSetIfChanged ( ref _trueIsEditModeEnabled , value );
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

		#endregion

		#region Protected Fields

		protected IUser _user;
		protected const int MAX_DIGITS_FOR_NUMBER = 11;

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
		Window? ownerWindow = WindowSearcher.FindWindowByType(WindowGetType());

		if ( string . IsNullOrEmpty ( Login ) ) {
		MsgBox . InfoMsg ( "Введите логин." ) . Dialog ( ownerWindow );
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
		MsgBox . InfoMsg ( "Введите пароль." ) . Dialog ( ownerWindow );
		return false;
			}
		if ( string . IsNullOrEmpty ( Name ) ) {
		MsgBox . InfoMsg ( "Введите имя." ) . Dialog ( ownerWindow );
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
		MsgBox . InfoMsg ( "Введите фамилию." ) . Dialog ( ownerWindow );
		return false;
			}
		if ( string . IsNullOrEmpty ( Patronymic ) ) {
		MsgBox . InfoMsg ( "Введите отчество." ) . Dialog ( ownerWindow );
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
		MsgBox . InfoMsg ( "Введите корректный номер телефона." ) . Dialog ( ownerWindow );
		return false;
			}
		if ( string . IsNullOrEmpty ( Gender ) ) {
		MsgBox . InfoMsg ( "Выберите пол." ) . Dialog ( ownerWindow );
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
		MsgBox . InfoMsg ( "Введите дату рождения." ) . Dialog ( ownerWindow );
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
		Password=userInfo . Password;
		Name=userInfo . Name;
		Surname=userInfo . Surname;
		Patronymic=userInfo . Patronymic;
		PhoneNumber=userInfo . PhoneNumber;
		Gender=userInfo . Gender;
		Position=userInfo . Position;
		DateOfBirth=userInfo . DateOfBirth;

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


		if ( Gender==UserInfo . MALE ) {
		IsMaleGenderChecked=true;
			} else if ( Gender==UserInfo . FEMALE ) {
		IsFemaleGenderChecked=true;
			}
			}

		protected virtual IUser InformationAboutUserGet ( ) {
		byte[] userImageBytes = UserImage is not null
							 ? BitmapService.BmpToBytes(UserImage)
							 : Array.Empty<byte>();

		var userInfo = new UserInfo();
		_user . CopyToOtherUser ( userInfo );

		userInfo . Login=Login;
		userInfo . Password=Password;
		userInfo . Name=Name;
		userInfo . Surname=Surname;
		userInfo . Patronymic=Patronymic;
		userInfo . PhoneNumber=PhoneNumber;
		userInfo . Gender=Gender;
		userInfo . Position=Position;
		userInfo . Icon=userImageBytes;
		userInfo . DateOfBirth=DateOfBirth! . Value;


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

		#region Events

		public delegate void UpdatedTheInformationHandler ( );
		public event UpdatedTheInformationHandler? UpdatedTheInformation;

		#endregion
		}
	}
