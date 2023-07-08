namespace cs_learn.application.services.dto;

public class MailerDTO
{
    public string Name;
    public string Email;
    public string Code;

    public MailerDTO(string name, string email, string code)
    {
        this.Name = name;
        this.Email = email;
        this.Code = code;
    }
}