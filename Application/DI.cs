using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DI
{
    public static IServiceCollection AddAppServices(this IServiceCollection collection)
    {
        collection.AddScoped<MessagesService>();

        return collection;
    }
}