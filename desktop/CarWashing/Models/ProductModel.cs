using Avalonia . Media . Imaging;
using System;

namespace CarWashing . Models {
	public class ProductModel : IProductModel {
		public ProductModel ( string i , string n , string c , double p , DateTime d , Bitmap? img = null ) {
		ID=i;
		Name=n;
		Company=c;
		Price=p;
		CreationDate=d;
		Icon=img;
			}

		public string ID { get; }
		public double Price { get; }
		public double FinalPrice { get; }
		public double NormalizedPrice { get; }
		public DateTime CreationDate { get; }
		public DateTime CreationDateStamp { get; }
		public Bitmap? Icon { get; }
		public string Name { get; }
		public string CompanyStatus { get; }
		public string CompanyName { get; }
		public string Company { get; }

		public ProductModel Copy ( ) => new ( ID , Name , Company , Price , CreationDate , Icon );

		public override string ToString ( ) => Name;

		public ProductModel Self => this;
		public string PricePresenter => $"Цена: {Price}";
		}
	}
