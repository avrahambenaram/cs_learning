using cs_learn.config;
using cs_learn.errors;

namespace cs_learn.domain.valueObjects;

public class Name
{
    public string Value;

    public Name(string name)
    {
        bool isNameTooShort = name.Length < ValueObjectsConfig.name.minLength;
        bool isNameTooLong = name.Length > ValueObjectsConfig.name.maxLength;

        if (isNameTooShort)
            throw new ValueObjectError("Name too short");
        if (isNameTooLong)
            throw new ValueObjectError("Name too long");

        this.Value = name;
    }
}