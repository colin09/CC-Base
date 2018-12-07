


using System;
using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data
{

    public class BaseEntity
    {

        public BaseEntity()
        {
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            IsDeleted = false;
        }

        public int Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }

        public bool IsDeleted { set; get; }





        // public string CreateObjectId()
        // {
        //     return "";   //return new Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32).ToLower();
        // }
    }

}


