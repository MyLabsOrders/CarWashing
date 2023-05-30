using System . Collections . Generic;

namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseOrderCollection {
		public IEnumerable<DatabaseOrder>? orders { get; set; } = null;
		public int page { get; set; } = 1;
		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		public int totalPages { get; set; } = 0;
		}

#pragma warning restore IDE1006
	}
