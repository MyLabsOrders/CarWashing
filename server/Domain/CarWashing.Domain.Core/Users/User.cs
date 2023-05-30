using CarWashing.Domain.Common.Exceptions;
using CarWashing.Domain.Core.Orders;
using CarWashing.Domain.Core.ValueObject;
#pragma warning disable CS8618

namespace CarWashing.Domain.Core.Users;

public class User {
    private readonly List<Order> _orders;

    protected User() {
        _orders = new List<Order>();
    }

    public User(
        Guid id,
        string firstName,
        string middleName,
        string lastName,
        DateOnly birthDate,
        PhoneNumber phoneNumber,
        Gender gender,
        string image)
    {
        ArgumentNullException.ThrowIfNull(firstName);
        ArgumentNullException.ThrowIfNull(middleName);
        ArgumentNullException.ThrowIfNull(lastName);
        ArgumentNullException.ThrowIfNull(phoneNumber);

        Id = id;
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Gender = gender;
        Status = true;

        _orders = new List<Order>();
        Money = 0;
        Image = image;
    }

    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateOnly BirthDate { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public string Image { get; set; }
    public bool Status { get; set; }
    public Gender Gender { get; set; }
    public virtual IEnumerable<Order> Orders => _orders;
    public decimal Money { get; set; }

    public void WithdrawMoney(decimal amount) {
        if (amount > Money) {
            throw UserInputException.NotEnoughMoneyException("User doesn't have enough money");
        }

        Money -= amount;
    }

    public override string ToString() {
        return $"{LastName} {FirstName} {MiddleName}";
    }

    public Order AddOrder(Order order) {
        ArgumentNullException.ThrowIfNull(order);

        _orders.Add(order);
        order.User = this;

        return order;
    }

    public void RemoveOrder(Order order) {
        order.User = null;
        _orders.Remove(order);
    }
}
