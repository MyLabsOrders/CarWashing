namespace RentDesktop . Models . DatabaseModels {
 

	internal class DatabaseLoginResponseContent {
		public string userId { get; set; } = string . Empty;
		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		public string token { get; set; } = string . Empty;
		}

 
	}
