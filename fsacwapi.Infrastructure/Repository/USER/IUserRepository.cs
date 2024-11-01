using fsacwapi.Core.Abstractions;
using fsacwapi.Core.Models;
using fsacwapi.Core.DTOs.UserDTO.Request;

namespace fsacwapi.Infrastructure.Repository.USER;

public interface IUserRepository
{
    Task<Result> AddUser(RegisterRequestDTO request);
    Task<(Result Result, User? User)> GetUserByEmail(string email);
}