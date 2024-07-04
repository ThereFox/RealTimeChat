using Application.DTO;
using CSharpFunctionalExtensions;

namespace Application.Interfaces;

public interface IFileStore
{
    public Task<Result> Contain(string key, CancellationToken token);
    public Task<Result<SavingFile>> GetFile(string key, CancellationToken token);
    public Task<Result<string>> SaveFile(SavingFile uploadingFile, CancellationToken token);
    public Task<Result> DeleteFile(string key, CancellationToken token);
}