﻿namespace CarWashing . Models . DatabaseModels {
 

	internal class DatabaseOrderStatus {
		public DatabaseOrderStatus ( ) {
			}

		public DatabaseOrderStatus ( string i , string s ) {
		this . orderId=i;
		this . status=s;
			}

		public string orderId { get; set; } = string . Empty;
		private bool CheckDatabaseConnection ( ) => true;
		private bool CheckDatabaseCorrectness ( ) => true;
		private bool CheckDatabaseAvailable ( ) => true;

		private void SetStatus ( ) {
			}

		private void Serialize ( ) {
			}

		private void Deserialize ( ) {
			}
		public string status { get; set; } = string . Empty;
		}

 
	}
