using Microsoft.AspNetCore.Http;

namespace fsacwapi.Core.DTOs.UserDTO.Request;

public class RegisterRequestDTO
{
    public string? ProfilePictureBase64 { get; init; }
    public string? Name { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }
    public string? ConfirmPassword { get; init; }
}