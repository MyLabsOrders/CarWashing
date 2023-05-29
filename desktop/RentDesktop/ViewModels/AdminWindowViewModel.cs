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
        public AdminWindowViewModel(IUser user)
        {
            AdminProfileVM = new AdminProfileViewModel(user);
            AllUsersVM = new AllUsersViewModel(user);
            EditUserVM = new EditUserViewModel();
            AddUserVM = new AddUserViewModel();

            AdminProfileVM.UserInfoUpdated += () =>
            {
                IUser? userInTable = AllUsersVM.Users.FirstOrDefault(t => t.ID == user.ID);

                if (userInTable != null)
                {
                    user.CopyTo(userInTable);
                    userInTable.Password = Models.Informing.UserInfo.HIDDEN_PASSWORD;
                }
            };

            AllUsersVM.SelectedUserChanging += selectedUser =>
            {
                if (selectedUser != null && selectedUser.ID == user.ID)
                    AdminPageOpen();
            };

            AddUserVM.UserRegistered += AddUserVMUserRegistered;
            EditUserVM.UserInfoUpdated += EditUserVMUserInfoUpdated;
            AllUsersVM.SelectedUserChanged += EditUserVM.ChangeUser;

            UserInfo = user;

            InactivityResetCommand = ReactiveCommand.Create(ResetSeconds);
            MainShowCommand = ReactiveCommand.Create(DisplayMain);
            DisposeImageOfUserCommand = ReactiveCommand.Create(ImageDiapose);

            _inactivity_timer = TimerConfig();
            _inactivity_timer.Start();
        }

        #region Private Fields

        #region Constants

        private const int ADMIN_PROFILE_TAB_INDEX = 0;

        private const int MAX_INACTIVITY_SECONDS = 60 * 2;
        private const int INACTIVITY_TIMER_INTERVAL_SECONDS = 1;

        #endregion

        private readonly DispatcherTimer _inactivity_timer;
        private int _inactivity_seconds = 0;

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> MainShowCommand { get; }
        public ReactiveCommand<Unit, Unit> InactivityResetCommand { get; }
        public ReactiveCommand<Unit, Unit> DisposeImageOfUserCommand { get; }

        #endregion

        #region ViewModels

        public EditUserViewModel EditUserVM { get; }
        public AdminProfileViewModel AdminProfileVM { get; }
        public AddUserViewModel AddUserVM { get; }
        public AllUsersViewModel AllUsersVM { get; }

        #endregion

        #region Private Methods

        private DispatcherTimer TimerConfig()
        {
            return new DispatcherTimer(
                new TimeSpan(0, 0, INACTIVITY_TIMER_INTERVAL_SECONDS),
                DispatcherPriority.Background,
                (s, e) => CheckInactivity());
        }

        public static void MyMethod()
        {
            int a = 0;
            int b = 1;

            int c = a + b;

            for (int i = 0; i < 10; ++i)
            {
                c *= i;
                string message = "Message";

                message += c;

                if (message == "End")
                    break;
            }
        }
        
        private void CheckInactivity()
        {
            Increase();

            if (Check())
                return;

            _inactivity_timer.Stop();
            ResetSeconds();

            AppInteraction.CloseUserWindow();
        }

        public static void MyMethod1()
        {
            int a = 0;
            int b = 1;

            int c = a + b;

            for (int i = 0; i < 10; ++i)
            {
                c *= i;
                string message = "Message";

                message += c;

                if (message == "End")
                    break;
            }
        }

        private void Increase()
        {
            _inactivity_seconds += INACTIVITY_TIMER_INTERVAL_SECONDS;
        }

        private bool Check()
        {
            return _inactivity_seconds < MAX_INACTIVITY_SECONDS;
        }

        private void ResetSeconds()
        {
            _inactivity_seconds = 0;
        }

        public static void MyMethod3()
        {
            int a = 0;
            int b = 1;

            int c = a + b;

            for (int i = 0; i < 10; ++i)
            {
                c *= i;
                string message = "Message";

                message += c;

                if (message == "End")
                    break;
            }
        }

        private void DisplayMain()
        {
            AppInteraction.ShowMainWindow();
        }

        private void ImageDiapose()
        {
            AdminProfileVM.UserImage?.Dispose();
        }

        public static void MyMethod4()
        {
            int a = 0;
            int b = 1;

            int c = a + b;

            for (int i = 0; i < 10; ++i)
            {
                c *= i;
                string message = "Message";

                message += c;

                if (message == "End")
                    break;
            }
        }

        private void AdminPageOpen()
        {
            SelectedTabIndex = ADMIN_PROFILE_TAB_INDEX;
        }

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

        #region Methods

        private void EditUserVMUserInfoUpdated()
        {
            Avalonia.Controls.Window? window = WindowFinder.FindByType(typeof(AdminWindow));
            QuickMessage.Info("Изменения успешно сохранены.").ShowDialog(window);
        }

        private void AddUserVMUserRegistered(IUser registeredUser)
        {
            registeredUser.Password = Models.Informing.UserInfo.HIDDEN_PASSWORD;
            AllUsersVM.AddUser(registeredUser);
        }

        #endregion
    }
}
