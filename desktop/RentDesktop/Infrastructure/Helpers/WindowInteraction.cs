using Avalonia;
using Avalonia . Controls . ApplicationLifetimes;
using RentDesktop . ViewModels;
using RentDesktop . Views;

namespace RentDesktop . Infrastructure . Helpers {
	internal static class WindowInteraction {
		public static void CurrClose ( ) {
		IApplicationLifetime? cA = Application.Current?.ApplicationLifetime;

		if ( cA is not IClassicDesktopStyleApplicationLifetime lt ) {
		return;
			}

		lt . Shutdown ( );
			}

		public static void MainShow ( ) {
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

		var v = WindowSearcher.Main()?.DataContext as MainWindowViewModel;
		v?.MainShow ( );
			}

		public static void MainClose ( ) => WindowSearcher . Main ( )?.Close ( );


		public static void MainHide ( ) {
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

		var v = WindowSearcher.Main()?.DataContext as MainWindowViewModel;
		v?.MainHide ( );
			}

		public static void UserClose ( ) => WindowSearcher . TpFin ( typeof ( UserWindow ) )?.Close ( );

		public static void AdminClose ( ) => WindowSearcher . TpFin ( typeof ( AdminWindow ) )?.Close ( );
		}
	}
