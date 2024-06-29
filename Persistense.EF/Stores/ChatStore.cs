using Application.Interfaces;
using CSharpFunctionalExtensions;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistense.Stores;

public sealed class ChatStore : IChatStore
{
    private readonly ApplicationDBContext _context;

    public ChatStore(ApplicationDBContext context)
    {
        _context = context;
    }
    
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
            .AddAsync(chat);
        await _context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> UpdateName(Guid clientId, string newNickname)
    {
        var chat = await _context
            .Chats
            .FirstAsync(ex => ex.Id == clientId);

        var updateResult = chat.UpdateName(newNickname);

        if (updateResult.IsFailure)
        {
            return updateResult;
        }

        await _context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> Delete(Guid id)
    {
        var entity = await _context
            .Chats
            .FirstOrDefaultAsync(ex => ex.Id == id);

        if (entity is null)
        {
            return Result.Failure("dont contain entity with this id");
        }
        
        _context
            .Remove(entity);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}