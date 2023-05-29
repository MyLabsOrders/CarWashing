using System . Collections . Generic;

namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseUser {
		public string id { get; set; } = string . Empty;
		public string firstName { get; set; } = string . Empty;
		public string middleName { get; set; } = string . Empty;
		public string lastName { get; set; } = string . Empty;
		public string image { get; set; } = string . Empty;
		public string birthDate { get; set; } = string . Empty;
		public string number { get; set; } = string . Empty;
		public string gender { get; set; } = string . Empty;
		public bool isActive { get; set; } = true;
		public double money { get; set; } = 0;
		public IEnumerable<DatabaseOrder>? orders { get; set; } = null;
		}

#pragma warning restore IDE1006
	}
