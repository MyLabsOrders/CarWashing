using Avalonia.Threading;
using ReactiveUI;
using RentDesktop.Infrastructure.App;
using RentDesktop.Models.Communication;
using RentDesktop.Models.Informing;
using RentDesktop.ViewModels.Base;
using RentDesktop.ViewModels.Pages.AdminWindowPages;
using RentDesktop.Views;
using System;
using System.Linq;
using System.Reactive;

namespace RentDesktop.ViewModels
{
    public class AdminWindowViewModel : BaseViewModel
    {
        public AdminWindowViewModel(int seconds, int minutes)
        {
            InactivityIncreaseCommand = ReactiveCommand.Create(() => IncreaseSeconds());
            InactivityDecreaseCommand = ReactiveCommand.Create(() => DecreaseSeconds());

            _seconds_inactivity = seconds + minutes * 60;
        }

        public AdminWindowViewModel(IUser user)
        {
            ViewModelAdminProfile = new AdminProfileViewModel(user);
            ViewModelAllUsers = new AllUsersViewModel(user);
            ViewModelEditUser = new EditUserViewModel();
            ViewModelAddUser = new AddUserViewModel();

            ViewModelAdminProfile.UpdatedTheInformation += () =>
            {
                IUser? userInTable = ViewModelAllUsers.Users.FirstOrDefault(t => t.ID == user.ID);

                if (userInTable != null)
                {
                    user.CopyTo(userInTable);
                    userInTable.Password = Models.Informing.UserInfo.HIDDEN_PASSWORD;
                }
            };

            ViewModelAllUsers.ChangingUser += selectedUser =>
            {
                if (selectedUser != null && selectedUser.ID == user.ID)
                    AdminPageOpen();
            };

            ViewModelAddUser.RegisteredTheUser += ViewModelAddUserUserRegistered;
            ViewModelEditUser.UpdatedTheInformation += ViewModelEditUserUserInfoUpdated;
            ViewModelAllUsers.ChangedUser += ViewModelEditUser.UserPut;

            UserInfo = user;

            InactivityResetCommand = ReactiveCommand.Create(ResetSeconds);
            MainShowCommand = ReactiveCommand.Create(DisplayMain);
            DisposeImageOfUserCommand = ReactiveCommand.Create(ImageDiapose);

            _timer_of_inactivity = TimerConfig();
            _timer_of_inactivity.Start();
        }

        #region Private Fields

        private readonly DispatcherTimer _timer_of_inactivity;
        private int _seconds_inactivity = 0;

        private const int TAB_ADMIN_PROFILE = 0;

        private const int SECONDS_OF_MAX_INACTIVITY = 60 * 2;
        private const int SECONDS_OFINACTIVITY_TIMER_INTERVAL = 1;

        #endregion

        #region Private Methods

        private DispatcherTimer TimerConfig()
        {
            return new DispatcherTimer(
                new TimeSpan(0, 0, SECONDS_OFINACTIVITY_TIMER_INTERVAL),
                DispatcherPriority.Background,
                (s, e) => CheckInactivity());
        }

        public bool CheckSeconds()
        {
            for (int i = 0; i < SECONDS_OFINACTIVITY_TIMER_INTERVAL; ++i)
            {
                if (_seconds_inactivity == i)
                    return true;
            }

            return false;
        }

        private void Increase()
        {
            _seconds_inactivity += SECONDS_OFINACTIVITY_TIMER_INTERVAL;
        }

        private bool Check()
        {
            return _seconds_inactivity < SECONDS_OF_MAX_INACTIVITY;
        }

        private void ResetSeconds()
        {
            _seconds_inactivity = 0;

            if (Math.Abs(0) < _seconds_inactivity)
                _seconds_inactivity = 0;
        }

        public void IncreaseSeconds()
        {
            for (int i = 0; i < SECONDS_OFINACTIVITY_TIMER_INTERVAL; ++i)
            {
                _seconds_inactivity += i;
            }
        }

        private void CheckInactivity()
        {
            Increase();

            if (Check())
                return;

            _timer_of_inactivity.Stop();
            ResetSeconds();

            AppInteraction.CloseUserWindow();
        }

        private void ImageDiapose()
        {
            ViewModelAdminProfile.UserImage?.Dispose();
        }

        public void DecreaseSeconds()
        {
            for (int i = 0; i < SECONDS_OFINACTIVITY_TIMER_INTERVAL; ++i)
            {
                _seconds_inactivity -= i;
            }
        }

        private void AdminPageOpen()
        {
            SelectedTabIndex = TAB_ADMIN_PROFILE;
        }

        private void DisplayMain()
        {
            AppInteraction.ShowMainWindow();
        }

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> InactivityIncreaseCommand { get; }
        public ReactiveCommand<Unit, Unit> MainShowCommand { get; }
        public ReactiveCommand<Unit, Unit> InactivityDecreaseCommand { get; }
        public ReactiveCommand<Unit, Unit> InactivityResetCommand { get; }
        public ReactiveCommand<Unit, Unit> InactivityCheckCommand { get; }
        public ReactiveCommand<Unit, Unit> DisposeImageOfUserCommand { get; }

        #endregion

        #region Properties

        public IUser UserInfo { get; }

        private int _selectedTabIndex = 0;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedTabIndex, value);
        }

        #endregion

        #region Private Methods Helpers

        private void ViewModelEditUserUserInfoUpdated()
        {
            Avalonia.Controls.Window? window = WindowFinder.FindByType(typeof(AdminWindow));
            QuickMessage.Info("Изменения успешно сохранены.").ShowDialog(window);
        }

        private void ViewModelAddUserUserRegistered(IUser registeredUser)
        {
            registeredUser.Password = Models.Informing.UserInfo.HIDDEN_PASSWORD;
            ViewModelAllUsers.UserPutAndAdd(registeredUser);
        }

        #endregion

        #region ViewModels

        public EditUserViewModel ViewModelEditUser { get; }
        public AddUserViewModel VM_AddUser { get; }
        public AddUserViewModel ViewModelAddUser { get; }
        public AllUsersViewModel ViewModelAllUsers { get; }
        public AllUsersViewModel VM_AllUsers { get; }
        public AdminProfileViewModel ViewModelAdminProfile { get; }

        #endregion
    }
}
