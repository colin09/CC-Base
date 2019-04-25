using System;
using System.Collections.Generic;
using System.Linq;
using C.B.Models.Data;
using C.B.Mongo.data;
using MongoDB.Driver;

namespace C.B.Mongo.service {
    public class MgContentService : BaseService<MgContent> {

        public void Init () {
            CreateCounter ();
        }

        public List<MgContent> SearchById (long id) {
            var filter = Builders<MgContent>.Filter.Eq ("ID", id);
            return Search (filter);
        }

        /// <summary>
        /// 查看所有用户
        /// </summary>
        /// <returns></returns>
        public List<MgContent> SearchAll () {
            return Search ();
        }
        /// <summary>
        /// 多条件查询
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public List<MgContent> SearchPage (Pager pager) {
            var filter = Builders<MgContent>.Filter.Eq (p => p.IsDelete, false);

            long total = 0;
            var result = SearchByPage (filter, order => order.CreateTime, false, pager.PageIndex, pager.PageSize, out total);
            pager.TotalCount = (int) total;
            return result;
        }

    }
}