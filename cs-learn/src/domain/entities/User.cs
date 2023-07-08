using cs_learn.core;
using cs_learn.domain.valueObjects;

namespace cs_learn.domain.entities;

public class User : Entity
{
    public readonly string Avatar;
    public readonly string Name;
    public readonly string Email;
    public readonly string Password;

    public User(string avatar, string name, string email, string password, EntityProps? entityProps = null) : base(entityProps)
    {
        this.Avatar = avatar;
        this.Name = new Name(name).Value;
        this.Email = new Email(email).Value;
        this.Password = new Password(password).Value;
    }
}