using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StmWeb.Models;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;
using C.B.Models.Data;

namespace StmWeb.Area.Sys.Controllers
{
    [Area("Sys")]
    [Authorize(Roles = "develop,admin")]
    public class InfoController : Controller
    {
        private EventTypeRepository _eventTypeRepository;
        private EventInfoRepository _eventInfoRepository;
        private NoticeRepository _noticeRepository;
        private NewsInfoRepository _newsInfoRepository;
        private MessageRepository _messageRepository;
        private ExpertInfoRepository _expertInfoRepository;

        public InfoController()
        {
            _eventTypeRepository = new EventTypeRepository();
            _eventInfoRepository = new EventInfoRepository();
            
            _noticeRepository = new NoticeRepository();
            _newsInfoRepository = new NewsInfoRepository();
            _messageRepository = new MessageRepository();
            _expertInfoRepository = new ExpertInfoRepository();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }

        public IActionResult SaveEditor([FromBody]EditorModel model)
        {
            switch (model.EditType)
            {
                case "event":
                    ModifyEventInfo(model);
                    break;
                case "message":
                    ModifyMessageInfo(model);
                    break;
                case "notice":
                    ModifyNoticeInfo(model);
                    break;
                case "expert":
                    ModifyExpertInfo(model);
                    break;
                case "news":
                    ModifyNewsInfo(model);
                    break;
            };
            return View();
        }


        public IActionResult ModifyEditor([FromBody]EditorModel model)
        {
            switch (model.EditType)
            {
                case "event":
                    ModifyEventInfo(model);
                    break;
                case "message":
                    ModifyMessageInfo(model);
                    break;
                case "notice":
                    ModifyNoticeInfo(model);
                    break;
                case "expert":
                    ModifyExpertInfo(model);
                    break;
                case "news":
                    ModifyNewsInfo(model);
                    break;
            };
            return View();
        }

        private bool ModifyEventInfo(EditorModel m)
        {
            var model = new EventInfo
            {
                Id = m.Id,
                EventId = m.TypeId,
                Title = m.Title,
                Content = m.Content,
                Author = m.Author,

                ThumbId = m.ThumbId,
                ThumbUrl = m.ThumbUrl,

                IsShow = m.IsShow ? 1 : 0,
                IsTop = m.IsTop ? 1 : 0,
            };
            var result = 0;
            if (model.Id > 0)
                result = _eventInfoRepository.Update(model);
            else
                result = _eventInfoRepository.Insert(model);
            return result > 0;
        }
        private bool ModifyNewsInfo(EditorModel m)
        {
            var model = new NewsInfo
            {
                Id = m.Id,
                NewsType = m.TypeId,
                Title = m.Title,
                Content = m.Content,
                Author = m.Author,

                PubOrg = m.PubOrg,
                ThumbId = m.ThumbId,
                ThumUrl = m.ThumbUrl,
                FileId = 0,
                FileUrl = "",

                IsShow = m.IsShow ? 1 : 0,
                IsTop = m.IsTop ? 1 : 0,
                IsRoll = m.IsRoll ? 1 : 0,
                SortNo = DateTime.Now.ToOADate(),
            };
            var result = 0;
            if (model.Id > 0)
                result = _newsInfoRepository.Update(model);
            else
                result = _newsInfoRepository.Insert(model);
            return result > 0;
        }
        private bool ModifyMessageInfo(EditorModel m)
        {
            var model = new Message
            {
                Id = m.Id,
                Title = m.Title,
                Content = m.Content,

                Region = "",
                Name = "",
                ReplyContent = "",
                ReplyName = "",
                ReplyTime = null,

                IsShow = m.IsShow ? 1 : 0,
                IsTop = m.IsTop ? 1 : 0,
                SortNo = DateTime.Now.ToOADate(),
            };
            var result = 0;
            if (model.Id > 0)
                result = _messageRepository.Update(model);
            else
                result = _messageRepository.Insert(model);
            return result > 0;
        }
        private bool ModifyNoticeInfo(EditorModel m)
        {
            var model = new Notice
            {
                Id = m.Id,
                Title = m.Title,
                Content = m.Content,
                Author = m.Author,
                IsShow = m.IsShow ? 1 : 0,
                IsTop = m.IsTop ? 1 : 0,
                IsRoll = m.IsTop ? 1 : 0, //?????????????????????????????????????
                PubTime = DateTime.Now,
                PubOrg = "PubOrg",
                SortNo = DateTime.Now.ToOADate(),
            };
            var result = 0;
            if (model.Id > 0)
                result = _noticeRepository.Update(model);
            else
                result = _noticeRepository.Insert(model);
            return result > 0;
        }
        private bool ModifyExpertInfo(EditorModel m)
        {
            var model = new ExpertInfo
            {
                Id = m.Id,
                Title = m.Title,
                Content = m.Content,
                Author = m.Author,
                IsShow = m.IsShow ? 1 : 0,
                SortNo = DateTime.Now.ToOADate(),
                PicFileId = 0,
                PicUrl = "",
            };
            var result = 0;
            if (model.Id > 0)
                result = _expertInfoRepository.Update(model);
            else
                result = _expertInfoRepository.Insert(model);
            return result > 0;
        }


        #region -  Event  -
        public IActionResult EventIndex()
        {
            return View();
        }

        public IActionResult GetEventTypes()
        {
            var list = _eventTypeRepository.Where(m => m.IsDeleted == 0);
            return Json(BaseResponse.SuccessResponse(list));
        }

        [HttpPost]
        public IActionResult SaveEventType([FromBody]EventType model)
        {
            if (model.ParentId == 0)
            {
                return Json(BaseResponse.ErrorResponse("请选择一个类别作为父节点类别。"));
            }
            _eventTypeRepository.Insert(model);
            return Json(BaseResponse.SuccessResponse());
        }
        [HttpPost]
        public IActionResult EditEventType([FromBody]EventType model)
        {
            var type = _eventTypeRepository.FirstOrDefault(model.Id);
            if (type == null)
                return Json(BaseResponse.ErrorResponse("请选择一个类别进行删除操作。"));
            _eventTypeRepository.Update(model);
            return Json(BaseResponse.SuccessResponse());
        }
        [HttpPost]
        public IActionResult DeleteEventType(int id)
        {
            _eventTypeRepository.Delete(id);
            return Json(BaseResponse.SuccessResponse());
        }



        #endregion




        public IActionResult Expert()
        {
            return View();
        }
    }
}