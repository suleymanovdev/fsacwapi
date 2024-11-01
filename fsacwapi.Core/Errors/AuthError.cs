using fsacwapi.Core.Abstractions;
using fsacwapi.Core.Enums;

namespace fsacwapi.Core.Errors;

public static class AuthError
{
    public static readonly Error ValidationException = new (ErrorCode.ValidationException, "Invalid request");
    public static readonly Error ArgumentNullException = new (ErrorCode.ArgumentNullException, "Request is null");
    public static readonly Error GetException = new (ErrorCode.GetException, "User not found");
    public static readonly Error LoginException = new (ErrorCode.LoginException, "Invalid credentials");
    public static readonly Error RegistrationException = new (ErrorCode.RegistrationException, "Registration failed");
}