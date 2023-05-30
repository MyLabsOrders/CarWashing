namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseOrderProduct {
		public DatabaseOrderProduct ( ) {
			}

		public DatabaseOrderProduct ( string i , int c , int d ) {
		this . orderId=i;
		this . count=c;
		this . days=d;
			}

		public string orderId { get; set; } = string . Empty;
		public int count { get; set; } = 0;
		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		public int days { get; set; } = 0;
		}

#pragma warning restore IDE1006
	}
