using Application.Interfaces;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Stores;

public sealed class ChatStore : IChatStore
{
    private readonly ApplicationDBContext _context;
    
    public async Task<List<Chat>> GetAllWithPagination(int pageNumber, int pageSize)
    {
        return await _context
            .Chats
            .AsNoTracking()
            .OrderByDescending(ex => ex.Members.Count)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Result> Contain(Guid id)
    {
       return (await _context
            .Chats
            .AnyAsync(ex => ex.Id == id))
           ?
            Result.Success()
           :
            Result.Failure("don have element with this id");
    }

    public async Task<Result<Chat>> GetById(Guid id)
    {
        if ((await Contain(id)).IsFailure)
        {
            return Result.Failure<Chat>("dont contain element");
        }

        return Result.Success<Chat>(
                await _context
                    .Chats
                    .FirstAsync(ex => ex.Id == id)
                );
    }

    public async Task<Result> Create(Chat chat)
    {
        await _context
            .Chats
            .AddAsync(chat);
        
        return Result.Success();
    }

    public Task<Result> UpdateName(Chat chat)
    {
        throw new NotImplementedException();
    }

    public Task<Result> Delite(Guid id)
    {
        throw new NotImplementedException();
    }
}