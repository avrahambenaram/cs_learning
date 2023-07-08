using System.Text.RegularExpressions;
using cs_learn.config;
using cs_learn.errors;

namespace cs_learn.domain.valueObjects;

public class Email
{
    public string Value;
    private readonly string _emailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";

    public Email(string email)
    {
        bool isValidEmail = Regex.IsMatch(email, this._emailPattern);
        bool isEmailTooShort = email.Length < ValueObjectsConfig.email.minLength;
        bool isEmailTooLong = email.Length > ValueObjectsConfig.email.maxLength;

        if (!isValidEmail)
            throw new ValueObjectError("Invalid email");
        if (isEmailTooShort)
            throw new ValueObjectError("Email too short");
        if (isEmailTooLong)
            throw new ValueObjectError("Email too long");
        
        this.Value = email;
    }
}