using RentDesktop.Models.Base;
using System;
using System.Collections.Generic;

namespace RentDesktop.Models
{
    public class OrderModel : ModelBase, IOrderModel
    {
        public const string AVAILABLE_STATUS = "Available";
        public const string RENTED_STATUS = "Rented";

        public const string ORDERS_MODELS_DELIMITER = ", ";

        public OrderModel(string id, double price, string status, DateTime dateOfCreation, IEnumerable<ProductModel> models,
            string? dateOfCreationStamp = null)
        {
            ID = id;
            Price = price;
            Status = status;
            DateOfCreationStamp = dateOfCreationStamp;
            DateOfCreation = dateOfCreation;
            Models = new List<ProductModel>(models);
        }

        public string ID { get; }
        public double Price { get; }
        public string Status { get; set; }
        public string? DateOfCreationStamp { get; set; }
        public DateTime DateOfCreation { get; }
        public IReadOnlyList<ProductModel> Models { get; }

        public string ModelsPresenter => string.Join(ORDERS_MODELS_DELIMITER, Models);
        public string DateOfCreationPresenter => DateOfCreation.ToShortDateString();
    }
}
