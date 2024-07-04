using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Minio;

namespace Infrastructure.BlobStorage;

public static class DI
{
    public static IServiceCollection AddMinioS3(this IServiceCollection collection, string PublicKey, string PrivateKey)
    {
        
        collection.AddMinio(
            configureClient => configureClient
                .WithCredentials(PublicKey, PrivateKey)
                .Build());
        
        collection.AddScoped<IFileStore, MinioFileStore>();
        
        return collection;
    }
}