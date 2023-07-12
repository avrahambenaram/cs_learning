using cs_learn.application.repositories.implementations;
using cs_learn.application.services.dto;
using cs_learn.application.services.implementations;
using cs_learn.application.useCases.User;
using cs_learn.application.useCases.User.dto;
using cs_learn.domain.entities;

namespace cs_learn.Tests.application.useCases;

[TestFixture]
public class UserRecoverConfirmTest
{
    private UserRepositoryMemory _userRepository;
    private UsersCode _usersCode;
    private EncrypterSpy _encrypter;
    private UserRecoverConfirm _userRecoverConfirm;
    private string _code;
    private User _user;
    private UserCodeDto _userCode;

    [SetUp]
    public async Task SetUp()
    {
        this._userRepository = new UserRepositoryMemory();
        this._usersCode = new UsersCode();
        this._encrypter = new EncrypterSpy();
        this._userRecoverConfirm = new UserRecoverConfirm(
            this._userRepository,
            this._usersCode,
            this._encrypter
            );

        this._user = new User(
            "",
            "Avraham",
            "avraham@gmail.com",
            "User_password123"
        );
        await this._userRepository.Save(this._user);

        this._code = this._usersCode.GenerateCode();
        this._userCode = new UserCodeDto(
            this._code,
            this._user,
            "",
            "",
            ""
            );
        this._usersCode.Save(this._userCode);
    }

    [Test]
    public async Task TestForInvalidCode()
    {
        var props = new UserRecoverConfirmDto(this._user.Email, "987654", "User_password321");
        var result = await this._userRecoverConfirm.Execute(props);
        
        Assert.IsFalse(result.Success);
    }

    [Test]
    public async Task TestForSuccessRecovering()
    {
        var props = new UserRecoverConfirmDto(this._user.Email, this._userCode.Code, "User_password321");
        var result = await this._userRecoverConfirm.Execute(props);
        
        Assert.IsTrue(result.Success);
    }
}