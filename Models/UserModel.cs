using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using BCrypt.Net;
using MongoDB.Driver;
using PlaneCards.Client;

namespace PlaneCards.Models
{

    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [BsonElement("Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; }

        //Async method to register users
        public async Task<bool> RegisterUserAsync(string email, string password)
        {
            //Pull up client
            IMongoClient mongoClient = MongoDBClientSingleton.Instance;
            var collection = mongoClient.GetDatabase("PlaneCards").GetCollection<User>("Users");

            //set up filter to find previously registered email
            var filter = Builders<User>.Filter.Eq("Email", email);
            var existingUser = await collection.Find(filter).FirstOrDefaultAsync();

            //Check if email is already registered
            if (existingUser != null)
            {
                return false; //Email registered, return false
            }

            //If no email registered to that, hash password before storing.
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            //Create a new user document to store into MongoDB
            var newUser = new User { Email = email, PasswordHash = passwordHash };

            //Insert user document into MongoDBCollection
            await collection.InsertOneAsync(newUser);

            //Successful Registration
            return true;
        }

        //Async method to authenticate users
        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            //Pull up client
            IMongoClient mongoClient = MongoDBClientSingleton.Instance;
            var collection = mongoClient.GetDatabase("PlaneCards").GetCollection<User>("Users");

            //set filter for email and find match
            var filter = Builders<User>.Filter.Eq("Email", email);
            var user = await collection.Find(filter).FirstOrDefaultAsync();

            //Check if email was found
            if (user != null)
            {
                //Verify the password, return false if not correct, return true if correct
                return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            }

            //If user not found,
            return false;
        }

    }

    
}
