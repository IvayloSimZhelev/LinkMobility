using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Enitities
{
    public class Invoice
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }        
        public string? InvoiceNumber { get; set; }
        public DateTime? Date { get; set; }
        public decimal Total { get; set; }
    }
}