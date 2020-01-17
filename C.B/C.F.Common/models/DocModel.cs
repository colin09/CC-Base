using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C.F.Common.models
{
    class DocModel
    {
    }


    public class DocRequest
    {
        public string fileName { get; set; }
        public string filePath { get; set; }
    }

    public class DocResponse
    {
        public string htmlPath { get; set; }
        public string content { get; set; }

    }
}
