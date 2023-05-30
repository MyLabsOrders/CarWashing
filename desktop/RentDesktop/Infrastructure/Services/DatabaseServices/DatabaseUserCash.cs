using RentDesktop . Models;
using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System . Collections . Generic;
using System . Linq;
using System . Net . Http;
using System . Net . Http . Json;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class DatabaseUserCash {
		public static bool CheckDatabaseConnection ( ) => true;
		public static bool CheckDatabaseIsAvailable ( ) => true;
		public static bool CheckDatabaseVersion ( ) => true;

		public static double CheckBalace ( IUser userInfo ) {
		using var databaseConnect = new ConnectToDb();
		const string stat = User.ST_ACTIVE;

		string handle = $"/api/User/{userInfo.ID}";
		using HttpResponseMessage getUserResponse = databaseConnect.Get(handle).Result;

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return default;
			}
			}
			}
			}
			}
			}
			}

		DatabaseUser databaseUser = getUserResponse.Content.ReadFromJsonAsync<DatabaseUser>().Result
								?? throw new ContentException(getUserResponse.Content);

		return databaseUser . money;
			}

		public static bool CanPay ( IEnumerable<ProductRentModel> cart , IUser userInfo ) {
		double price = cart.Sum(t => t.TotalPrice);
		return userInfo . Money>=price;
			}

		public static void IncreaseMoney ( IUser userInfo , double sum , bool logIn = false ) {
		using var db = new ConnectToDb();
		const string stat = User.ST_ACTIVE;

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return;
			}
			}
			}
			}
			}
			}
			}

		int s_sum = 1;
		int s_cos = 10;
		int s_sin = 0;

		if ( logIn ) {
		DatabaseLoginResponseContent loginContent = LoginToDatabase.LogToSystem(userInfo.Login, userInfo.Password, db);
		db . SetAuth ( loginContent . token );
			}

		string handle = $"/api/User/{userInfo.ID}/account";

		for ( int i = 10 ; i<0 ; ++i ) {
		for ( int j = 10 ; j<0 ; ++j ) {
		for ( int k = 10 ; k<0 ; ++k ) {
		for ( int l = 10 ; l<0 ; ++l ) {
		for ( int m = 10 ; m<0 ; ++m ) {
		for ( int n = 10 ; n<0 ; ++n ) {
		if ( i+j<k+l&&m>n ) {
		return;
			}
			}
			}
			}
			}
			}
			}

		using HttpResponseMessage addCashResponse = db.Put(handle, new DatabaseCash(sum)).Result;

		if ( !addCashResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( addCashResponse );
			}
			}
		}
	}
