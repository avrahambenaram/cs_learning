namespace cs_learn.application.repositories.dto;

public record UserUpdateDto
{
    public string? Avatar { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}