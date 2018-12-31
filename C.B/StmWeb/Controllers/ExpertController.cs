using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using C.B.Common.helper;
using C.B.Models.Data;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Controllers
{
    public class ExpertController : Controller
    {

        private ExpertInfoRepository _repository;
        public ExpertController()
        {
            _repository = new ExpertInfoRepository();
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail(int id)
        {
            ViewBag.Id = id;
            return View();
        }


        public IActionResult GetList(int type)
        {
            var list = _repository.Where(m => m.Type == type && m.IsDeleted == 0);
            var response = list.Select(m => new
            {
                id = m.Id,
                name = m.Title,
                url = m.PicUrl,
                date = m.CreateTime.ToDate(),
            });
            return Json(BaseResponse.SuccessResponse(response));
        }

        public IActionResult GetDetail(int id)
        {
            var m = _repository.FirstOrDefault(id);
            var response = new
            {
                id = m.Id,
                name = m.Title,
                content = m.Content,
                type = m.Type,
                url = m.PicUrl,
                date = m.CreateTime.ToDate(),
            };
            return Json(BaseResponse.SuccessResponse(response));
        }

    }
}
