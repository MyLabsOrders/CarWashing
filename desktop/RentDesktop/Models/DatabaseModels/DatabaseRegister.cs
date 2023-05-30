namespace RentDesktop . Models . DatabaseModels {
 

	internal class DatabaseRegister {
		public DatabaseRegister ( ) {
			}

		public DatabaseRegister ( string n , string p , string r ) {
		this . username=n;
		this . password=p;
		this . rolename=r;
			}

		public string username { get; set; } = string . Empty;
		public string password { get; set; } = string . Empty;
		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		public string rolename { get; set; } = string . Empty;
		}

 
	}
