using cs_learn.domain.entities;

namespace cs_learn.application.services.dto;

public class UserCodeDTO
{
    public string Code;
    public User User;
    public string Redirect;
    public string FailureRedirect;
    public string Address;

    public UserCodeDTO(string code, User user, string redirect, string failureRedirect, string address)
    {
        this.Code = code;
        this.User = user;
        this.Redirect = redirect;
        this.FailureRedirect = failureRedirect;
        this.Address = address;
    }
}