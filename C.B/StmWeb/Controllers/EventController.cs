using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using C.B.Common.helper;
using C.B.Common.Mvc;
using C.B.Models.Data;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;

namespace StmWeb.Controllers {
    public class EventController : BaseController // Controller
    {
        private EventInfoRepository _repository;
        private EventTypeRepository _typeRepository;
        private MessageRepository _messageRepository;
        private AreaInfoRepository _areaRepository;
        private DocumentRepository _documentRepository;

        public EventController () {
            _repository = new EventInfoRepository ();
            _typeRepository = new EventTypeRepository ();
            _messageRepository = new MessageRepository ();
            _areaRepository = new AreaInfoRepository ();
            _documentRepository = new DocumentRepository ();
        }

        public IActionResult Index () {
            ViewBag.Id = 1;
            return View ();
        }
        public IActionResult Award () {
            return View ();
        }
        public IActionResult Review () {
            return View ();
        }
        public IActionResult Contact () {
            return View ();
        }
        public IActionResult Active () {
            return View ();
        }

        public IActionResult GetTypeList (int parentId, int level = 1) {
            var list = _typeRepository.Where (m => m.ParentId == parentId && m.IsDeleted == 0);
            if (parentId == 0)
                list = _typeRepository.Where (m => m.Level == level && m.IsDeleted == 0 && m.IsShow == 1);
            var response = list.Select (m => new {
                id = m.Id,
                    name = m.Name,
            }).ToList ();
            return Json (BaseResponse.SuccessResponse (response));
        }

        public IActionResult GetDetail (int typeId) {
            var m = _repository.FirstOrDefault (e => e.EventId == typeId && e.IsDeleted == 0);
            if (m == null)
                return Json (BaseResponse.SuccessResponse ());
            var doc = _documentRepository.FirstOrDefault (m.DocumentId);
            var response = new {
                id = m.Id,
                title = m.Title,
                simpleContent = m.Content.TakeString (200),
                content = m.Content,
                date = m.CreateTime.ToDate (),
                document = doc,
            };
            return Json (BaseResponse.SuccessResponse (response));
        }

        public IActionResult GetAreaList () {
            var list = _areaRepository.Where (m => m.IsDeleted == 0);
            var response = list.Select (m => m.Name);
            return Json (BaseResponse.SuccessResponse (response));
        }

        [HttpPost]
        //public IActionResult SubmitAsk(string name, string area, string content, string code)
        public IActionResult SubmitAsk ([FromBody] BaseRequest request) {
            var code = request.Key4;
            if (code.IsEmpty ())
                return Json (BaseResponse.ErrorResponse ("请填写验证码。"));
            var vCode = HttpContext.Session.GetString ("Session.VerifyCode");
            if (vCode.ToLower () != code.ToLower ())
                return Json (BaseResponse.ErrorResponse ("验证码错误。"));

            HttpContext.Session.SetString ("Session.VerifyCode", "empty-empty");

            var message = new Message {
                Title = "",
                Content = request.Key3,
                Region = request.Key2,
                Name = request.Key1,
                IsShow = 0,
                IsTop = 0,
                SortNo = DateTime.Now.ToOADate (),
            };
            _messageRepository.Insert (message);
            return Json (BaseResponse.SuccessResponse ());
        }
        public IActionResult GetAskList (Pager pager) {
            var list = _messageRepository.Where (pager, m => m.IsDeleted == 0 && m.IsShow == 1, m => m.SortNo);
            var response = list.Select (m => new {
                id = m.Id,
                    name = m.Name,
                    content = m.Content,
                    area = m.Region,
                    date = m.CreateTime.ToDate (),
                    replyName = m.ReplyName,
                    replyContent = m.ReplyContent,
                    replyDate = m.ReplyTime.ToDate (),
                    isReply = m.ReplyTime.HasValue ? 1 : 0,
            }).ToList ();
            return Json (BaseResponse.SuccessResponse (response));
        }

    }
}