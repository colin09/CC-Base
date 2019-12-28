using C.B.WorkApi.Utinity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace C.B.WorkApi.Controllers
{
    public class OfficeController : ApiController
    {



        // GET api/values/5
        public bool ConvertWord(string filePath)
        {
            try
            {
                var word = new WordConvert();
                word.Convert(filePath);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}
