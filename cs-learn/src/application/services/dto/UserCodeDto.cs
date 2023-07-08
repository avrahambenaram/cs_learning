using cs_learn.domain.entities;

namespace cs_learn.application.services.dto;

public record UserCodeDto(string Code, User User, string Redirect, string FailureRedirect, string Address);