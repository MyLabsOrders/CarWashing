﻿namespace RentDesktop . Models . DatabaseModels {
#pragma warning disable IDE1006

	internal class DatabaseOrderStatus {
		public DatabaseOrderStatus ( ) {
			}

		public DatabaseOrderStatus ( string orderId , string status ) {
		this . orderId=orderId;
		this . status=status;
			}

		public string orderId { get; set; } = string . Empty;
		public string status { get; set; } = string . Empty;
		}

#pragma warning restore IDE1006
	}
