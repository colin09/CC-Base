using System;
using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data {
    public class Document : BaseEntity {

        ///<summary>
        /// 文档类别， 1-富文本内容，2-html地址，
        ///</summary>
        public int DocType { get; set; }

        [MaxLength (128)]
        public string Title { get; set; }

        [MaxLength (256)]
        public string SubTitle { get; set; }

        ///<summary>
        /// 文档内容，超长文本
        ///</summary>
        public string Content { get; set; }

        ///<summary>
        /// 文档内容，去除html标签，取500长度
        ///</summary>
        [MaxLength (512)]
        public string SimpleContent { get; set; }

        public string Url { get; set; }

        public string Author { get; set; }
        public DateTime PublishTime { get; set; }

    }
}