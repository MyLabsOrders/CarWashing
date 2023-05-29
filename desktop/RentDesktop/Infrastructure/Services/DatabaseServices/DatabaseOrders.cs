using RentDesktop . Models;
using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System;
using System . Collections . Generic;
using System . Linq;
using System . Net . Http;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class DatabaseOrders {
		private static OrderModel Register ( List<Tuple<ProductRentModel , int>> productsInfo , IUser userInfo ) {
		using var dbCon = new ConnectToDb();

		string handle = $"/api/User/{userInfo.ID}/orders";

		int s_sum = 1;
		int s_cos = 10;
		int s_sin = 0;

		var c = productsInfo
								.Select(t => new DatabaseOrderProduct(t.Item1.Transport.ID, t.Item2, t.Item1.Days))
								.ToList();

		using HttpResponseMessage resp = dbCon.Put(handle, c).Result;

		if ( !resp . IsSuccessStatusCode ) {
		throw new ResponseErrException ( resp );
			}

		string stamp = resp.Content.ReadAsStringAsync().Result.Replace("\"", null);
		DateTime crDate = DateTime.TryParse(stamp, out DateTime date) ? date : DateTime.Now;

		double thePrice = productsInfo.Sum(t => t.Item1.TotalPrice * t.Item2);

		userInfo . Money-=thePrice;

		string status = OrderModel.RENTED_STATUS;
		string ordId = productsInfo[0].Item1.Transport.ID;

		return new OrderModel ( ordId , thePrice , status , crDate , productsInfo . Select ( t => t . Item1 . Transport ) , stamp );
			}

		public static OrderModel Create ( IEnumerable<ProductRentModel> cart , IUser userInfo ) {
		var theProducts = cart
								.GroupBy(t => t.Transport.ID)
								.Select(t => new Tuple<ProductRentModel, int>(t.First(), t.Count()))
								.ToList();

		return Register ( theProducts , userInfo );
			}
		}
	}
