//using Middleware;
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;

//namespace DemoDocker.Auth.API.Model;

//public class User
//{
//    public static readonly string DocumentName = nameof(User);

//    [BsonId]
//    [BsonRepresentation(BsonType.ObjectId)]
//    public string? Id { get; init; }
//    public required string Email { get; init; }
//    public required string Password { get; set; }
//    public bool IsAdmin { get; init; }

//    public void SetPassword(string password, IEncryptor encryptor)
//    {
//        Password = encryptor.GetHash(password);
//    }

//    public bool ValidatePassword(string password, IEncryptor encryptor) =>
//        Password == encryptor.GetHash(password);
//}