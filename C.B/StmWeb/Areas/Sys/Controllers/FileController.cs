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
            string webRootPath = @"../../Sources/";
            string contentRootPath = $"{Directory.GetCurrentDirectory()}/SourcesFile/";

            var fileList = new List<ResourceInfo>();

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
            list.ForEach(item =>
            {
                if (item.FileType == "videl")
                    CreateVideoThrum(item);
            });

        }
        private void CreateVideoThrum(ResourceInfo model)
        {
            /**
            # 50分钟处截屏 
            ffmpeg -ss 00:50:00  -i RevolutionOS.rmvb sample.jpg  -r 1 -vframes 1 -an -vcodec mjpeg  
            # 或者使用 -f 参数指定输出的格式为 mjpeg ，效果一样 
            ffmpeg -ss 00:50:00  -i RevolutionOS.rmvb sample.jpg  -r 1 -vframes 1 -an -f mjpeg
            */

            var time = "00:01:00";
            var fileImagePath = model.Filepath.Substring(0, model.Filepath.LastIndexOf(".")) + ".jpg";

            var cmd = "ffmpeg -ss {0}  -i {1} {2}  -r 1 -vframes 1 -an -f mjpeg";
            cmd = cmd.Frmt(time, model.Filepath, fileImagePath);

            var message = "";
            CmdTool.RunCmd(cmd, out message);
            System.Console.WriteLine(message);
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

        public IActionResult Select(string urls, string callback)
        {

            string tpl = "<script type=\"text/javascript\">window.opener.CKEDITOR.tools.callFunction(\"{1}\", \"{0}\", \"{2}\");window.close();</script>";
            return Content(string.Format(tpl, urls, callback, ""), "text/html");
        }

    }
}