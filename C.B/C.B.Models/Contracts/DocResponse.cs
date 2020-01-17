namespace C.B.Models.Contracts {

    public class DocRequest {
        public string fileName { get; set; }
        public string filePath { get; set; }
    }

    public class DocResponse {
        public string htmlPath { get; set; }
        public string content { get; set; }

    }
}