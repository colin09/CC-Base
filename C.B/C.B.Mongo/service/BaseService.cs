using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using C.B.Mongo.data;
using C.B.Mongo.client;
using System.Linq.Expressions;

namespace C.B.Mongo.service
{
    public class BaseService<T> where T : MgBaseModel, new()
    {
        protected IMongoDatabase _database;

        public BaseService()
        {
            _database = MgClient.GetDB();
        }

        /// <summary>
        /// 创建实例T 的自增长起始值：0 
        /// 每个T(Model)只需调用一次
        /// </summary>
        public void CreateCounter()
        {
            MgClient.CreateDefaultCounter<T>();
        }





        public void Insert(T model)
        {
            MgClient.Insert<T>(model);
        }

        public void InsertMany(IEnumerable<T> models)
        {
            MgClient.InsertMany<T>(models);
        }

        public long Update(FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            return MgClient.Update<T>(filter, update);
        }

        protected long Delete(FilterDefinition<T> filter)
        {
            return MgClient.Delete<T>(filter);
        }










        protected List<T> Search(FilterDefinition<T> filter)
        {
            return MgClient.Search<T>(filter);
        }
        protected List<T> SearchByPage(FilterDefinition<T> filter, Expression<Func<T, object>> sort, bool isAsc, int pageIndex, int pageSize, out long total)
        {
            pageIndex = pageIndex > 0 ? pageIndex : 1;
            pageSize = pageSize > 0 ? pageSize : 12;

            return MgClient.Search<T>(filter,sort,isAsc,pageSize,pageIndex,out total);
        }

        protected List<T> Search()
        {
            return MgClient.Search<T>();
        }

        protected string Index(IndexKeysDefinition<T> indexKeys)
        {
            return MgClient.Index<T>(indexKeys);
        }

        protected List<BsonDocument> Aggregate(FilterDefinition<T> filter, ProjectionDefinition<T> group)
        {
            return MgClient.Aggregate(filter, group);
        }




        

    }
}
