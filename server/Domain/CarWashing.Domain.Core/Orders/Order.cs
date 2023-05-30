using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Users;
#pragma warning disable CS8618

namespace CarWashing.Domain.Core.Orders;

public class Order {
    protected Order() { }

    public Order(
        Guid id,
        User? user,
        string name,
        string company,
        decimal price,
        DateTime? orderDate,
        string image)
    {
        Id = id;
        User = user;
        UserId = user?.Id;
        Name = name;
        Company = company;
        Price = price;
        OrderDate = orderDate;
        Image = image;
    }

    public Guid Id { get; }
    public virtual User? User { get; set; }
    public Guid? UserId { get; set; }
    public string Name { get; }
    public string Company { get; }
    public string Image { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal Price { get; }
    public uint? Amount { get; set; }
    public decimal TotalPrice => Price * (Amount ?? 0);
}
