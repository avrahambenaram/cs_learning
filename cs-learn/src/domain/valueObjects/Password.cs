using cs_learn.config;
using cs_learn.errors;

namespace cs_learn.domain.valueObjects;

public class Password
{
    public string Value;

    public Password(string password)
    {
        bool isPasswordTooShort = password.Length < ValueObjectsConfig.password.minLength;

        if (isPasswordTooShort)
            throw new ValueObjectError("Password too short");
            
        this.Value = password;
    }
}