using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using fraturas_csharp.Data.DTOs;
using fraturas_csharp.Models;
using fraturas_csharp.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;

namespace fraturas_csharp.Services;

public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;

    public LoginService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IResponseMessages Authenticate(string username, string password)
    {
        var loggedUser = Login(username, password);
        if (loggedUser == null)
            return new ResponseMessages(
                message: "Usuário ou senha incorreto(s)",
                statusCode: HttpStatusCode.NoContent);

        return new ResponseMessages
        (
            new
            {
                User = loggedUser,
                Token = GenerateJwtToken(username, "Usuário")
            }
        );
    }
    private string GenerateJwtToken(string username, string role)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        string? tokenKey = Environment.GetEnvironmentVariable("tokenKey", EnvironmentVariableTarget.User);
        if (string.IsNullOrEmpty(tokenKey))
            throw new NullReferenceException("Token nulo!");

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
            Issuer = "FraturaAPI",
            Audience = "Anônima",
            Expires = DateTime.UtcNow.AddHours(1), // Tempo de expiração do token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenKey)), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    private UserViewModel? Login(string username, string password)
    {
        return _userRepository
            .Where(x =>
                x.Name == username &&
                x.Password == password)
            .Select(x =>
                new UserViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
            .FirstOrDefault();

    }
}
