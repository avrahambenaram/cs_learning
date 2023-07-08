namespace cs_learn.application.useCases.User.dto;

public record CreateUserDto(string Name, string Email, string Password, string Redirect, string FailureRedirect, string Address);