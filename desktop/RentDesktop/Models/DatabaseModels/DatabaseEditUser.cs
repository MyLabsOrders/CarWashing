﻿namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseEditUser {
		public DatabaseEditUser ( ) {
			}

		public string identityId { get; set; } = string . Empty;
		public string firstName { get; set; } = string . Empty;
		public string middleName { get; set; } = string . Empty;
		public string lastName { get; set; } = string . Empty;
		public string phoneNumber { get; set; } = string . Empty;
		public string userImage { get; set; } = string . Empty;
		public string birthDate { get; set; } = string . Empty;
		public string gender { get; set; } = string . Empty;
		public bool isActive { get; set; } = true;
		}

#pragma warning restore IDE1006
	}
