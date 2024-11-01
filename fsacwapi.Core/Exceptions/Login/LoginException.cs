namespace fsacwapi.Core.Exceptions.Register;

public class LoginException : Exception
{
    public LoginException(string message) : base(message)
    {
    }

    public LoginException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public override string Message
    {
        get
        {
            return "LOGIN_REQUEST_EXCEPTION: " + base.Message;
        }
    }
}