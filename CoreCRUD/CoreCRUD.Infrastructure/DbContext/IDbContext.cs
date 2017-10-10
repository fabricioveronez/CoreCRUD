using MongoDB.Driver;

namespace CoreCRUD.Infrastructure.Repository
{
    public interface IDbContext
    {
        IMongoDatabase Context { get; }
    }
}
