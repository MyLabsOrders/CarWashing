using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ReactiveUI;
using RentDesktop.Infrastructure.Helpers;
using RentDesktop.Infrastructure.Safety;
using RentDesktop.Infrastructure.Services;
using RentDesktop.Infrastructure.Services.DatabaseServices;
using RentDesktop.Models.Messaging;
using RentDesktop.Models.Informing;
using RentDesktop.ViewModels.Base;
using RentDesktop.Views;
using System;
using System.Linq;
using System.Reactive;

namespace RentDesktop.ViewModels.Pages.MainWindowPages
{
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel(string pos)
        {
            _position = pos;

            ImageOfTheUserLoadCommand = ReactiveCommand.Create(LoadUserImage);

            GenderPutCommand = ReactiveCommand.Create<string>(SetGender);
            DoUserRegistrationCommand = ReactiveCommand.Create(RegisterUser);

            ClosePageCommand = ReactiveCommand.Create(ClosePage);
        }

        public RegisterViewModel() : this(UserInfo.POS_USER)
        {
        }

        #region Private Methods

        private void RegisterUser()
        {
            if (!VerifyFieldsCorrectness())
                return;

            IUser userInfo = GetUserInfo();

            try
            {
                RegisterDbUserToDatabase.Register(userInfo);
                FieldsClear();

                RegisteredTheUser?.Invoke(userInfo);
                ClosingTheTabOrPage?.Invoke();
            }
            catch (Exception ex)
            {
                string message = "Не удалось зарегистрировать пользователя.";
#if DEBUG
                message += $" Причина: {ex.Message}";
#endif
                Window? window = WindowSearcher.FindWindowByType(GetOwnerWindowType());
                MsgBox.ErrorMsg(message).Dialog(window);
            }
        }

        private async void LoadUserImage()
        {
            if (WindowSearcher.FindWindowByType(GetOwnerWindowType()) is not Window window)
                return;

            OpenFileDialog dialog = DialogHelper.OpenImage();
            string[]? paths = await dialog.ShowAsync(window);

            if (paths is null || paths.Length == 0)
                return;

            if (!TrySetUserImage(paths[0]))
                MsgBox.ErrorMsg("Не удалось открыть фото.").Dialog(window);
        }

        private void ClosePage()
        {
            ClosingTheTabOrPage?.Invoke();
        }

        private void SetGender(string gender)
        {
            Gender = gender;
        }

