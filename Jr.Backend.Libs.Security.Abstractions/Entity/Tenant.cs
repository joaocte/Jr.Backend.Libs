﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Jr.Backend.Libs.Security.Abstractions.Entity
{
    public class Tenant
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid ClientId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid ClientSecret { get; set; }

        public string Status { get; set; }
        public string TenantName { get; set; }
        public string TenantKey { get; set; }
    }
}