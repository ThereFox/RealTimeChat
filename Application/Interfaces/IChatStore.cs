using CSharpFunctionalExtensions;
using Domain;

namespace Application.Interfaces;

public interface IChatStore
{
    public Task<List<Chat>> GetAllWithPagination(int pageNumber, int pageSize);
    public Task<Result> Contain(Guid id);
    public Task<Result<Chat>> GetById(Guid id);
    public Task<Result> Create(Chat chat);
    public Task<Result> UpdateName(Chat chat);
    public Task<Result> Delite(Guid id);
}