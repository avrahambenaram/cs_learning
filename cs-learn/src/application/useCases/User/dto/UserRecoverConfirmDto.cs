namespace cs_learn.application.useCases.User.dto;

public record UserRecoverConfirmDto(string Email, string Code, string NewPassword);