using MongoDB.Driver;

namespace PlaneCards.Client { 

    //Use Singleton method to create MongoDB client to allow access to server without reusing connection string
    public sealed class MongoDBClientSingleton
    {
        private static readonly IMongoClient _client;
        private static readonly string _connectionString = "mongodb+srv://CAnderson1:DaemonAnim1@gcu-cst345.qhfd8wl.mongodb.net/";

        static MongoDBClientSingleton()
        {
            _client = new MongoClient(_connectionString);
        }

        public static IMongoClient Instance => _client;
    }
}
