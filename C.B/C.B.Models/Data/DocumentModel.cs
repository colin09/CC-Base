using System;

namespace C.B.Models.Data {
    public class DocumentModel {

        public int Id { set; get; }
        public int DocType { set; get; }

        public string Title { set; get; }
        public string SubTitle { set; get; }
        public string Content { set; get; }

        public string PubOrg { set; get; }
        public string Author { set; get; }
        public string ImageUrl { set; get; }
        public string VideoUrl { set; get; }
        public string DocUrl { set; get; }

        public DateTime CreateTime { set; get; }
        public string CreateTimeTxt => CreateTime.ToString ("yyyy-MM-dd HH:mm");

    }
}