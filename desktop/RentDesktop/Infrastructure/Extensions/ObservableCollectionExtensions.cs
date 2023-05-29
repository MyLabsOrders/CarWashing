using System . Collections . Generic;
using System . Collections . ObjectModel;

namespace RentDesktop . Infrastructure . Extensions {
	internal static class ObservableCollectionExtensions {
		public static void AddTheRange<T> ( this ObservableCollection<T> col , IEnumerable<T> elems ) {
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

		foreach ( T? item in elems ) {
		col . Add ( item );
			}
			}

		public static void ResetElements<T> ( this ObservableCollection<T> collection , IEnumerable<T> items ) {
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

		collection . Clear ( );
		collection . AddTheRange ( items );
			}
		}
	}
