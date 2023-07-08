using cs_learn.application.repositories;
using cs_learn.application.services;
using cs_learn.application.useCases.User.dto;
using cs_learn.errors;

namespace cs_learn.application.useCases.User;

public class AuthUser
{

    private readonly IUserRepository _userRepository;
    private readonly IEncrypter _encrypter;
    
    public AuthUser(IUserRepository userRepository, IEncrypter encrypter)
    {
        this._userRepository = userRepository;
        this._encrypter = encrypter;
    }

    public async Task<domain.entities.User> Execute(AuthUserDto props)
    {
        domain.entities.User user = await this._userRepository.FindByEmail(props.Email);
        bool isPasswordRight = this._encrypter.Compare(props.Password, user.Password);
        if (!isPasswordRight)
            throw new UseCaseError("Invalid password", 401);
        return user;
    }
}