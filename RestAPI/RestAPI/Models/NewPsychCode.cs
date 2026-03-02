using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RestAPI.Models
{
    public class NewPsychCode
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Code { get; set; }

        public bool IsUsed { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
