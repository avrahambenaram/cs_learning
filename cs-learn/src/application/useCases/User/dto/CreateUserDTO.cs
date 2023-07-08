namespace cs_learn.application.useCases.User.dto;

public class CreateUserDTO
{
    public string Name;
    public string Email;
    public string Password;
    public string Redirect;
    public string FailureRedirect;
    public string Address;

    public CreateUserDTO(string name, string email, string password, string redirect, string failureRedirect, string address)
    {
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.Redirect = redirect;
        this.FailureRedirect = failureRedirect;
        this.Address = address;
    }
}