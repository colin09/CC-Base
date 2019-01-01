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
        private AreaInfoRepository _areaRepository;

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
            _areaRepository = new AreaInfoRepository();
        }


        public IActionResult Index()
        {
            return View();
        }

        #region  -  EventInfo、Expert、News、Notice  添加 编辑  -

        public IActionResult Editor(int type = 1, int id = 0, int typeId = 0)
        {
            System.Console.WriteLine($" =====》 action Editor, type:{type},id:{id},typeId:{typeId}");
            ViewBag.Id = id;
            ViewBag.Type = type == 1 ? "event" : type == 2 ? "expert" : type == 3 ? "news" : type == 4 ? "notice" : "event";

            return View();
        }

        [HttpPost]
        public IActionResult SaveEditor([FromForm]EditorModel model)
        {
            //System.Console.WriteLine($"==> EditorModel - 01 : {model.ToJson()}");
            var msg = "";
            if (!model.Validate(out msg))
                return Json(BaseResponse.ErrorResponse(msg));

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
                    {
                        model.ThumbUrl = item.ThumbUrl;
                        model.FileUrl = item.FileUrl;
                    }
                });
            }
            //System.Console.WriteLine($"==> EditorModel - 02 : {model.ToJson()}");
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

        public IActionResult DeleteEditorInfo(string type, int id = 0)
        {
            var result = _editorService.DeleteEditorModel(type, id);
            return Json(new BaseResponse(result));
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
            var parent = _eventTypeRepository.FirstOrDefault(model.ParentId);
            model.Level = parent.Level + 1;
            model.SortNo = DateTime.Now.ToOADate();

            _eventTypeRepository.Insert(model);
            return Json(BaseResponse.SuccessResponse());
        }
        [HttpPost]
        public IActionResult EditEventType([FromBody]EventType model)
        {
            var type = _eventTypeRepository.FirstOrDefault(model.Id);
            if (type == null)
                return Json(BaseResponse.ErrorResponse("请选择一个类别进行删除操作。"));
            type.Name = model.Name;
            // type.Level = model.Level;
            type.IsShow = model.IsShow;
            // type.Icon = model.Icon;
            type.SortNo = model.SortNo;
            _eventTypeRepository.Update(type);
            return Json(BaseResponse.SuccessResponse());
        }

        public IActionResult DeleteEventType(int id)
        {
            System.Console.WriteLine($"---> DeleteEventType : {id}");
            _eventTypeRepository.Delete(id);
            return Json(BaseResponse.SuccessResponse());
        }

        public IActionResult GetEventInfoList(int type, Pager pager)
        {
            var list = _eventInfoRepository.Where(pager, m => m.IsDeleted == 0 && m.EventId == type, s => s.CreateTime);
            return Json(BaseResponse.SuccessResponse(_mapper.Map<EditorModel[]>(list)));
        }

        public IActionResult GetEventArea()
        {
            var list = _areaRepository.Where(m => m.IsDeleted == 0);
            var areas = list.Select(m => m.Name).ToList();
            var response = string.Join(",", areas);
            return Json(BaseResponse.SuccessResponse(response));
        }
        public void DeleteAllEventArea()
        {
            var list = _areaRepository.Where(m => m.IsDeleted == 0);
            if (list.Count() > 0)
            {
                list.ToList().ForEach(item =>
                {
                    _areaRepository.Delete(item.Id);
                });
            }
        }
        public IActionResult SaveEventArea([FromBody]BaseRequest request)
        {
            request.Key2 = request.Key2.Replace("，", ",");
            if (request.Key2.IsEmpty())
                return Json(BaseResponse.ErrorResponse("请填写赛区信息。"));
            var areas = request.Key2.Split(",", StringSplitOptions.RemoveEmptyEntries);
            var list = areas.Select(m => new AreaInfo
            {
                Name = m,
                ParentId = 1,
            }).ToList();
            DeleteAllEventArea();
            _areaRepository.InsertBatch(list);
            return Json(BaseResponse.SuccessResponse());
        }




        #endregion

        public IActionResult ExpertIndex()
        {
            return View();
        }
        public IActionResult GetExpertInfoList(Pager pager)
        {
            var list = _expertInfoRepository.Where(pager, m => m.IsDeleted == 0, s => s.CreateTime);
            return Json(BaseResponse.SuccessResponse(_mapper.Map<EditorModel[]>(list)));
        }

        public IActionResult NewsIndex()
        {
            return View();
        }
        public IActionResult GetNewsInfoList(NewsType type, Pager pager)
        {
            var list = _newsInfoRepository.Where(pager, m => m.IsDeleted == 0 && m.NewsType == type, s => s.CreateTime);
            return Json(BaseResponse.SuccessResponse(_mapper.Map<EditorModel[]>(list)));
        }

        public IActionResult NoticeIndex()
        {
            return View();
        }
        public IActionResult GetNoticeList(Pager pager)
        {
            var list = _noticeRepository.Where(pager, m => m.IsDeleted == 0, s => s.CreateTime);
            return Json(BaseResponse.SuccessResponse(_mapper.Map<EditorModel[]>(list)));
        }









        public IActionResult MessageIndex()
        {
            return View();
        }
        public IActionResult GetMessageList(NewsType type, Pager pager)
        {
            var list = _messageRepository.Where(pager, m => m.IsDeleted == 0, s => s.CreateTime, false);
            return Json(BaseResponse.SuccessResponse(list));
        }

    }
}