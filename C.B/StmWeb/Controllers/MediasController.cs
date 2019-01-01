using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using C.B.Models.Data;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;
using C.B.Common.helper;

namespace StmWeb.Controllers
{
    public class MediasController : Controller
    {
        private NewsInfoRepository _repository;

        public MediasController()
        {
            _repository = new NewsInfoRepository();
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

        public IActionResult GetList([FromBody]BasePageRequest request)
        {
            //System.Console.WriteLine($" ===>  pager:{request.Pager.ToJson()}, type:{request.Num1}");
            var newType = (NewsType)request.Num1;
            var result = _repository.Where(request.Pager, m => m.NewsType == newType && m.IsDeleted == 0, m => m.CreateTime);
            var response = result.Select(m => new
            {
                id = m.Id,
                title = m.Title,
                content = m.Content,
                pubOrg = m.PubOrg,
                author = m.Author,
                url = m.ThumUrl,
                video = m.VideoId,
                date = m.CreateTime.ToString("yyyy-MM-dd"),
            });
            return Json(BaseResponse.SuccessResponse(response));
        }
        public IActionResult GetDetail(int id)
        {
            var m = _repository.FirstOrDefault(id);
             var response = new
            {
                id = m.Id,
                type = m.NewsType,
                title = m.Title,
                content = m.Content,
                pubOrg = m.PubOrg,
                author = m.Author,
                url = m.ThumUrl,
                video = m.VideoUrl,
                date = m.CreateTime.ToString("yyyy-MM-dd"),
            };
            return Json(BaseResponse.SuccessResponse(response));
        }

    }
}
