namespace RentDesktop . Models . DatabaseModels {
 

	internal class DatabaseOrder {
		public string id { get; set; } = string . Empty;
		public string status { get; set; } = string . Empty;
		public string name { get; set; } = string . Empty;
		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		public string company { get; set; } = string . Empty;
		public string image { get; set; } = string . Empty;
		public double total { get; set; } = 0;
		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		public string? orderDate { get; set; } = null;
		public int? count { get; set; } = null;
		public int? days { get; set; } = null;
		}

 
	}
