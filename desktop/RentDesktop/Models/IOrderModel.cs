using System;
using System . Collections . Generic;

namespace RentDesktop . Models {
	public interface IOrderModel {
		string Status { get; set; }
		string? DateOfCreationStamp { get; set; }
		string ID { get; }
		DateTime DateOfCreation { get; }
		IReadOnlyList<ProductModel> Models { get; }
		double Price { get; }
		}
	}
