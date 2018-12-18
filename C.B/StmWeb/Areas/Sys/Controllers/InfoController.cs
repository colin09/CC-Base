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
using C.B.Common.helper;

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

        #region  -  EventInfo、Expert、News、Notice  添加 编辑  -

        public IActionResult Editor(int type = 1, int id = 0, int typeId = 0)
        {
            ViewBag.Id = id;
            ViewBag.Type = type == 1 ? "event" : type == 2 ? "expert" : type == 3 ? "news" : type == 4 ? "notice" : "event";
            ViewBag.TypeId = typeId;
            return View();
        }

        [HttpPost]
        public IActionResult SaveEditor([FromForm]EditorModel model)
        {
            var files = Request.Form.Files;
            long size = files.Sum(f => f.Length);
            if (size > 0)
            {
                var fileList = Task.Run(() => FileHelper.SaveFiles(files)).Result;
                fileList.ToList().ForEach(item =>
                {
                    if (item.FileType == "image")
                        model.ThumbUrl = item.FileUrl;
                    if (item.FileType == "video")
                        model.FileUrl = item.FileUrl;
                });
            }
            System.Console.WriteLine($"==> EditorModel: {model.ToJson()}");
            var result = false;
            var action = "Editor";
            switch (model.EditType)
            {
                case "event":
                    result = ModifyEventInfo(model);
                    action = "EventIndex";
                    break;
                case "message":
                    result = ModifyMessageInfo(model);
                    action = "MessageIndex";
                    break;
                case "notice":
                    result = ModifyNoticeInfo(model);
                    action = "NoticeIndex";
                    break;
                case "expert":
                    result = ModifyExpertInfo(model);
                    action = "ExpertIndex";
                    break;
                case "news":
                    result = ModifyNewsInfo(model);
                    action = "NewsIndex";
                    break;
                default:
                    break;
            };
            if (!result)
                return Json(BaseResponse.ErrorResponse("数据错误。"));
            return Json(BaseResponse.SuccessResponse($"/Sys/Info/{action}"));

            return RedirectToAction(action, "Info", new { area = "Sys" }); ;
        }

        public IActionResult GetEditorInfo(string type, int id)
        {
            switch (type)
            {
                case "event": var eventM = _eventInfoRepository.FirstOrDefault(id); return Json(BaseResponse.SuccessResponse(eventM));
                case "expert": var expert = _expertInfoRepository.FirstOrDefault(id); return Json(BaseResponse.SuccessResponse(expert));
                case "news": var news = _newsInfoRepository.FirstOrDefault(id); return Json(BaseResponse.SuccessResponse(news));
                case "notice": var notice = _noticeRepository.FirstOrDefault(id); return Json(BaseResponse.SuccessResponse(notice));
            }
            return Json(BaseResponse.ErrorResponse("id 不存在。"));
        }

        [HttpPost]
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
                NewsType = (NewsType)m.TypeId,
                Title = m.Title,
                Content = m.Content,
                Author = m.Author,

                PubOrg = m.PubOrg,
                ThumbId = m.ThumbId,
                ThumUrl = m.ThumbUrl,
                VideoId = 0,
                VideoUrl = "",

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


        #endregion

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

        public IActionResult GetEventInfoList(int typeId)
        {
            var list = _eventInfoRepository.Where(m => m.IsDeleted == 0 && m.EventId == typeId);
            return Json(BaseResponse.SuccessResponse(list));
        }


        #endregion

        public IActionResult ExpertIndex()
        {
            return View();
        }
        public IActionResult GetExpertInfoList()
        {
            var list = _expertInfoRepository.Where(m => m.IsDeleted == 0);
            return Json(BaseResponse.SuccessResponse(list));
        }

        public IActionResult NewsIndex()
        {
            return View();
        }
        public IActionResult GetNewsInfoList(NewsType type)
        {
            var list = _newsInfoRepository.Where(m => m.IsDeleted == 0 && m.NewsType == type);
            return Json(BaseResponse.SuccessResponse(list));
        }

        public IActionResult NoticeIndex()
        {
            return View();
        }
        public IActionResult GetNoticeList(NewsType type)
        {
            var list = _noticeRepository.Where(m => m.IsDeleted == 0);
            return Json(BaseResponse.SuccessResponse(list));
        }

        public IActionResult MessageIndex()
        {
            return View();
        }
        public IActionResult GetMessageList(NewsType type)
        {
            var list = _messageRepository.Where(m => m.IsDeleted == 0);
            return Json(BaseResponse.SuccessResponse(list));
        }

    }
}