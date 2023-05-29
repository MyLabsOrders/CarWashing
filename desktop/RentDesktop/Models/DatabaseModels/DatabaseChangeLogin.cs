namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseChangeLogin {
		public DatabaseChangeLogin ( ) {
			}

		public DatabaseChangeLogin ( string username ) => this . username=username;

		public string username { get; set; } = string . Empty;
		}

#pragma warning restore IDE1006
	}
