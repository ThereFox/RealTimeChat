using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistense;

public class ApplicationDBContext : DbContext
{
    public DbSet<Chat> Chats { get; private set; }
    public DbSet<Client> Client { get; private set; }
    public DbSet<SendedMessage> Messages { get; private set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
    }

    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
    }
}