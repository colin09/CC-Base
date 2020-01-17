using C.F.Common.models;
using C.F.Common.utinity;
using System;
using System.Web.Http;

namespace C.B.WorkApi.Controllers
{
    public class OfficeController : ApiController
    {



        [HttpPost]
        public BaseResponse ConvertWord([FromBody]DocRequest request)
        {
            try
            {
                var word = new WordConvert();
                var result = word.Convert(request.filePath);

                return new BaseResponse<DocResponse>(result);
            }
            catch (Exception ex)
            {
                return BaseResponse.Error(ex.ToString());
            }

        }

    }
}
