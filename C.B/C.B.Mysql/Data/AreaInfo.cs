using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace C.B.MySql.Data {

    /// <summary>
    /// 赛事分区
    /// </summary>
    public class AreaInfo: BaseEntity {
        public int ParentId { set; get; }

        [MaxLength (64)]
        public string Name { set; get; }

        public int SortNo { set; get; }
    }
}
