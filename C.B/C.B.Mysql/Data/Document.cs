using System;

namespace C.B.MySql.Data {
    public class Document : BaseEntity {

        ///<summary>
        /// 文档类别， 1-富文本内容，2-html地址，
        ///</summary>
        public int DocType { get; set; }

        public string Title { get; set; }
        public string SubTitle { get; set; }

        public string Content { get; set; }
        public string SimpleContent { get; set; }

        public string Url { get; set; }

        public string Author { get; set; }
        public DateTime PublishTime { get; set; }

    }
}