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
            string webRootPath = @"../../";
            string contentRootPath = Directory.GetCurrentDirectory();

            var fileList = new List<ResourceInfo>();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    string fileExt = System.IO.Path.GetExtension(formFile.FileName); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位

                    string newFileName = System.Guid.NewGuid().ToString() + fileExt; //随机生成新的文件名
                    var filePath = contentRootPath + "/upload/" + newFileName;
                    var fileUrl = webRootPath + "upload/" + newFileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    fileList.Add(new ResourceInfo
                    {
                        Filepath = filePath,
                        Url = fileUrl,
                        FileName = formFile.FileName,
                        FileType = fileExt.ToFileType(),
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




        public IActionResult Checker()
        {
            //request.args.get("CKEditorFuncNum")
           var callback = Request.Query["CKEditorFuncNum"];
            System.Console.WriteLine($" ==> callback: {callback}");

            ViewBag.callback = callback;
            return View();
        }

        [HttpGet]
        public IActionResult Browse(string type = "image")
        {
            var list = _resourceInfoRepository.Where(m => m.FileType == type);

            return Json(BaseResponse.SuccessResponse(list));
        }

        public IActionResult Select (string urls,string callback){
            
            string tpl = "<script type=\"text/javascript\">window.opener.CKEDITOR.tools.callFunction(\"{1}\", \"{0}\", \"{2}\");window.close();</script>";
            return Content(string.Format(tpl, urls, callback, ""), "text/html");
        }




    }
}