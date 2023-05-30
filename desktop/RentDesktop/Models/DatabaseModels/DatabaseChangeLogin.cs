namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseChangeLogin {
		public DatabaseChangeLogin ( ) {
			}

		public DatabaseChangeLogin ( string username ) => this . username=username;

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

#pragma warning restore IDE1006
	}
