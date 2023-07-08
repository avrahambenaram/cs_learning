using cs_learn.application.repositories;
using cs_learn.application.repositories.dto;
using cs_learn.application.services;
using cs_learn.application.services.dto;
using cs_learn.application.useCases.User.dto;

namespace cs_learn.application.useCases.User;

public class UserRecoverConfirm
{
    private readonly IUserRepository _userRepository;
    private readonly IUsersCode _usersCode;
    private readonly IEncrypter _encrypter;
    private readonly UserRecoverConfirmResponseDto _result = new UserRecoverConfirmResponseDto
    {
        Message = "",
        Success = false,
        UserCode = null
    };

    public UserRecoverConfirm(IUserRepository userRepository, IUsersCode usersCode, IEncrypter encrypter)
    {
        this._userRepository = userRepository;
        this._usersCode = usersCode;
        this._encrypter = encrypter;
    }

    public async Task<UserRecoverConfirmResponseDto> Execute(UserRecoverConfirmDto props)
    {
        domain.entities.User user = await this._userRepository.FindByEmail(props.Email);
        UserCodeDto userCode = this._usersCode.FindByEmail(user.Email);
        await this.CheckUserCode(userCode, user, props);
        
        this._result.UserCode = userCode;
        return this._result;
    }

    async Task CheckUserCode(
        UserCodeDto userCode,
        domain.entities.User user,
        UserRecoverConfirmDto props
    )
    {
        bool isUserCodeValid = userCode.Code == props.Code;
        if (isUserCodeValid)
        {
            await this.UpdateUserPassword(props, user);
            return;
        }

        this._result.Message = "Invalid code";
    }

    private async Task UpdateUserPassword(
        UserRecoverConfirmDto props,
        domain.entities.User user
    )
    {
        var userUpdated = new domain.entities.User("", user.Name, user.Email, props.NewPassword);

        UserUpdateDto userUpdatedProps = new UserUpdateDto
        {
            Password = this._encrypter.Hash(userUpdated.Password)
        };
        await this._userRepository.Update(user.id, userUpdatedProps);
        
        this._usersCode.Delete(user.Email);
    }
}