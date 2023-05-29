using TechRental.Domain.Common.Exceptions;

namespace TechRental.Domain.Core.ValueObject;

public readonly record struct PhoneNumber {
    public PhoneNumber(string number) {
        if (string.IsNullOrEmpty(number) is true)
            throw UserInputException.InvalidPhoneNumberException($"{number} is not in correct format.");

        Value = number;
    }

    public string Value { get; }
}