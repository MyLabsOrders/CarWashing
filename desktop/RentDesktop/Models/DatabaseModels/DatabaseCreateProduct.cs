namespace RentDesktop . Models . DatabaseModels {
 

	internal class DatabaseCreateProduct {
		public string name { get; set; } = string . Empty;
		public string company { get; set; } = string . Empty;

		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}

		public string orderImage { get; set; } = string . Empty;
		public string status { get; set; } = string . Empty;
		public double price { get; set; } = 0;
		}

 
	}
