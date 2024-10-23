namespace FinManagerGf.Shared.Dto
{
    public record AuthTokenDto(string? Token, bool Succeeded, List<string>? Errors, UserDto? UserDto);
}
