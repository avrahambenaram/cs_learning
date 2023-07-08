using cs_learn.application.services.dto;

namespace cs_learn.application.useCases.User.dto;

public record UserRecoverConfirmResponseDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public UserCodeDto? UserCode { get; set; }
}