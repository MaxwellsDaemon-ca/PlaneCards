using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using PlaneCards.Client;

namespace PlaneCards.Models
{
    public class ProductCardModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }


        [BsonElement("CardName")]
        [Required]
        public string CardName { get; set; }
        [BsonElement("CardType")]
        [Required]
        public string CardType { get; set; }
        [BsonElement("CardSubtype")]
        public string CardSubtype { get; set; }
        [BsonElement("CardColor")]
        [Required]
        public string CardColor { get; set; }
        [BsonElement("CardEdition")]
        [Required]
        public string CardEdition { get; set; }
        [BsonElement("CardOracle")]
        public string CardOracle { get; set; }
        [BsonElement("CardConvertedManaCost")]
        [Required]
        public int CardConvertedManaCost { get; set; }

        public ProductCardModel() { }

        public ProductCardModel(string name, string type, string sub, string color, string edition, string oracle, int manacost)
        {
            CardName = name;
            CardType = type;
            CardSubtype = sub;
            CardColor = color;
            CardEdition = edition;
            CardOracle = oracle;
            CardConvertedManaCost = manacost;
        }

      
    }
}
