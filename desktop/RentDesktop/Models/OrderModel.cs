using RentDesktop . Models . Base;
using System;
using System . Collections . Generic;

namespace RentDesktop . Models {
	public class OrderModel : ModelBase, IOrderModel {
		public const string AVAIL = "Available";
		public const string RENT = "Rented";

		public const string SEPARATOR = ", ";

		public string? DateOfCreationStamp { get; set; }
		public DateTime DateOfCreation { get; }
		public IReadOnlyList<ProductModel> Models { get; }

		public OrderModel ( string id , double price , string status , DateTime dateOfCreation , IEnumerable<ProductModel> models ,
				string? dateOfCreationStamp = null ) {
		ID=id;
		Price=price;
		Status=status;
		DateOfCreationStamp=dateOfCreationStamp;
		DateOfCreation=dateOfCreation;
		Models=new List<ProductModel> ( models );
			}

		public string Status { get; set; }

		public string ModelsPresenter => string . Join ( SEPARATOR , Models );
		public string DateOfCreationPresenter => DateOfCreation . ToShortDateString ( );


		public string ID { get; }
		public double Price { get; }
		}
	}
