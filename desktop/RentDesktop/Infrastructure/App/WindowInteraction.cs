using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using RentDesktop.ViewModels;
using RentDesktop.Views;

namespace RentDesktop.Infrastructure.App
{
    internal static class WindowInteraction
    {
        public static void CloseCurrentApp()
        {
            IApplicationLifetime? currAppLifetime = Application.Current?.ApplicationLifetime;

            if (currAppLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
                lifetime.Shutdown();
        }

        public static void CloseMainWindow()
        {
            Window? mainWindow = WindowSearcher.FindMainWindow();
            mainWindow?.Close();
        }

        public static void CloseUserWindow()
        {
            Window? userWindow = WindowSearcher.FindByType(typeof(UserWindow));
            userWindow?.Close();
        }

        public static void CloseAdminWindow()
        {
            Window? adminWindow = WindowSearcher.FindByType(typeof(AdminWindow));
            adminWindow?.Close();
        }

        public static void HideMainWindow()
        {
            Window? mainWindow = WindowSearcher.FindMainWindow();
            var viewModel = mainWindow?.DataContext as MainWindowViewModel;

            viewModel?.MainHide();
        }

        public static void ShowMainWindow()
        {
            Window? mainWindow = WindowSearcher.FindMainWindow();
            var viewModel = mainWindow?.DataContext as MainWindowViewModel;

            viewModel?.MainShow();
        }
    }
}
