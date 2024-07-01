using Application.Interfaces;
using Infrastructure.BlobStorage.Configs;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Infrastructure.BlobStorage;

public static class DI
{
    public static IServiceCollection AddMinioS3(this IServiceCollection collection, S3Config config)
    {
        
        collection.AddMinio(
            configureClient => configureClient
                .WithEndpoint(config.endpointUrl)
                .WithCredentials(config.publicKey, config.privateKey)
                .Build());
        
        collection.AddScoped<IFileStore, MinioFileStore>();
        
        return collection;
    }
}