namespace cs_learn.errors;

public class ValueObjectError : ErrorException
{
    public ValueObjectError(string message) : base(message, 403)
    {}
}