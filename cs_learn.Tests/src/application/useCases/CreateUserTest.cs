using cs_learn.application.repositories.implementations;
using cs_learn.application.services.implementations;
using cs_learn.application.useCases.User;
using cs_learn.application.useCases.User.dto;
using cs_learn.domain.entities;
using cs_learn.errors;
using cs_learn.Tests.utils;

namespace cs_learn.Tests.application.useCases;

[TestFixture]
public class CreateUserTest
{
    private InvalidFlowError _invalidFlow;
    private UsersCode _usersCode;
    private UserRepositoryMemory _userRepository;
    private MailerSpy _mailer;
    private PasswordCheckerSpy _passwordChecker;
    private CreateUser _createUser;

    [SetUp]
    public async Task SetUp()
    {
        this._invalidFlow = new InvalidFlowError();
        this._usersCode = new UsersCode();
        this._userRepository = new UserRepositoryMemory();
        this._mailer = new MailerSpy();
        this._passwordChecker = new PasswordCheckerSpy();
        this._createUser = new CreateUser(
            this._usersCode,
            this._userRepository,
            this._mailer,
            this._passwordChecker
            );
        
        var user = new User(
            "",
            "Avraham",
            "avraham@gmail.com",
            "User_password123"
        );
        await this._userRepository.Save(user);
    }

    [Test]
    public async Task TestForInvalidEmail()
    {
        try
        {
            CreateUserDto props = new CreateUserDto(
                "Any_user_name",
                "jy456nu465",
                "User_password123",
                "",
                "",
                "any_address"
            );
            await _createUser.Execute(props);
            this._invalidFlow.Generate();
        }
        catch (Exception err)
        {
            Assert.IsInstanceOf<ValueObjectError>(err);
        }
    }

    [Test]
    public async Task TestForNameTooLong()
    {
        try
        {
            CreateUserDto props = new CreateUserDto(
                "A too long name for throwing an expected error",
                "email@example.com",
                "User_password123",
                "",
                "",
                "any_address"
            );
            await _createUser.Execute(props);
            this._invalidFlow.Generate();
        }
        catch (Exception err)
        {
            Assert.IsInstanceOf<ValueObjectError>(err);
        }
    }

    [Test]
    public async Task TestForUserAlreadyExists()
    {
        try
        {
            CreateUserDto props = new CreateUserDto(
                "Avraham",
                "avraham@gmail.com",
                "User_password123",
                "",
                "",
                "any_address"
            );
            await this._createUser.Execute(props);
            this._invalidFlow.Generate();
        }
        catch (Exception err)
        {
            Assert.IsInstanceOf<UseCaseError>(err);
        }
    }

    [Test]
    public async Task TestCreatingUserSuccessfully()
    {
        CreateUserDto props = new CreateUserDto(
            "Avraham",
            "user@example.com",
            "User_password123",
            "",
            "",
            "any_address"
        );
        await this._createUser.Execute(props);

        var code = this._usersCode.FindByEmail("user@example.com");
        
        Assert.That(code.User.Email, Is.EqualTo("user@example.com"));
    }
}