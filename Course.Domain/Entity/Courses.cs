using LearningManagement.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Domain.Entity
{
    public class Courses : AuditableEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonRequired]
        public string Id { get; set; }
        [BsonRequired]
        public string Name { get; set; }
        [BsonRequired]
        public string Description { get; set; }
        [BsonRequired]
        public int Duration { get; set; }
        [BsonRequired]
        public string Technology { get; set; }
        [BsonRequired]
        public string LaunchURL { get; set; }
    }
}
