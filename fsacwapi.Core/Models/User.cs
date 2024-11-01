using fsacwapi.Core.Enums;

namespace fsacwapi.Core.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? ProfilePictureUrl { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public Role Role { get; set; } = Enums.Role.USER;
}
