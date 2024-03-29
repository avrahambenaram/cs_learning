using cs_learn.application.repositories;
using cs_learn.application.repositories.implementations;
using cs_learn.application.services;
using cs_learn.application.services.implementations;

namespace cs_learn.infra.implementations;

public class ProvidersFactoryTest : IProvidersFactory
{
    private UserRepositoryMemory? _userRepository = null;
    private EncrypterSpy? _encrypter = null;
    private MailerSpy? _mailer = null;
    private PasswordCheckerSpy? _passwordChecker = null;
    private UsersCode? _usersCode = null;

    public IUserRepository CreateUserRepository()
    {
        this._userRepository = this._userRepository ?? new UserRepositoryMemory();
        return this._userRepository;
    }

    public IEncrypter CreateEncrypter()
    {
        this._encrypter = this._encrypter ?? new EncrypterSpy();
        return this._encrypter;
    }

    public IMailer CreateMailer()
    {
        this._mailer = this._mailer ?? new MailerSpy();
        return this._mailer;
    }

    public IPasswordChecker CreatePasswordChecker()
    {
        this._passwordChecker = this._passwordChecker ?? new PasswordCheckerSpy();
        return this._passwordChecker;
    }

    public IUsersCode CreateUsersCode()
    {
        this._usersCode = this._usersCode ?? new UsersCode();
        return this._usersCode;
    }
}