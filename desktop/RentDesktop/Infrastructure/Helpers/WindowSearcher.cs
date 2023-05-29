using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using RentDesktop.Views;
using System;
using System.Linq;

namespace RentDesktop.Infrastructure.Helpers
{
    internal static class WindowSearcher
    {
        public static Window? Main()
        {
            return Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime app
                ? null
                : app.MainWindow;
        }

        public static Window? User()
        {
            return FindWindowByType(typeof(UserWindow));
        }

        public static Window? Admin()
        {
            return FindWindowByType(typeof(AdminWindow));
        }

        public static Window? FindWindow(Func<Window, bool> predicate)
        {
            return Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime app
                ? null
                : app.Windows.FirstOrDefault(predicate);
        }

        public static Window? FindWindowByType(Type type)
        {
            return FindWindow(t => t.GetType() == type);
        }
    }
}
