using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using C.B.Common.helper;
using C.B.Common.Mvc;
using C.B.Models.Data;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StmWeb.Area.Sys.Controllers {
    [Area ("Sys")]
    [Authorize (Roles = "system,admin,registered")]
    public class HomeController : MgrBaseController {

        private readonly UserInfoRepository _userRepository;
        private readonly UserWorkRepository _userWorkRepository;

        public HomeController () {
            _userRepository = new UserInfoRepository ();
            _userWorkRepository = new UserWorkRepository ();
        }

        public IActionResult Index () {
            return View ();
        }
        public IActionResult BaseInfo () {
            return View ();
        }
        public IActionResult Works () {
            return View ();
        }

        public IActionResult Audits () {
            return View ();
        }

        public async Task<IActionResult> SubmitWorks () {
            var files = Request.Form.Files;
            long size = files.Sum (f => f.Length);
            string webRootPath = @"../../Sources/";
            string contentRootPath = $"{Directory.GetCurrentDirectory()}/SourcesFile/";
            var curUserInfo = GetCurrentUserInfo (HttpContext.User);

            var fileList = new List<ResourceInfo> ();

            foreach (var formFile in files) {
                if (formFile.Length > 0) {
                    string fileExt = System.IO.Path.GetExtension (formFile.FileName); //文件扩展名，不含“.”
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位

                    string newFileName = System.Guid.NewGuid ().ToString () + fileExt; //随机生成新的文件名

                    var filePath = $"{contentRootPath}UW_{curUserInfo.Id}/{newFileName}";
                    var fileUrl = $"{webRootPath}UW_{curUserInfo.Id}/{newFileName}";

                    Utility.DirectoryCheck ($"{contentRootPath}UW_{curUserInfo.Id}");

                    using (var stream = new FileStream (filePath, FileMode.Create)) {
                        await formFile.CopyToAsync (stream);
                    }
                    fileList.Add (new ResourceInfo {
                        Filepath = filePath,
                            Url = fileUrl,
                            FileName = formFile.FileName,
                            FileType = fileExt.ToFileType (),
                            FileMd5 = "",
                    });

                    _userWorkRepository.Insert (new UserWork {
                        AuthUserId = curUserInfo.Id,
                            Filepath = filePath,
                            Url = fileUrl,
                            FileMd5 = "",
                            FileName = formFile.FileName,
                            State = UserWorkState.待提交,
                    });
                }
            }
            return Ok (new { count = files.Count, size });
        }

        public IActionResult GetWorkList () {
            var curUserInfo = GetCurrentUserInfo (HttpContext.User);

            var list = _userWorkRepository.Where (m => m.IsDeleted == 0 && m.AuthUserId == curUserInfo.Id).OrderBy (m => m.CreateTime);

            return Json (BaseResponse.SuccessResponse (list));
        }

    }
}