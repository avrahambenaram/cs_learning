namespace cs_learn.application.services.implementations;

public class Encrypter : IEncrypter
{
    public string Hash(string password)
    {
        return password;
    }

    public bool Compare(string receivedPassword, string hashedPassword)
    {
        return receivedPassword == hashedPassword;
    }
}