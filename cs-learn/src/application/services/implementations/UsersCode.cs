using cs_learn.application.services.dto;
using cs_learn.errors;

namespace cs_learn.application.services.implementations;

public class UsersCode : IUsersCode
{
    private readonly List<UserCodeDto> _userCodes = new List<UserCodeDto>();

    public string GenerateCode()
    {
        Random randomizer = new Random();
        int code = randomizer.Next(1, 1000000);
        return this.FormatCode(code);
    }
    string FormatCode(int code)
    {
        string codeString = code.ToString();

        if (codeString.Length < 6)
        {
            return $"0{codeString}";
        }

        if (codeString.Length < 5)
        {
            return $"00{codeString}";
        }

        if (codeString.Length < 4)
        {
            return $"000{codeString}";
        }

        if (codeString.Length < 3)
        {
            return $"0000{codeString}";
        }

        if (codeString.Length < 2)
        {
            return $"00000{codeString}";
        }

        return codeString;
    }

    public void Save(UserCodeDto userCode)
    {
        this._userCodes.Add(userCode);
    }

    public UserCodeDto FindByEmail(string email)
    {
        UserCodeDto? userCode = this._userCodes.Find(userCode => userCode.User.Email == email);
        if (userCode == null)
            throw new ErrorException("Code not found", 404);
        return userCode;
    }

    public void Delete(string email)
    {
        UserCodeDto? userCode = this._userCodes.Find(userCode => userCode.User.Email == email);
        if (userCode != null)
        {
            this._userCodes.Remove(userCode);
        }
    }
}