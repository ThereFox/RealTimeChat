using Application.DTO;
using CSharpFunctionalExtensions;

namespace Application.Interfaces;

public interface IFileStore
{
    public Result Contain(Guid key);
    public Result<FileContent> GetFile(Guid key);
    public Result<Guid> SaveFile(FileContent content);
    public Result DeleteFile(Guid key);
}