        private bool TrySetUserImage(string path)
        {
            UserImage?.Dispose();
            UserImage = null;

            try
            {
                var image = new Bitmap(path);
                UserImage = image;
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Properties

        private Bitmap? _iconOfUser = null;
        public Bitmap? UserImage
        {
            get => _iconOfUser;
            private set => this.RaiseAndSetIfChanged(ref _iconOfUser, value);
        }

        private char? _pwdChr = HIDDEN;
        public char? PasswordChar
        {
            get => _pwdChr;
            private set => this.RaiseAndSetIfChanged(ref _pwdChr, value);
        }

        private Bitmap? _iconUser = null;
        public Bitmap? IconUser
        {
            get => null;
            private set => _iconUser = value;
        }

        private string _theLogin = string.Empty;
        public string Login
        {
            get => _theLogin;
            set => this.RaiseAndSetIfChanged(ref _theLogin, value);
        }

        private string _theSurname = string.Empty;
        public string Surname
        {
            get => _theSurname;
            set => this.RaiseAndSetIfChanged(ref _theSurname, value);
        }

        private string _passwordConfirmation = string.Empty;
        public string PasswordConfirmation
        {
            get => _passwordConfirmation;
            set => this.RaiseAndSetIfChanged(ref _passwordConfirmation, value);
        }

        private string _patronymic = string.Empty;
        public string Patronymic
        {
            get => _patronymic;
            set => this.RaiseAndSetIfChanged(ref _patronymic, value);
        }

        private string _phoneNumber = string.Empty;
        public string PhoneNumber
        {
            get => _phoneNumber;
            set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
        }

        private DateTime? _dateOfBirth = null;
        public DateTime? DateOfBirth
        {
            get => _dateOfBirth;
            set => this.RaiseAndSetIfChanged(ref _dateOfBirth, value);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }


        private string _gender = string.Empty;
        public string Gender
        {
            get => _gender;
            set => this.RaiseAndSetIfChanged(ref _gender, value);
        }

        private bool _showPassword = false;
        public bool ShowPassword
        {
            get => _showPassword;
            set
            {
                _ = this.RaiseAndSetIfChanged(ref _showPassword, value);
                PasswordChar = value ? null : HIDDEN;
            }
        }

        private bool _trueIsMaleGenderChecked = false;
        public bool IsMaleGenderChecked
        {
            get => _trueIsMaleGenderChecked;
            set => this.RaiseAndSetIfChanged(ref _trueIsMaleGenderChecked, value);
        }

        private bool _trueIsFemaleGenderChecked = false;
        public bool IsFemaleGenderChecked
        {
            get => _trueIsFemaleGenderChecked;
            set => this.RaiseAndSetIfChanged(ref _trueIsFemaleGenderChecked, value);
        }

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> TryDoUserRegistrationCommand { get; }
        public ReactiveCommand<Unit, Unit> DoUserRegistrationCommand { get; }
        public ReactiveCommand<Unit, Unit> ImageOfTheUserLoadCommand { get; }
        public ReactiveCommand<string, Unit> ReGenderPutCommand { get; }
        public ReactiveCommand<string, Unit> GenderPutCommand { get; }
        public ReactiveCommand<Unit, Unit> ClosePageCommand { get; }

        #endregion

        #region Protected Methods

        protected virtual Type GetOwnerWindowType()
        {
            return typeof(MainWindow);
        }

        protected virtual IUser GetUserInfo()
        {
            byte[] image = UserImage is not null
                ? BitmapService.BmpToBytes(UserImage)
                : Array.Empty<byte>();

            return new UserInfo()
            {
                Login = Login,
                Password = Password,
                Name = Name,
                Surname = Surname,
                Patronymic = Patronymic,
                PhoneNumber = PhoneNumber,
                Gender = Gender,
                Position = _position,
                Status = UserInfo.ST_ACTIVE,
                Money = 0,
                Icon = image,
                DateOfBirth = DateOfBirth!.Value
            };
        }

        protected virtual bool VerifyFieldsCorrectness()
        {
            var verifier = new CheckPassword(Password);
            Window? ownerWindow = WindowSearcher.FindWindowByType(GetOwnerWindowType());

            if (string.IsNullOrEmpty(Login))
            {
                MsgBox.InfoMsg("Введите логин.").Dialog(ownerWindow);
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                MsgBox.InfoMsg("Введите пароль.").Dialog(ownerWindow);
                return false;
            }
            if (!verifier.IsGood())
            {
                MsgBox.InfoMsg($"Пароль слишком слабый. {CheckPassword.REQUIREMENTS}").Dialog(ownerWindow);
                return false;
            }
            if (Password != PasswordConfirmation)
            {
                MsgBox.InfoMsg("Пароли не совпадают.").Dialog(ownerWindow);
                return false;
            }
            if (string.IsNullOrEmpty(Name))
            {
                MsgBox.InfoMsg("Введите имя.").Dialog(ownerWindow);
                return false;
            }
            if (string.IsNullOrEmpty(Surname))
            {
                MsgBox.InfoMsg("Введите фамилию.").Dialog(ownerWindow);
                return false;
            }
            if (string.IsNullOrEmpty(Patronymic))
            {
                MsgBox.InfoMsg("Введите отчество.").Dialog(ownerWindow);
                return false;
            }
            if (PhoneNumber.Where(t => char.IsDigit(t)).Count() != NUMBER_MAX_DIGITS)
            {
                MsgBox.InfoMsg("Введите корректный номер телефона.").Dialog(ownerWindow);
                return false;
            }
            if (string.IsNullOrEmpty(Gender))
            {
                MsgBox.InfoMsg("Выберите пол.").Dialog(ownerWindow);
                return false;
            }
            if (DateOfBirth is null)
            {
                MsgBox.InfoMsg("Введите дату рождения.").Dialog(ownerWindow);
                return false;
            }

            return true;
        }

        protected virtual void FieldsClear()
        {
            PhoneNumber = string.Empty;
            Gender = string.Empty;
            Login = string.Empty;
            Name = string.Empty;
            Surname = string.Empty;
            Patronymic = string.Empty;
            Password = string.Empty;
            PasswordConfirmation = string.Empty;

            IsMaleGenderChecked = false;
            IsFemaleGenderChecked = false;
            ShowPassword = false;

            UserImage?.Dispose();
            UserImage = null;
            DateOfBirth = null;
        }

        #endregion

        #region Events

        public delegate void RegisteredTheUserHandler(IUser user);
        public event RegisteredTheUserHandler? RegisteredTheUser;

        public delegate void ClosingTheTabOrPageHandler();
        public event ClosingTheTabOrPageHandler? ClosingTheTabOrPage;

        #endregion

        #region Private Fields

        private readonly string _position;

        private const int NUMBER_MAX_DIGITS = 11;
        private const char HIDDEN = '*';

        #endregion
    }
}
