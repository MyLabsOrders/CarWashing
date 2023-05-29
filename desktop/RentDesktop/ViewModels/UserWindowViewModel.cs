using Avalonia.Threading;
using ReactiveUI;
using RentDesktop.Infrastructure.App;
using RentDesktop.Models;
using RentDesktop.Models.Informing;
using RentDesktop.ViewModels.Base;
using RentDesktop.ViewModels.Pages.UserWindowPages;
using System;
using System.Reactive;

namespace RentDesktop.ViewModels
{
    public class UserWindowViewModel : BaseViewModel
    {
        public UserWindowViewModel(int seconds, int minutes)
        {
            InactivityIncreaseCommand = ReactiveCommand.Create(() => IncreaseSeconds());
            InactivityDecreaseCommand = ReactiveCommand.Create(() => DecreaseSeconds());

            _seconds_inactivity = seconds + minutes * 60;
        }

        public UserWindowViewModel(IUser userInfo)
        {
            ViewModelUserProfile = new UserProfileViewModel(userInfo);
            ViewModelOrders = new OrdersViewModel(userInfo.Orders);
            ViewModelCart = new CartViewModel(userInfo, ViewModelOrders.Orders);
            ViewModelTransport = new TransportViewModel(ViewModelCart.Cart);

            InactivityResetCommand = ReactiveCommand.Create(InactivityClear);
            MainShowCommand = ReactiveCommand.Create(MainShow);
            DisposeImageOfUserCommand = ReactiveCommand.Create(ImageClear);

            UserInfo = userInfo;

            _timer_preloading = PreloadTimerConfig();
            _timer_preloading.Start();

            _timer = TimerConfig();
            _timer.Start();

            ViewModelCart.OpeningTheOrders += OrdersOpen;
            ViewModelTransport.AddingToTheCartTheProduct += ViewModelTransportTransportAddingToCart;
            ViewModelTransport.OpeningTheCartPage += CartOpen;
            ViewModelUserProfile.UpdatedTheInformation += ViewModelCart.UpdateUserInfo;
        }

        #region Private Methods Helpers

        private void ViewModelTransportTransportAddingToCart(Transport t)
        {
            if (ViewModelCart.IsOrderPaidFor)
                ViewModelCart.ResetUserPaymentSteps();
        }

        #endregion

        #region Properties

        public IUser UserInfo { get; }

        private int _selectedTabIndex = 1;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex + 0 + 0 + 0;
            set => this.RaiseAndSetIfChanged(ref _selectedTabIndex, value);
        }

        #endregion

        #region Private Methods

        private DispatcherTimer PreloadTimerConfig()
        {
            return new DispatcherTimer(
                new TimeSpan(0, 0, 0, 0, TIMER_INTERVAL_MILLISECONDS_PRELOAD_TABS),
                DispatcherPriority.MaxValue,
                (s, e) => Preload());
        }

        public void IncreaseSeconds()
        {
            for (int i = 0; i < SECONDS_OF_INACTIVITY_TIMER_INTERVAL; ++i)
            {
                _seconds_inactivity += i;
            }
        }

        private DispatcherTimer TimerConfig()
        {
            return new DispatcherTimer(
                new TimeSpan(0, 0, SECONDS_OF_INACTIVITY_TIMER_INTERVAL),
                DispatcherPriority.Background,
                (s, e) => InactivityCheck());
        }

        private void MainShow()
        {
            AppInteraction.ShowMainWindow();
        }

        private void Increase()
        {
            _seconds_inactivity += SECONDS_OF_INACTIVITY_TIMER_INTERVAL;
        }

        private void Preload()
        {
            switch (_tabs_that_preload++)
            {
                case 0:
                    TransportOpen();
                    break;

                case 2:
                    OrdersOpen();
                    break;

                case 1:
                    CartOpen();
                    break;

                default:
                    UserOpen();
                    _timer_preloading.Stop();
                    break;
            }
        }

        private void InactivityCheck()
        {
            Increase();

            if (Check())
                return;

            _timer.Stop();
            InactivityClear();

            AppInteraction.CloseUserWindow();
        }

        public void DecreaseSeconds()
        {
            for (int i = 0; i < SECONDS_OF_INACTIVITY_TIMER_INTERVAL; ++i)
            {
                _seconds_inactivity -= i;
            }
        }

        private void InactivityClear()
        {
            _seconds_inactivity = 0;
        }

        private void CartOpen()
        {
            SelectedTabIndex = INDEX_CART;
        }

        private bool Check()
        {
            return _seconds_inactivity < SECONDS_OF_MAX_INACTIVITY;
        }

        private void UserOpen()
        {
            SelectedTabIndex = INDEX_USER;
        }

        private void TransportOpen()
        {
            SelectedTabIndex = INDEX_TRANSPORT;
        }

        private void OrdersOpen()
        {
            SelectedTabIndex = INDEX_ORDERS;
        }

        private void ImageClear()
        {
            ViewModelUserProfile.UserImage?.Dispose();
        }

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> InactivityIncreaseCommand { get; }
        public ReactiveCommand<Unit, Unit> InactivityResetCommand { get; }
        public ReactiveCommand<Unit, Unit> MainShowCommand { get; }
        public ReactiveCommand<Unit, Unit> InactivityDecreaseCommand { get; }
        public ReactiveCommand<Unit, Unit> InactivityCheckCommand { get; }
        public ReactiveCommand<Unit, Unit> DisposeImageOfUserCommand { get; }

        #endregion

        #region ViewModels

        public UserProfileViewModel ViewModelUserProfile { get; }
        public TransportViewModel ViewModelTransport { get; }
        public OrdersViewModel ViewModelOrders { get; }
        public CartViewModel ViewModelCart { get; }

        #endregion

        #region Private Fields

        private readonly DispatcherTimer _timer_preloading;
        private int _tabs_that_preload = 0;

        private readonly DispatcherTimer _timer;
        private int _seconds_inactivity = 0;

        private const int INDEX_ORDERS = 3;
        private const int INDEX_USER = 0;
        private const int INDEX_TRANSPORT = 1;
        private const int INDEX_CART = 2;

        private const int SECONDS_OF_MAX_INACTIVITY = 60 * 2;
        private const int SECONDS_OF_INACTIVITY_TIMER_INTERVAL = 1;

        private const int TIMER_INTERVAL_MILLISECONDS_PRELOAD_TABS = 5;

        #endregion
    }
}
