using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using C.B.Common.helper;
using C.B.Models.Data;
using C.B.Models.Enums;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Area.Sys.Controllers
{
    [Area("Sys")]
    [Authorize(Roles = "develop,admin")]
    public class FileController : Controller
    {

        private readonly UserInfoRepository _userRepository;
        private readonly ResourceInfoRepository _resourceInfoRepository;

        public FileController()
        {
            _userRepository = new UserInfoRepository();
            _resourceInfoRepository = new ResourceInfoRepository();
        }

        public IActionResult Images()
        {
            return View();
        }
        public IActionResult Videos()
        {
            return View();
        }
        public IActionResult Doces()
        {
            return View();
        }




        public IActionResult Upload()
        {
            return View();
        }

        public async Task<IActionResult> FileSave()
        {
            //var date = Request;
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            string webRootPath = @"~/";
            string contentRootPath = Directory.GetCurrentDirectory();

            var fileList = new List<ResourceInfo>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = System.IO.Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位

                    string newFileName = System.Guid.NewGuid().ToString() + "." + fileExt; //随机生成新的文件名
                    var filePath = webRootPath + "/upload/" + newFileName;
                    var fileUrl = webRootPath + "/upload/" + newFileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    fileList.Add(new ResourceInfo
                    {
                        Filepath = fileUrl,
                        FileName = formFile.FileName,
                        FileType = fileExt,
                        FileMd5 = "",
                    });
                }
            }
            Save2DB(fileList);
            return Ok(new { count = files.Count, size });
        }



        private void Save2DB(List<ResourceInfo> list)
        {
            var result = _resourceInfoRepository.InsertBatch(list);


        }
        private void CreateVideoThrum()
        {
            var cmd = "";
            var message = "";
            CmdTool.RunCmd(cmd, out message);
        }

    }

    /*
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

    }*/

}