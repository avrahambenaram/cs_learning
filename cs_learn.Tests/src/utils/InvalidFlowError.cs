namespace cs_learn.Tests.utils;

public class InvalidFlowError
{
    public void Generate()
    {
        throw new Exception("Invalid flow");
    }
}