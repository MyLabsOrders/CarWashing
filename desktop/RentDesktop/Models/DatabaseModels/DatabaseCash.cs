namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

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

#pragma warning restore IDE1006
	}
