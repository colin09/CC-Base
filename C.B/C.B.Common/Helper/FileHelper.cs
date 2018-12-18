using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


namespace C.B.Common.helper
{
    public static class FileHelper
    {

        public async static Task<FileModel[]> SaveFiles(IFormFileCollection files)
        {

            string webRootPath = @"../../Sources/";
            string contentRootPath = $"{Directory.GetCurrentDirectory()}/SourcesFile/";

            var fileList = new List<FileModel>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = System.IO.Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位

                    string newFileName = System.Guid.NewGuid().ToString() + fileExt; //随机生成新的文件名

                    var filePath = $"{contentRootPath}{fileExt.ToFileType()}/{newFileName}";
                    var fileUrl = $"{webRootPath}{fileExt.ToFileType()}/{newFileName}";

                    Utility.DirectoryCheck($"{contentRootPath}{fileExt.ToFileType()}");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    fileList.Add(new FileModel
                    {
                        Filepath = filePath,
                        FileUrl = fileUrl,
                        FileName = formFile.FileName,
                        FileType = fileExt.ToFileType(),
                        FileMd5 = "",
                    });
                }
            }
            return fileList.ToArray();
        }



    }

    public class FileModel
    {
        public string FileName {set;get;}
        public string Filepath {set;get;}
        public string FileUrl {set;get;}
        public string FileType {set;get;}
        public string FileMd5 {set;get;}
    }
}