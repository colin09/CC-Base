using C.B.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace C.B.Mongo.data {

    [BsonIgnoreExtraElements]
    public class MgContent : MgBaseModel {
        public string Title { set; get; }
        public string Content { set; get; }
        public string Author { set; get; }
        public string Url { set; get; }

    }

}