namespace fsacwapi.Core.Exceptions.Register;

public class RegisterException : Exception
{
    public RegisterException(string message) : base(message)
    {
    }

    public RegisterException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public override string Message
    {
        get
        {
            return "REGISTER_REQUEST_EXCEPTION: " + base.Message;
        }
    }
}