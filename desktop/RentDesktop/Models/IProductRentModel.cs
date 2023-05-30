namespace CarWashing . Models {
	public interface IProductRentModel {
		int Days { get; set; }
		ProductModel Transport { get; }
		double TotalPrice { get; }
		}
	}
