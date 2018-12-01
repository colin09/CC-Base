


using System;
using System.ComponentModel.DataAnnotations;

namespace C.B.MySql.Data
{

    public class BaseEntity
    {

        public BaseEntity()
        {

            Id = "";
            CreateTime = DateTime.Now;
            UpdateTime = DateTime.Now;
            IsDeleted = false;
        }

        [MaxLength(32)]
        public string Id { set; get; }

        public DateTime CreateTime { set; get; }

        public DateTime UpdateTime { set; get; }

        public bool IsDeleted { set; get; }





        public string CreateObjectId()
        {
            return "";   //return new Guid.NewGuid().ToString().Replace("-", "").Substring(0, 32).ToLower();
        }
    }

}


