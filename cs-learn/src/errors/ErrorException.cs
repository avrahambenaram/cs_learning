namespace cs_learn.errors;

public class ErrorException : Exception
{
    public int statusCode;
    
    public ErrorException(string message, int statusCode) : base(message)
    {
        this.statusCode = statusCode;
    }
}