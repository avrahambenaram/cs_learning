using cs_learn.application.services.dto;

namespace cs_learn.application.services;

public interface IMailer
{
    public Task sendConfirmationEmail(MailerDTO props);
}