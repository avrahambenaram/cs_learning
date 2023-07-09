using cs_learn.application.useCases.User;

namespace cs_learn.infra;

public interface IUserUseCases
{
    AuthUser CreateAuthUser();
    CreateUser CreateCreateUser();
    CreateUserConfirm CreateUserConfirm();
    UserRecover CreateUserRecover();
    UserRecoverConfirm CreateUserRecoverConfirm();
}