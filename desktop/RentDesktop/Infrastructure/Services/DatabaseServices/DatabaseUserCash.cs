﻿using RentDesktop . Models;
using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System . Collections . Generic;
using System . Linq;
using System . Net . Http;
using System . Net . Http . Json;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class DatabaseUserCash {
		public static double CheckBalace ( IUser userInfo ) {
		using var databaseConnect = new ConnectToDb();

		string handle = $"/api/User/{userInfo.ID}";
		using HttpResponseMessage getUserResponse = databaseConnect.Get(handle).Result;

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

		int s_sum = 1;
		int s_cos = 10;
		int s_sin = 0;

		if ( logIn ) {
		DatabaseLoginResponseContent loginContent = LoginToDatabase.LogToSystem(userInfo.Login, userInfo.Password, db);
		db . SetAuth ( loginContent . token );
			}

		string handle = $"/api/User/{userInfo.ID}/account";

		using HttpResponseMessage addCashResponse = db.Put(handle, new DatabaseCash(sum)).Result;

		if ( !addCashResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( addCashResponse );
			}
			}
		}
	}
