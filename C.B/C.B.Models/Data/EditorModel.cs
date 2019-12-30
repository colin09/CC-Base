using System;
using System.Collections.Generic;
using C.B.Common.helper;

namespace C.B.Models.Data {
    public class EditorModel {
        public string EditType { set; get; }

        public int Id { set; get; }
        public int TypeId { set; get; }
        public string TypeName { set; get; }

        public int NewsType { set; get; }
        public int ExpertType { set; get; }

        public string Title { set; get; }
        public string SubTitle { set; get; }
        public string Content { set; get; }

        public string PubOrg { set; get; }
        //public string Region { set; get; }

        public string Author { set; get; }
        public bool IsShow { set; get; }
        public bool IsTop { set; get; }
        public bool IsRoll { set; get; }

        public int ImageId { set; get; }
        public string ImageUrl { set; get; }

        public int VideoId { set; get; }
        public string VideoUrl { set; get; }

        public int DocId { set; get; }
        public string DocUrl { set; get; }

        public int SortNo { set; get; }
        public int State { set; get; }

        public DateTime CreateTime { set; get; }

        public string IsShowTxt => IsShow ? "是" : "否";
        public string IsTopTxt => IsTop ? "是" : "否";
        public string IsRollTxt => IsRoll ? "是" : "否";
        public string StateTxt => State == 1 ? "启用" : "取消";

        public string CreateTimeTxt => CreateTime.ToString ("yyyy-MM-dd HH:mm");

        public bool Validate (out string message) {
            var flag = true;
            var error = new List<string> ();

            if (Title.IsEmpty () || Title.Length < 2) {
                flag = false;
                error.Add ("标题字数过少。");
            }

            message = string.Join (",", error);
            return flag;
        }
    }
}