namespace cs_learn.application.services;

public interface IPasswordChecker
{
    bool IsPasswordWeak(string password);
}