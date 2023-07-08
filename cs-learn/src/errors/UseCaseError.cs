namespace cs_learn.errors;

public class UseCaseError : ErrorException
{
    public UseCaseError(string message, int statusCode): base(message, statusCode)
    {}
}