using cs_learn.application.repositories.implementations;
using cs_learn.application.services.dto;
using cs_learn.application.services.implementations;
using cs_learn.application.useCases.User;
using cs_learn.application.useCases.User.dto;
using cs_learn.domain.entities;

namespace cs_learn.Tests.application.useCases;

[TestFixture]
public class CreateUserConfirmTest
{
    private UsersCode _usersCode;
    private EncrypterSpy _encrypter;
    private UserRepositoryMemory _userRepository;
    private CreateUserConfirm _createUserConfirm;
    private string _code;
    private User _user;

    [SetUp]
    public void SetUp()
    {
        this._usersCode = new UsersCode();
        this._encrypter = new EncrypterSpy();
        this._userRepository = new UserRepositoryMemory();
        this._createUserConfirm = new CreateUserConfirm(
            this._usersCode,
            this._encrypter,
            this._userRepository
            );
        this._user = new User(
            "",
            "Avraham",
            "avraham@gmail.com",
            "User_password123"
        );
        this._code = this._usersCode.GenerateCode();
        var userCode = new UserCodeDto(
            this._code,
            this._user,
            "",
            "",
            ""
        );
        this._usersCode.Save(userCode);
    }

    [Test]
    public async Task TestForNoEmailWaiting()
    {
        var props = new CreateUserConfirmDto("test@example.com", "987654");
        var result = await this._createUserConfirm.Execute(props);
        
        Assert.IsFalse(result.Success);
    }

    [Test]
    public async Task TestForInvalidCode()
    {
        var props = new CreateUserConfirmDto(this._user.Email, "987654");
        var result = await this._createUserConfirm.Execute(props);
        
        Assert.IsFalse(result.Success);
    }

    [Test]
    public async Task TestForUserCreationSuccess()
    {
        var props = new CreateUserConfirmDto(this._user.Email, this._code);
        var result = await this._createUserConfirm.Execute(props);
        
        Assert.IsTrue(result.Success);
    }
}