namespace TechRental.Domain.Common.Exceptions;

public class UserInputException : DomainException {
    private UserInputException() : base() { }

    private UserInputException(string message)
        : base(message) { }

    public static UserInputException InvalidPhoneNumberException(string message) {
        return new(message);
    }

    public static UserInputException NegativeOrderTotalException() {
        return new();
    }

    public static UserInputException NegativeOrderAmountException() {
        return new();
    }

    public static UserInputException NegativeOrderPeriodException() {
        return new();
    }

    public static UserInputException NegativeUserBalanceException(string message) {
        return new(message);
    }

    public static UserInputException UpdateUsernameFailedException(string message) {
        return new(message);
    }

    public static UserInputException IdentityOperationNotSucceededException(string message) {
        return new(message);
    }
}
