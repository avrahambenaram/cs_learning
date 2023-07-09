using cs_learn.application.repositories;
using cs_learn.application.services;

namespace cs_learn.infra;

public interface IProviders
{
    IUserRepository CreateUserRepository();
    IEncrypter CreateEncrypter();
    IMailer CreateMailer();
    IPasswordChecker CreatePasswordChecker();
    IUsersCode CreateUsersCode();
}