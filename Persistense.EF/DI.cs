using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistense.Stores;

namespace Persistense;

public static class DI
{
    public static IServiceCollection AddEFPersistense(
        this IServiceCollection collection,
        Action<DbContextOptionsBuilder> DBConfiguration)
    {
        collection.AddDbContext<ApplicationDBContext>(DBConfiguration, ServiceLifetime.Scoped);

        collection.AddScoped<IChatStore, ChatStore>();
        collection.AddScoped<IClientsStore, ClientStore>();
        collection.AddScoped<IMessagesStore, MessageStore>();

        
        return collection;
    }
}