using cs_learn.application.repositories;
using cs_learn.application.services;
using cs_learn.application.services.dto;
using Timer = System.Timers.Timer;

namespace cs_learn.application.useCases.User;

public class UserRecover
{
    private readonly IUserRepository _userRepository;
    private readonly IUsersCode _usersCode;
    private readonly IMailer _mailer;

    public UserRecover(IUserRepository userRepository, IUsersCode usersCode, IMailer mailer)
    {
        this._userRepository = userRepository;
        this._usersCode = usersCode;
        this._mailer = mailer;
    }

    public async Task Execute(string userEmail)
    {
        domain.entities.User user = await this._userRepository.FindByEmail(userEmail);

        await this.GenerateUserCode(user);
        
        this.DeleteUserCodeAfter10Minutes(user.Email);
    }

    async Task GenerateUserCode(domain.entities.User user)
    {
        string code = this._usersCode.GenerateCode();
        
        UserCodeDto userCode = new UserCodeDto(code, user, "", "", "");
        this._usersCode.Save(userCode);

        MailerDto mailerProps = new MailerDto(user.Name, user.Email, code);
        await this._mailer.SendRecoverEmail(mailerProps);
    }

    void DeleteUserCodeAfter10Minutes(string userEmail)
    {
        IUsersCode usersCode = this._usersCode;
        long minutes10 = 1000 * 60 * 10;
        var timer = new Timer();
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