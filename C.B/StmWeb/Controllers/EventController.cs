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
    public class EventController : Controller
    {
        private EventInfoRepository _repository;
        private EventTypeRepository _typeRepository;

        public EventController()
        {
            _repository = new EventInfoRepository();
            _typeRepository = new EventTypeRepository();
        }

        public IActionResult Index()
        {
            ViewBag.Id = 1;
            return View();
        }
        public IActionResult Award()
        {
            return View();
        }
        public IActionResult Review()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Active()
        {
            return View();
        }


        public IActionResult GetTypeList(int parentId)
        {
            var list = _typeRepository.Where(m => m.ParentId == parentId && m.IsDeleted == 0);
            var response = list.Select(m => new
            {
                id = m.Id,
                name = m.Name,
            });
            return Json(BaseResponse.SuccessResponse(response));
        }



        public IActionResult GetDetail(int typeId)
        {
            var m = _repository.FirstOrDefault(e => e.EventId == typeId && e.IsDeleted == 0);
            if (m == null)
                return Json(BaseResponse.SuccessResponse());
            var response = new
            {
                id = m.Id,
                title = m.Title,
                simpleContent = m.Content.TakeString(200),
                content = m.Content,
                date = m.CreateTime.ToDate(),
            };
            return Json(BaseResponse.SuccessResponse(response));
        }




    }
}
