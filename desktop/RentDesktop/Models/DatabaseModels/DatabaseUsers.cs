﻿using System . Collections . Generic;

namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseUsers {
		public IEnumerable<DatabaseUser>? users { get; set; } = null;
		public int page { get; set; } = 0;
		public int totalPages { get; set; } = 0;
		}

#pragma warning restore IDE1006
	}
