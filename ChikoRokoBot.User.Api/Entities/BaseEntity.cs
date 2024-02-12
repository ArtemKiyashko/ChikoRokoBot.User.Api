using System;
using Azure;
using Azure.Data.Tables;

namespace ChikoRokoBot.User.Api.Entities
{
	public class BaseEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}

