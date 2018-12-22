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
using AutoMapper;
using C.B.MySql.Repository.Services;

namespace StmWeb.Area.Sys.Controllers
{
    [Area("Sys")]
    [Authorize(Roles = "develop,admin")]
    public class InfoController : Controller
    {
        private IMapper _mapper;
        private EditorService _editorService;
        private EventTypeRepository _eventTypeRepository;
        private EventInfoRepository _eventInfoRepository;
        private NoticeRepository _noticeRepository;
        private NewsInfoRepository _newsInfoRepository;
        private MessageRepository _messageRepository;
        private ExpertInfoRepository _expertInfoRepository;

        public InfoController(IMapper mapper)
        {
            _mapper = mapper;
            _editorService = new EditorService(mapper);
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
            var result = _editorService.ModifyEditor(model);
            if (!result)
                return Json(BaseResponse.ErrorResponse("数据错误。"));
            return Json(BaseResponse.SuccessResponse($"/Sys/Info/{model.EditType}Index"));
        }

        public IActionResult GetEditorInfo(string type, int typeId = 0, int id = 0)
        {
            var result = _editorService.GetEditorModel(type, typeId, id);
            return Json(BaseResponse.SuccessResponse(result));
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