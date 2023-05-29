using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using RentDesktop.ViewModels;
using RentDesktop.Views;

namespace RentDesktop.Infrastructure.Helpers
{
    internal static class WindowInteraction
    {
        public static void CloseCurrentApp()
        {
            IApplicationLifetime? currApp = Application.Current?.ApplicationLifetime;

            if (currApp is not IClassicDesktopStyleApplicationLifetime lifetime)
                return;

            lifetime.Shutdown();
        }

        public static void CloseMainWindow()
        {
            WindowSearcher.Main()?.Close();
        }

        public static void CloseUserWindow()
        {
            WindowSearcher.FindWindowByType(typeof(UserWindow))?.Close();
        }

        public static void CloseAdminWindow()
        {
            WindowSearcher.FindWindowByType(typeof(AdminWindow))?.Close();
        }

        public static void HideMainWindow()
        {
            var vm = WindowSearcher.Main()?.DataContext as MainWindowViewModel;
            vm?.MainHide();
        }

        public static void ShowMainWindow()
        {
            var vm = WindowSearcher.Main()?.DataContext as MainWindowViewModel;
            vm?.MainShow();
        }
    }
}
