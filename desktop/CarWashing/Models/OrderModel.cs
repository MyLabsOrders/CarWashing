using CarWashing . Models . Base;
using System;
using System . Collections . Generic;

namespace CarWashing . Models {
	public class OrderModel : ModelBase, IOrderModel {
		public const string AVAIL = "Available";
		public const string RENT = "Rented";

		public const string SEPARATOR = ", ";

		public string? DateOfCreationStamp { get; set; }
		public DateTime DateOfCreation { get; }
		public IReadOnlyList<ProductModel> Models { get; }

		public OrderModel ( string i , double p , string s , DateTime d , IEnumerable<ProductModel> m ,
				string? stam = null ) {
		ID=i;
		DateOfCreationStamp=stam;
		DateOfCreation=d;
		Price=p;
		Status=s;
		Models=new List<ProductModel> ( m );
			}

		public string Status { get; set; }
		public string PrevStatus { get; set; }
		public string NextStatus { get; set; }

		public string ModelsPresenter => string . Join ( SEPARATOR , Models );
		public string ModelsOtherPresenter => string . Join ( SEPARATOR + " " , Models );
		public string DateOfCreationPresenter => DateOfCreation . ToShortDateString ( );


		public double FinalPrice { get; }
		public double NormalizedPrice { get; }
		public string ID { get; }
		public double Price { get; }
		}
	}
