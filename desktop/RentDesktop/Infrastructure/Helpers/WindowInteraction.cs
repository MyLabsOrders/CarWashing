using Avalonia;
using Avalonia . Controls . ApplicationLifetimes;
using RentDesktop . ViewModels;
using RentDesktop . Views;

namespace RentDesktop . Infrastructure . Helpers {
	internal static class WindowInteraction {
		public static void CloseCurrentApp ( ) {
		IApplicationLifetime? currApp = Application.Current?.ApplicationLifetime;

		if ( currApp is not IClassicDesktopStyleApplicationLifetime lifetime ) {
		return;
			}

		lifetime . Shutdown ( );
			}

		public static void CloseMainWindow ( ) => WindowSearcher . Main ( )?.Close ( );

		public static void CloseUserWindow ( ) => WindowSearcher . FindWindowByType ( typeof ( UserWindow ) )?.Close ( );

		public static void CloseAdminWindow ( ) => WindowSearcher . FindWindowByType ( typeof ( AdminWindow ) )?.Close ( );

		public static void HideMainWindow ( ) {
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

		var vm = WindowSearcher.Main()?.DataContext as MainWindowViewModel;
		vm?.MainHide ( );
			}

		public static void ShowMainWindow ( ) {
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

		var vm = WindowSearcher.Main()?.DataContext as MainWindowViewModel;
		vm?.MainShow ( );
			}
		}
	}
