using cs_learn.application.repositories;
using cs_learn.application.services;
using cs_learn.application.services.dto;
using cs_learn.application.useCases.User.dto;

namespace cs_learn.application.useCases.User;

public class CreateUserConfirm
{
    private CreateUserConfirmResponseDto _result = new CreateUserConfirmResponseDto
    {
        Success = false,
        Message = "",
        UserCode = null
    };

    private IUsersCode _usersCode;
    private IEncrypter _encrypter;
    private IUserRepository _userRepository;

    public CreateUserConfirm(IUsersCode usersCode, IEncrypter encrypter, IUserRepository userRepository)
    {
        this._usersCode = usersCode;
        this._encrypter = encrypter;
        this._userRepository = userRepository;
    }

    async Task<CreateUserConfirmResponseDto> Execute(CreateUserConfirmDto props)
    {
        UserCodeDto userCode = this._usersCode.FindByEmail(props.Email);
        await this.ValidateUserCode(userCode, props);
        return this._result;
    }

    private async Task ValidateUserCode(UserCodeDto userCode, CreateUserConfirmDto props)
    {
        bool isUserCodeValid = userCode.Code == props.Code;
        if (isUserCodeValid)
        {
            await this.CreateUser(userCode);
        }
        else
        {
            this._result.Message = "Código inválido";
        }
    }

    private async Task CreateUser(UserCodeDto userCode)
    {
        this._result.Success = true;

        domain.entities.User user = new domain.entities.User(
            "",
            userCode.User.Name,
            userCode.User.Email,
            this._encrypter.Hash(userCode.User.Password)
        );
        await this._userRepository.Save(user);
        this._usersCode.Delete(user.Email);
    }
}