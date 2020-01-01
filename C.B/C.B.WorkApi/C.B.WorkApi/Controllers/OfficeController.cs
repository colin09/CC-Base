using C.B.WorkApi.Utinity;
using System;
using System.Web.Http;
using C.B.WorkApi.Models;

namespace C.B.WorkApi.Controllers
{
    public class OfficeController : ApiController
    {



        [HttpPost]
        public BaseResponse ConvertWord(string filePath)
        {
            try
            {
                var word = new WordConvert();
                var result = word.Convert(filePath);

                return new BaseResponse<DocResponse>(result);
            }
            catch (Exception ex)
            {
                return BaseResponse.Error(ex.Message);
            }

        }

    }
}
