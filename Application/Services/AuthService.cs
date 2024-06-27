using CSharpFunctionalExtensions;
using Domain;

namespace Application.Services;

public class AuthService
{
    public Task<Result> Registrate()
    {}

    public Task<Result<Client>> Auth();
    public Task<Result<Client>> GetCurrentUserInfo(){}
}