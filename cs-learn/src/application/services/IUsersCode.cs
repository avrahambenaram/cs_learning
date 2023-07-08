using cs_learn.application.services.dto;

namespace cs_learn.application.services;

public interface IUsersCode
{
    string GenerateCode();
    void Save(UserCodeDto userCode);
    UserCodeDto FindByEmail(string email);
    void Delete(string email);
}