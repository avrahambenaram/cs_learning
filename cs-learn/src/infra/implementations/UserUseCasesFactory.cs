using cs_learn.application.useCases.User;

namespace cs_learn.infra.implementations;

public class UserUseCasesFactory : IUserUseCasesFactory
{
    private readonly IProvidersFactory _providers;
    private AuthUser? _authUser;
    private CreateUser? _createUser;
    private CreateUserConfirm? _createUserConfirm;
    private UserRecover? _userRecover;
    private UserRecoverConfirm? _userRecoverConfirm;

    public UserUseCasesFactory(IProvidersFactory providers)
    {
        this._providers = providers;
    }
    
    public AuthUser CreateAuthUser()
    {
        this._authUser = this._authUser ?? new AuthUser(
            this._providers.CreateUserRepository(),
            this._providers.CreateEncrypter()
            );
        return this._authUser;
    }
    
    public CreateUser CreateCreateUser()
    {
        this._createUser = this._createUser ?? new CreateUser(
            this._providers.CreateUsersCode(),
            this._providers.CreateUserRepository(),
            this._providers.CreateMailer(),
            this._providers.CreatePasswordChecker()
        );
        return this._createUser;
    }
    
    public CreateUserConfirm CreateUserConfirm()
    {
        this._createUserConfirm = this._createUserConfirm ?? new CreateUserConfirm(
            this._providers.CreateUsersCode(),
            this._providers.CreateEncrypter(),
            this._providers.CreateUserRepository()
        );
        return this._createUserConfirm;
    }
    
    public UserRecover CreateUserRecover()
    {
        this._userRecover = this._userRecover ?? new UserRecover(
            this._providers.CreateUserRepository(),
            this._providers.CreateUsersCode(),
            this._providers.CreateMailer()
        );
        return this._userRecover;
    }
    
    public UserRecoverConfirm CreateUserRecoverConfirm()
    {
        this._userRecoverConfirm = this._userRecoverConfirm ?? new UserRecoverConfirm(
            this._providers.CreateUserRepository(),
            this._providers.CreateUsersCode(),
            this._providers.CreateEncrypter()
        );
        return this._userRecoverConfirm;
    }
}