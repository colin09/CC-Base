using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using C.B.Models.Data;
using C.B.Models.Enums;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Area.Sys.Controllers {
    [Area ("Sys")]
    [Authorize (Roles = "develop,admin")]
    public class FileController : Controller {

        private readonly UserInfoRepository _userRepository;

        public FileController () {
            _userRepository = new UserInfoRepository ();
        }

        public IActionResult Index () {
            return View ();
        }

        public async Task<IActionResult> FileSave ()
        {
            var date = Request;
            var files = Request.Form.Files;
            long size = files.Sum (f => f.Length);
            string webRootPath = "" ; // _hostingEnvironment.WebRootPath;
            string contentRootPath = "" ; // _hostingEnvironment.ContentRootPath;

            foreach (var formFile in files) {
                if (formFile.Length > 0) {
                    string fileExt = GetFileExt (formFile.FileName); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位
                    string newFileName = System.Guid.NewGuid ().ToString () + "." + fileExt; //随机生成新的文件名
                    var filePath = webRootPath + "/upload/" + newFileName;
                    using (var stream = new FileStream (filePath, FileMode.Create)) {
                        await formFile.CopyToAsync (stream);
                    }
                }
            }

            return Ok (new { count = files.Count, size });
        }

        private string GetFileExt(string fileName){

            return "";
        }



    }

    public interface IFormFile {

        string ContentType { get; }
        string ContentDisposition { get; }
        IHeaderDictionary Headers { get; }
        long Length { get; }
        string Name { get; }
        string FileName { get; }
        Stream OpenReadStream ();
        void CopyTo (Stream target);
        Task CopyToAsync (Stream target, CancellationToken cancellationToken);

    }
}