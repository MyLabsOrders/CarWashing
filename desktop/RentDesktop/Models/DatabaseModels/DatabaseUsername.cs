namespace CarWashing . Models . DatabaseModels {
 

	internal class DatabaseUsername {
		public DatabaseUsername ( ) {
			}

		public DatabaseUsername ( string u ) => this . username=u;

		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}

		public string username { get; set; } = string . Empty;
		}

 
	}
