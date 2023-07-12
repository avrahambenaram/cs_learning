using cs_learn.application.repositories.implementations;
using cs_learn.application.services.implementations;
using cs_learn.application.useCases.User;
using cs_learn.domain.entities;
using cs_learn.errors;
using cs_learn.Tests.utils;

namespace cs_learn.Tests.application.useCases;

[TestFixture]
public class UserRecoverTest
{
    private InvalidFlowError _invalidFlow;
    private UserRepositoryMemory _userRepository;
    private UsersCode _usersCode;
    private MailerSpy _mailer;
    private UserRecover _userRecover;

    [SetUp]
    public async Task SetUp()
    {
        this._invalidFlow = new InvalidFlowError();
        this._userRepository = new UserRepositoryMemory();
        this._usersCode = new UsersCode();
        this._mailer = new MailerSpy();
        this._userRecover = new UserRecover(
            this._userRepository,
            this._usersCode,
            this._mailer
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
    public async Task TestForUserNotFound()
    {
        try
        {
            await this._userRecover.Execute("email@example.com");
            this._invalidFlow.Generate();
        }
        catch (Exception err)
        {
            Assert.IsInstanceOf<RepositoryError>(err);
        }
    }

    [Test]
    public async Task TestEmailingSuccess()
    {
        await this._userRecover.Execute("avraham@gmail.com");

        var code = this._usersCode.FindByEmail("avraham@gmail.com");
        
        Assert.That(code.User.Email, Is.EqualTo("avraham@gmail.com"));
    }
}