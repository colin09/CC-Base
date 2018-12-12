using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using C.B.Models.Data;
using C.B.MySql.Repository.EntityRepositories;
using C.B.MySql.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;
using C.B.Models.Enums;
using C.B.Common.helper;

namespace StmWeb.Area.Sys.Controllers
{
    [Area("Sys")]
    [Authorize(Roles = "develop")]
    public class FileController : Controller
    {
        private readonly FileInfoRepository _fileRepository;
        public FileController()
        {
            _fileRepository = new FileInfoRepository();
        }

        public IActionResult Index()
        {
            return View();
        }



    }
}