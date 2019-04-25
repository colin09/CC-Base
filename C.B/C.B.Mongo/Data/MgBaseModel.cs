using C.B.Common.Config;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace C.B.Mongo.data
{
    public class MgBaseModel
    {
        public MgBaseModel()
        {
            _id = CreateObjectId();
        }



        public ObjectId _id { get; set; }

        public string StringId => _id.ToString();


        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { set; get; } = DateTime.Now;

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime UpdateTime { set; get; } = DateTime.Now;

        public bool IsDelete { set; get; } = false;




        public ObjectId CreateObjectId()
        {
            return new ObjectId(Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32).ToLower());
        }
    }


    public class counters
    {
        public string _id { set; get; }
        public int seq { set; get; }
    }















    public static class MgBaseModelExt
    {
        public static string GetCollectionName(this MgBaseModel model)
        {
            return string.Format("{0}_{1}", AppSettingConfig.MgPrefix, model.GetType().Name.ToLower());
        }
    }







}
