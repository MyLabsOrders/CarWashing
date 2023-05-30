using System . Collections . Generic;

namespace CarWashing . Models . DatabaseModels {
 

	internal class DatabaseUsers {
		public IEnumerable<DatabaseUser>? users { get; set; } = null;
		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;
		public int page { get; set; } = 0;
		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		public int totalPages { get; set; } = 0;
		}

 
	}
