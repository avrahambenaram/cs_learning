using cs_learn.application.repositories.dto;
using cs_learn.domain.entities;

namespace cs_learn.application.repositories;

public interface IUserRepository
{
    Task<User> FindById(string userId);
    Task<User> FindByEmail(string email);
    Task Update(string userId, UserUpdateDTO props);
    Task Save(User user);
    Task Delete(string userId);
}