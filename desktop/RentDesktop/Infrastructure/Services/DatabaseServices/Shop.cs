using RentDesktop . Models;
using RentDesktop . Models . DatabaseModels;
using System;
using System . Collections . Generic;
using System . Linq;
using System . Net . Http;
using System . Net . Http . Json;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class Shop {
		public static void AddProduct ( IProductModel transport ) {
		using var db = new ConnectToDb();

		const string addOrderHandle = "/api/Order";

		byte[] transportIconBytes = transport.Icon is null
								? Array.Empty<byte>()
								: BitmapService.BmpToBytes(transport.Icon);

		var content = new DatabaseCreateProduct()
						{
			name = transport.Name,
			company = transport.Company,
			status = OrderModel.AVAIL,
			price = transport.Price,
			orderImage = BitmapService.BytesToStr(transportIconBytes)
			};

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

		using HttpResponseMessage addOrderResponse = db.Post(addOrderHandle, content).Result;

		if ( !addOrderResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( addOrderResponse );
			}
			}

		public static List<ProductModel> Products ( ) {
		var transports = new List<ProductModel>();
		using var db = new ConnectToDb();

		int currentPage = 1;
		IEnumerable<DatabaseOrder> currentOrder;

		do {
		string getOrdersHandle = $"/api/Order?page={currentPage++}";
		using HttpResponseMessage getOrdersResponse = db.Get(getOrdersHandle).Result;

		if ( !getOrdersResponse . IsSuccessStatusCode ) {
		throw new ResponseErrException ( getOrdersResponse );
			}

		DatabaseOrderCollection? orderCollection = getOrdersResponse.Content.ReadFromJsonAsync<DatabaseOrderCollection>().Result;

		if ( orderCollection is null||orderCollection . orders is null ) {
		throw new ContentException ( getOrdersResponse . Content );
			}

		IEnumerable<DatabaseOrder> orders = orderCollection.orders.Where(t => t.orderDate is null);

		IEnumerable<IReadOnlyList<ProductModel>> transportsCollection = ModelConverter.ConvertProductsToOrder(orders)
										.Select(t => t.Models);

		foreach ( IReadOnlyList<ProductModel>? currTransports in transportsCollection ) {
		transports . AddRange ( currTransports );
			}

		currentOrder=orderCollection . orders;
			}
		while ( currentOrder . Any ( ) );

		return transports;
			}
		}
	}
