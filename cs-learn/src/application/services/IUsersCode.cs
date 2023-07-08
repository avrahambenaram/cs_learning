using cs_learn.application.services.dto;

namespace cs_learn.application.services;

public interface IUsersCode
{
    string GenerateCode();
    void Save(UserCodeDTO userCode);
    UserCodeDTO FindByEmail(string email);
    void Delete(string email);
}