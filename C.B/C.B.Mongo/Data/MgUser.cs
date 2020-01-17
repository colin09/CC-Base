using C.B.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace C.B.Mongo.data
{

    [BsonIgnoreExtraElements]
    public class MgUser : MgBaseModel
    {
        public string UserName { set; get; }
        public string MobileNo { set; get; }
        public string Password { set; get; }

        public UserRoleType Role { set; get; }
        public int Satte { set; get; }

    }
    
}
