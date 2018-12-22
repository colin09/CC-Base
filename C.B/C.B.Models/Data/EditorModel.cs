namespace C.B.Models.Data
{
    public class EditorModel
    {
        public string EditType{set;get;}


        public int Id{set;get;}
        public int TypeId{set;get;}
        public string TypeName{set;get;}

        public int NewsType{set;get;}

        public string Title{set;get;}
        public string Content{set;get;}
        public string PubOrg { set; get; }
        //public string Region { set; get; }


        public string Author{set;get;}
        public bool IsShow{set;get;}
        public bool IsTop{set;get;}
        public bool IsRoll{set;get;}

        public int ThumbId { set; get; }
        public string ThumbUrl { set; get; }

        public int FileId{set;get;}
        public string FileUrl{set;get;}
        
        public int SortNo { set; get; }
    }
}