namespace cs_learn.application.services;

public interface IEncrypter
{
    string Hash(string password);
    bool Compare(string receivedPassword, string hashedPassword);
}