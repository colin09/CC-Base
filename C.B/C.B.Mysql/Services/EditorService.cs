using System;
using AutoMapper;
using C.B.Models.Data;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;

namespace C.B.MySql.Repository.Services
{
    public class EditorService
    {
        private IMapper _mapper;
        private EventTypeRepository _eventTypeRepository;
        private EventInfoRepository _eventInfoRepository;
        private NoticeRepository _noticeRepository;
        private NewsInfoRepository _newsInfoRepository;
        private MessageRepository _messageRepository;
        private ExpertInfoRepository _expertInfoRepository;

        public EditorService(IMapper mapper)
        {
            _eventTypeRepository = new EventTypeRepository();
            _eventInfoRepository = new EventInfoRepository();

            _noticeRepository = new NoticeRepository();
            _newsInfoRepository = new NewsInfoRepository();
            _messageRepository = new MessageRepository();
            _expertInfoRepository = new ExpertInfoRepository();

            _mapper = mapper;
        }


        public EditorModel GetEditorModel(string type, int typeId = 0, int id = 0)
        {
            System.Console.WriteLine($"service GetEditorModel ====>>  type：{type}, id:{id}, typeId:{typeId}");
            EditorModel model = null;
            switch (type)
            {
                case "event":
                    model = GetEventModel(typeId, id);
                    break;
                case "expert":
                    model = GetExpertModel(id);
                    break;
                case "news":
                    model = GetNewsModel(id);
                    break;
                case "notice":
                    model = GetNoticeModel(id);
                    break;
            }
            return model;
        }

        #region -  read  -

        private EditorModel GetEventModel(int typeId = 0, int id = 0)
        {
            var model = new EditorModel();
            if (id != 0)
            {
                var eventInfo = _eventInfoRepository.FirstOrDefault(id);
                if (eventInfo != null)
                {
                    model = _mapper.Map<EditorModel>(eventInfo);
                    typeId = eventInfo.EventId;
                }
            }
            if (typeId != 0)
            {
                var type = _eventTypeRepository.FirstOrDefault(typeId);
                if (type != null)
                {
                    model.TypeId = typeId;
                    model.TypeName = type.Name;
                }
            }
            return model;
        }

        private EditorModel GetExpertModel(int id = 0)
        {
            var model = new EditorModel();

            if (id != 0)
            {
                var expert = _expertInfoRepository.FirstOrDefault(id);
                if (expert != null)
                    model = _mapper.Map<EditorModel>(expert);
                //Mapper.Map<ExpertInfo, EditorModel>(expert);
            }
            return model;
        }

        private EditorModel GetNewsModel(int id = 0)
        {
            var model = new EditorModel();

            if (id != 0)
            {
                var news = _newsInfoRepository.FirstOrDefault(id);
                if (news != null)
                    model = _mapper.Map<EditorModel>(news);
            }
            return model;
        }

        private EditorModel GetNoticeModel(int id = 0)
        {
            var model = new EditorModel();

            if (id != 0)
            {
                var notice = _noticeRepository.FirstOrDefault(id);
                if (notice != null)
                    model = _mapper.Map<EditorModel>(notice);
            }
            return model;
        }

        #endregion



        public bool ModifyEditor(EditorModel model)
        {
            var result = false;
            switch (model.EditType)
            {
                case "event":
                    result = ModifyEventInfo(model);
                    break;
                case "message":
                    result = ModifyMessageInfo(model);
                    break;
                case "notice":
                    result = ModifyNoticeInfo(model);
                    break;
                case "expert":
                    result = ModifyExpertInfo(model);
                    break;
                case "news":
                    result = ModifyNewsInfo(model);
                    break;
            };
            return result;
        }




        #region -  modify   -


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
                NewsType = (NewsType)m.NewsType,
                Title = m.Title,
                Content = m.Content,
                Author = m.Author,

                PubOrg = m.PubOrg,
                ThumbId = m.ThumbId,
                ThumUrl = m.ThumbUrl,
                VideoId = m.FileId,
                VideoUrl = m.FileUrl,

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


        public bool DeleteEditorModel(string type, int id = 0)
        {
            System.Console.WriteLine($"service DeleteEditorModel ====>>  type：{type}, id:{id}");
            var result = 0;
            switch (type)
            {
                case "event":
                    result = _eventInfoRepository.Delete(id);
                    break;
                case "expert":
                    result = _expertInfoRepository.Delete(id);
                    break;
                case "news":
                    result = _newsInfoRepository.Delete(id);
                    break;
                case "notice":
                    result = _noticeRepository.Delete(id);
                    break;
            }
            return result > 0;
        }


    }
}