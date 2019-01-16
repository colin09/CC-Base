


using System;
using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data
{

    public class BaseEntity000000000000
    {

        public BaseEntity000000000000()
        {
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            IsDeleted = 0;
        }

        public int Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }

        public int IsDeleted { set; get; }





        // public string CreateObjectId()
        // {
        //     return "";   //return new Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32).ToLower();
        // }
    }

}


