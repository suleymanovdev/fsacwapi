using fsacwapi.Core.Models;
using fsacwapi.Core.DTOs.UserDTO.Request;
using fsacwapi.Core.Validations;
using fsacwapi.Infrastructure.Data.Storage;
using fsacwapi.Infrastructure.Repository.USER;
using fsacwapi.Core.Exceptions.Get;
using fsacwapi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using fsacwapi.Core.Abstractions;
using fsacwapi.Core.Errors;

namespace fsacwapi.Infrastructure.Services.USER;

public class UserService : IUserRepository
{
    private readonly StorageService _storageService;
    private readonly DBContext _dbContext;

    public UserService(StorageService storageService, DBContext dBContext)
    {
        _storageService = storageService ?? throw new ArgumentNullException(nameof(storageService));
        _dbContext = dBContext ?? throw new ArgumentNullException(nameof(dBContext));
    }

    public async Task<Result> AddUser(RegisterRequestDTO request)
    {
        RegisterRequestValidation validation = new RegisterRequestValidation();

        if (!(await validation.ValidateAsync(request)).IsValid)
        {
            return AuthError.ValidationException;
        }

        if (request == null)
        {
            return AuthError.ArgumentNullException;
        }

        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            Password = request.Password
        };

        if (string.IsNullOrEmpty(request.ProfilePictureBase64) || request.ProfilePictureBase64 == "string")
        {
            return AuthError.RegistrationException;
        }
        
        await _storageService.UploadProfilePhotoAsync(user.Id, request.ProfilePictureBase64);
        user.ProfilePictureUrl = await _storageService.GetProfilePhotoUrlAsync(user.Id);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
        return Result.Success();
    }
    
    

    public async Task<(Result Result, User? User)> GetUserByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return (AuthError.GetException, null);
        }

        User? user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            return (AuthError.GetException, null);
        }

        return (Result.Success(), user);
    }
}