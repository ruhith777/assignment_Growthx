using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Growthx_assignment.Models
{
	public class User
	{
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        //its a field like in mongodb element
        public string? Id { get; set; }

        [BsonElement("username")]
        public string? UserName { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }

        [BsonElement("role")]
        public string? Role { get; set; }
    }
}

