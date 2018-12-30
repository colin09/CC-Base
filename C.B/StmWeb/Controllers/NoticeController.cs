﻿using System;
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
    public class NoticeController : Controller
    {
        private NoticeRepository _repository;
        public NoticeController()
        {
            _repository = new NoticeRepository();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }



        public IActionResult GetList([FromBody]Pager pager)
        {
            var result = _repository.Where(pager, m => m.IsDeleted == 0, m => m.CreateTime);
            var response = result.Select(m => new
            {
                id = m.Id,
                title = m.Title,
                content = m.Content,
                simpleContent = m.Content.TakeString(200),
                year = m.CreateTime.Year,
                date = m.CreateTime.ToString("MM-dd"),
                pubOrg = m.PubOrg,
            });
            return Json(BaseResponse.SuccessResponse(response));
        }





    }
}
