using System;
using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data {

    public class UserWork : BaseEntity {

        public long AuthUserId { set; get; }
        public long EventId { set; get; }

        public string Title { set; get; }
        public string Description { set; get; }

        [MaxLength (128)]
        public string Filepath { set; get; }

        [MaxLength (128)]
        public string Url { set; get; }

        [MaxLength (64)]
        public string FileMd5 { set; get; }

        [MaxLength (64)]
        public string FileName { set; get; }

        public UserWorkState State { set; get; }

        public long AuditUserId { set; get; }
        public DateTime AuditTime { set; get; }
        public decimal AuditScore { set; get; }
        public string AuditRemark { set; get; }

        public long Sort { set; get; }
    }

    public enum UserWorkState {
        已删除 = 0,
        待提交 = 1,
        已提交 = 6,
        已审核 = 8,

    }

}