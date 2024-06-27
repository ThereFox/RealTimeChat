using Application.DTO;
using Application.Interfaces;
using CSharpFunctionalExtensions;

namespace Infrastructure.BlobStorage;

public class MinioFileStore  : IFileStore
{
    public Result Contain(Guid key)
    {
        throw new NotImplementedException();
    }

    public Result<FileContent> GetFile(Guid key)
    {
        throw new NotImplementedException();
    }

    public Result<Guid> SaveFile(FileContent content)
    {
        throw new NotImplementedException();
    }

    public Result DeleteFile(Guid key)
    {
        throw new NotImplementedException();
    }
}