using CoreCRUD.Infrastructure.Repository;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace CoreCRUD.Infrastructure.DbContext
{
    public class MongoContext : IDbContext
    {

        private IConfiguration Configuration { get; set; }
        private readonly IMongoDatabase _MongoDatabase;

        public MongoContext(IConfiguration configuration)
        {
            this.Configuration = configuration.GetSection("Mongo");

            //MongoUrl url = new MongoUrl(this.Configuration.GetSection("ConnectionString").Value);
            //MongoClient client = new MongoClient(url);
            //_MongoDatabase = client.GetDatabase(this.Configuration.GetSection("DataBase").Value);

            MongoUrl url = new MongoUrl("mongodb://fabricioveronez:123456@ds113835.mlab.com:13835/core-crud");
            MongoClient client = new MongoClient(url);
            _MongoDatabase = client.GetDatabase("core-crud");
        }

        public IMongoDatabase Context => _MongoDatabase;
    }
}
