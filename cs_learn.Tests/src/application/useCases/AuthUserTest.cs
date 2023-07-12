using cs_learn.application.repositories.implementations;
using cs_learn.application.services.implementations;
using cs_learn.application.useCases.User;
using cs_learn.application.useCases.User.dto;
using cs_learn.domain.entities;
using cs_learn.errors;
using cs_learn.Tests.utils;

namespace cs_learn.Tests.application.useCases;

[TestFixture]
public class AuthUserTest
{
    private InvalidFlowError _invalidFlow;
    private UserRepositoryMemory _userRepository;
    private EncrypterSpy _encrypter;
    private AuthUser _authUser;
    
    [SetUp]
    public async Task SetUp()
    {
        this._invalidFlow = new InvalidFlowError();
        this._userRepository = new UserRepositoryMemory();
        this._encrypter = new EncrypterSpy();
        this._authUser = new AuthUser(
            this._userRepository,
            this._encrypter
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
    public async Task TestForIncorrectPassword()
    {
        try
        {
            var props = new AuthUserDto("avraham@gmail.com", "user_wong_password");
            await this._authUser.Execute(props);
            this._invalidFlow.Generate();
        }
        catch (Exception err)
        {
            Assert.IsInstanceOf<UseCaseError>(err);
        }
    }
    
    [Test]
    public async Task TestForAuthenticationSuccess()
    {
        var props = new AuthUserDto("avraham@gmail.com", "User_password123");
        var user = await this._authUser.Execute(props);
        
        Assert.That(user.Email, Is.EqualTo("avraham@gmail.com"));
    }
}