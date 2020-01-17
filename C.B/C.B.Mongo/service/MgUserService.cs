using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using C.B.Mongo.data;
using C.B.Models.Data;

namespace C.B.Mongo.service
{
    public class MgUserService : BaseService<MgUser>
    {

        public void Init()
        {
            CreateCounter();
        }

        public List<MgUser> SearchById(long id)
        {
            var filter = Builders<MgUser>.Filter.Eq("ID", id);
            return Search(filter);
        }

        /// <summary>
        /// 验证是否存在电话账号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<MgUser> SearchLogin(string code)
        {
            var filter = Builders<MgUser>.Filter.Eq("Phone", code);

            return Search(filter);
        }

        /// <summary>
        /// 查看所有用户
        /// </summary>
        /// <returns></returns>
        public List<MgUser> SearchAll()
        {
            return Search();
        }
        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public List<MgUser> SearchWhere(string phone, Pager pager)
        {
            var filter = Builders<MgUser>.Filter.Eq(p => p.MobileNo, phone);

            long total = 0;
            var result = SearchByPage(filter, order => order.CreateTime, false, pager.PageIndex, pager.PageSize, out total);
            pager.TotalCount = (int)total;
            return result;
        }


    }
}
