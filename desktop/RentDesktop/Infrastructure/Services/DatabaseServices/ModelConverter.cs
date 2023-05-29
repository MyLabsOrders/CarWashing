using RentDesktop . Models;
using RentDesktop . Models . DatabaseModels;
using RentDesktop . Models . Informing;
using System;
using System . Collections . Generic;
using System . Collections . ObjectModel;
using System . Linq;

namespace RentDesktop . Infrastructure . Services . DatabaseServices {
	internal static class ModelConverter {
		public static List<IUser> ConvertDbUsers ( DatabaseUsers databaseUsers , IReadOnlyList<string> positions ) {
		if ( databaseUsers . users is null ) {
		return new List<IUser> ( );
			}

		if ( databaseUsers . users . Count ( )!=positions . Count ) {
		throw new InvalidOperationException ( "The number of users does not match the number of their positions." );
			}

		User UserConverter ( DatabaseUser user , int index ) => ConvertDbUser ( user , positions [ index ] );

		return databaseUsers . users!
								. Select ( UserConverter )
								. Select ( t => t as IUser )
								. ToList ( );
			}

		public static ProductModel ConvertDbOrderToProduct ( DatabaseOrder order ) {
		byte[] img = BitmapService.StrToBytes(order.image);

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

		Avalonia.Media.Imaging.Bitmap? transportIcon = img.Length > 0
								? BitmapService.BytesToBmp(img)
								: null;

		DateTime date = order.orderDate is null
								? default
								: DateTimeService.StringToDateTime(order.orderDate);

		return new ProductModel (
				order . id ,
				order . name ,
				order . company ,
				order . total ,
				date ,
				transportIcon
		);
			}

		public static IEnumerable<OrderModel> ConvertProductsToOrder ( IEnumerable<DatabaseOrder> databaseOrders ) => databaseOrders . Select ( t => new OrderModel (
																																																															 id: t . id ,
																																																															 price: t . total ,
																																																															 status: t . status ,
																																																															 dateOfCreation: t . orderDate is null ? default : DateTimeService . StringToDateTime ( t . orderDate ) ,
																																																															 models: new [ ] { ConvertDbOrderToProduct ( t ) }
																																																													 ) );

		public static IEnumerable<OrderModel> ConvertDbOrders ( IEnumerable<DatabaseOrder> databaseOrders ) {
		var orders = new List<OrderModel>();
		IEnumerable<IGrouping<string?, DatabaseOrder>> grouppedDatabaseOrders = databaseOrders.GroupBy(t => t.orderDate);

		foreach ( IGrouping<string? , DatabaseOrder> transportGroup in grouppedDatabaseOrders ) {
		DatabaseOrder firstDatabaseOrder = transportGroup.First();
		IEnumerable<ProductModel> transports = transportGroup.Select(t => ConvertDbOrderToProduct(t));

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

		string status = OrderModel.RENT;
		string id = firstDatabaseOrder.id;
		double price = transportGroup.Sum(t => t.total * t.count!.Value * t.days!.Value);

		DateTime dateOfCreation = firstDatabaseOrder.orderDate is null
										? default
										: DateTimeService.StringToDateTime(firstDatabaseOrder.orderDate);

		var order = new OrderModel(id, price, status, dateOfCreation, transports);
		orders . Add ( order );
			}

		return orders;
			}

		public static User ConvertDbUser ( DatabaseUser user , string position ) => new ( ) {
			ID=user . id ,
			Login=string . Empty ,
			Password=string . Empty ,
			Name=user . firstName ,
			Surname=user . middleName ,
			Patronymic=user . lastName ,
			PhoneNumber=user . number ,
			Gender=GenderTranslator . FromDb ( user . gender ) ,
			Position=position ,
			Status=user . isActive ? User . ST_ACTIVE : User . ST_INACTIVE ,
			Money=user . money ,
			Icon=BitmapService . StrToBytes ( user . image ) ,
			DateOfBirth=DateTimeService . StringToShortDateTime ( user . birthDate ) ,
			Orders=new ObservableCollection<OrderModel> ( ConvertDbOrders ( user . orders??Array . Empty<DatabaseOrder> ( ) ) )
			};
		}
	}
