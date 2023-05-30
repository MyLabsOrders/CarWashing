namespace RentDesktop . Models . DatabaseModels {
 

	internal class DatabaseIdentityInfo {
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
		public string role { get; set; } = string . Empty;
		}

 
	}
