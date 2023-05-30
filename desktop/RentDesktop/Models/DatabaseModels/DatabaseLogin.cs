namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseLogin {
		public DatabaseLogin ( ) {
			}

		public DatabaseLogin ( string n , string p ) {
		this . username=n;
		this . password=p;
			}


		public string username { get; set; } = string . Empty;

		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		public string password { get; set; } = string . Empty;
		}

#pragma warning restore IDE1006
	}
