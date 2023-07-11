using cs_learn.application.services.dto;

namespace cs_learn.application.services.implementations;

public class MailerSpy : IMailer
{
    public List<MailerDto> ConfirmationEmails = new List<MailerDto>();
    public List<MailerDto> RecoverEmails = new List<MailerDto>();
    
    public async Task SendConfirmationEmail(MailerDto props)
    {
        await Task.Delay(1);
        this.ConfirmationEmails.Add(props);
    }

    public async Task SendRecoverEmail(MailerDto props)
    {
        await Task.Delay(1);
        this.RecoverEmails.Add(props);
    }
}