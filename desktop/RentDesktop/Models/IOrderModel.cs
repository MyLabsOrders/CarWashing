using System;
using System.Collections.Generic;

namespace RentDesktop.Models
{
    public interface IOrderModel
    {
        string ID { get; }
        double Price { get; }
        string Status { get; set; }
        string? DateOfCreationStamp { get; set; }
        DateTime DateOfCreation { get; }
        IReadOnlyList<ProductModel> Models { get; }
    }
}