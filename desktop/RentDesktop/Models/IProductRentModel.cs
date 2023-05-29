namespace RentDesktop.Models
{
    public interface IProductRentModel
    {
        ProductModel Transport { get; }
        int Days { get; set; }
        double TotalPrice { get; }
    }
}