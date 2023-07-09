namespace cs_learn.application.services.implementations;

public class PasswordChecker : IPasswordChecker
{
    private bool _isWeak = false;
    
    public bool IsPasswordWeak(string password)
    {
        return this._isWeak;
    }

    public void SetIsWeak(bool isWeak)
    {
        this._isWeak = isWeak;
    }
}