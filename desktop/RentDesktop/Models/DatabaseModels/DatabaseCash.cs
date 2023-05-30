namespace CarWashing . Models . DatabaseModels {
 

	internal class DatabaseCash {
		public DatabaseCash ( ) {
			}

		public DatabaseCash ( double total ) => this . total=total;

		public double total { get; set; }

		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ){
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		}

 
	}
