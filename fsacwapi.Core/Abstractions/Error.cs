using fsacwapi.Core.Enums;

namespace fsacwapi.Core.Abstractions;

public sealed record Error(ErrorCode Code, string? Description = null)
{
    public static readonly Error None = new (ErrorCode.None, string.Empty);
    public static implicit operator Result(Error error) => Result.Failure(error);
}