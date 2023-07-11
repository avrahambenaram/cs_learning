using cs_learn.application.repositories;
using cs_learn.application.services;
using cs_learn.application.services.dto;
using cs_learn.application.useCases.User.dto;
using cs_learn.errors;

namespace cs_learn.application.useCases.User;

public class CreateUser
{
    private readonly IUsersCode _usersCode;
    private readonly IUserRepository _userRepository;
    private readonly IMailer _mailer;
    private readonly IPasswordChecker _passwordChecker;

    public CreateUser(
        IUsersCode usersCode,
        IUserRepository userRepository,
        IMailer mailer,
        IPasswordChecker passwordChecker
    )
    {
        this._usersCode = usersCode;
        this._userRepository = userRepository;
        this._mailer = mailer;
        this._passwordChecker = passwordChecker;
    }

    public async Task Execute(CreateUserDto props)
    {
        await this.VerifyUserExistence(props.Email);
        if (this._passwordChecker.IsPasswordWeak(props.Password))
            throw new UseCaseError("Password too weak", 403);

        cs_learn.domain.entities.User user = new cs_learn.domain.entities.User(
            "",
            props.Name,
            props.Email,
            props.Password
        );
        this.GenerateUserCode(user, props);
        this.DeleteUserCodeAfter10Minutes(user.Email);
    }

    private async Task VerifyUserExistence(string userEmail)
    {
        try
        {
            domain.entities.User userFound = await this._userRepository.FindByEmail(userEmail);
            if (userFound != null)
                throw new UseCaseError("User already exists", 403);
        }
        catch (Exception err)
        {
            if (err is UseCaseError)
            {
                throw err;
            }
        }
    }

    private void GenerateUserCode(domain.entities.User user, CreateUserDto props)
    {
        string code = this._usersCode.GenerateCode();
        MailerDto mailProps = new MailerDto(user.Name, user.Email, code);
        this._mailer.SendConfirmationEmail(mailProps);

        UserCodeDto userCodeProps = new UserCodeDto(
            code,
            user, 
            props.Address,
            props.Redirect,
            props.FailureRedirect
            );
        this._usersCode.Save(userCodeProps);
    }

    private void DeleteUserCodeAfter10Minutes(string userEmail)
    {
        IUsersCode usersCode = this._usersCode;
        long minutes10 = 1000 * 60 * 10;
        System.Timers.Timer timer = new System.Timers.Timer();

        timer.Elapsed += (sender, e) =>
        {
            usersCode.Delete(userEmail);
            timer.Stop();
            timer.Dispose();
        };

        timer.Interval = minutes10;
        timer.AutoReset = false;
        timer.Start();
    }
}