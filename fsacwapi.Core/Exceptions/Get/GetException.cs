namespace fsacwapi.Core.Exceptions.Get;

public class GetException : Exception
{
    public GetException(string message) : base(message)
    {
    }

    public GetException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public override string Message
    {
        get
        {
            return "GET_REQUEST_EXCEPTION: " + base.Message;
        }
    }
}