using Application.DTO;
using Application.Interfaces;
using CommunityToolkit.HighPerformance.Helpers;
using CSharpFunctionalExtensions;
using Minio;
using Minio.DataModel.Args;

namespace Infrastructure.BlobStorage;

public class MinioFileStore  : IFileStore
{
    private readonly IMinioClient _minioClient;

    public MinioFileStore(IMinioClient client)
    {
        _minioClient = client;
    }
    
    private const string BucketName = "ChatDefault";
    
    public async Task<Result> Contain(string key, CancellationToken token)
    {
        try
        {
            var args = new GetObjectArgs()
                .WithBucket(BucketName)
                .WithFile(key);
        
            var file = await _minioClient.GetObjectAsync(args, token);

            if (file.DeleteMarker)
            {
                return Result.Failure("file was delete");
            }

            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }
    public async Task<Result<SavingFile>> GetFile(string key, CancellationToken token)
    {
        try
        {
            var outputeStream = Stream.Null;
            
            var args = new GetObjectArgs()
                .WithBucket(BucketName)
                .WithFile(key)
                .WithCallbackStream((stream) => outputeStream = stream);
            
            var file = await _minioClient.GetObjectAsync(args, token);

            if (file.DeleteMarker)
            {
                return Result.Failure<SavingFile>("file was delete");
            }

            var ResultFile = new SavingFile()
            {
                FileName = key,
                Size = file.Size,
                ContentType = file.ContentType,
                Content = outputeStream
            };
            
            return Result.Success(ResultFile);
        }
        catch (Exception e)
        {
            return Result.Failure<SavingFile>(e.Message);
        }
    }

    public async Task<Result<string>> SaveFile(SavingFile uploadingFile, CancellationToken token)
    {
        try
        {
            var args = new PutObjectArgs()
                .WithFileName(uploadingFile.FileName)
                .WithObjectSize(uploadingFile.Size)
                .WithBucket(BucketName)
                .WithUnModifiedSince(DateTime.Now)
                .WithContentType(uploadingFile.ContentType)
                .WithStreamData(uploadingFile.Content);
        
            var response = await _minioClient.PutObjectAsync(args, token).ConfigureAwait(false);

            return Result.Success<string>(response.ObjectName);
        }
        catch (Exception e)
        {
            return Result.Failure<string>(e.Message);
        }
    }

    public async Task<Result> DeleteFile(string key, CancellationToken token)
    {
        try
        {
            var args = new RemoveObjectArgs()
                .WithBucket(BucketName)
                .WithObject(key);
        
            await _minioClient.RemoveObjectAsync(args);

            return Result.Success();
        }
        catch (Exception e)
        {
            return Result.Failure(e.Message);
        }
    }
}