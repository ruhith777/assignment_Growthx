using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Growthx_assignment.Models
{
	public class Assignment
	{
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        //its a field like in mongodb element
        public string? Id { get; set; }

        [BsonElement("userId")]
        public string? Userid { get; set; }


        [BsonElement("task")]
        public string? Task { get; set; }

        [BsonElement("adminId")]
        public string? AdminId { get; set; }


        [BsonElement("role")]
        public string? Role { get; set; }

        [BsonElement("status")]
        public string? Status { get; set; } = "pending";

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}

