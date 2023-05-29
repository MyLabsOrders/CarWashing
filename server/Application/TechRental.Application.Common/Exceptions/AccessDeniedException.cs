namespace TechRental.Application.Common.Exceptions;

public class AccessDeniedException : ApplicationException {
    public AccessDeniedException() : base("Access denied") { }

    private AccessDeniedException(string message)
        : base(message) { }

    public static AccessDeniedException AccessViolation(Guid userId) {
        return new($"User {userId} has not access to this field");
    }

    public static AccessDeniedException AccessViolation() {
        return new($"User has not access to this field");
    }

    public static AccessDeniedException AnonymousUserHasNotAccess() {
        return new("Anonymous user cannot have an access to this information");
    }

    public static AccessDeniedException NotInRoleException() {
        return new("User hasn't got a privilege for this operation.");
    }
}
