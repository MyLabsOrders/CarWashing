namespace CarWashing.Domain.Common.Exceptions;

public class UserInputException : DomainException {
    private UserInputException() : base() { }

    private UserInputException(string message)
        : base(message) { }

    public static UserInputException InvalidPhoneNumberException(string message) {
        return new(message);
    }

    public static UserInputException NotEnoughMoneyException(string message) {
        return new(message);
    }

    public static UserInputException UpdateUsernameFailedException(string message) {
        return new(message);
    }

    public static UserInputException IdentityOperationNotSucceededException(string message) {
        return new(message);
    }
}
