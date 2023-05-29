using System . Collections . Generic;
using System . Collections . ObjectModel;

namespace RentDesktop . Infrastructure . Extensions {
	internal static class ObservableCollectionExtensions {
		public static void AddTheRange<T> ( this ObservableCollection<T> col , IEnumerable<T> elems ) {
		foreach ( T? item in elems ) {
		col . Add ( item );
			}
			}

		public static void ResetElements<T> ( this ObservableCollection<T> collection , IEnumerable<T> items ) {
		collection . Clear ( );
		collection . AddTheRange ( items );
			}
		}
	}
