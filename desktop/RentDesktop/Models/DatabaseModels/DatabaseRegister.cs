namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseRegister {
		public DatabaseRegister ( ) {
			}

		public DatabaseRegister ( string username , string password , string rolename ) {
		this . username=username;
		this . password=password;
		this . rolename=rolename;
			}

		public string username { get; set; } = string . Empty;
		public string password { get; set; } = string . Empty;
		public string rolename { get; set; } = string . Empty;
		}

#pragma warning restore IDE1006
	}
