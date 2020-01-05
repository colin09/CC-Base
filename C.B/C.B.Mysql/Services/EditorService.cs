using System;
using AutoMapper;
using C.B.Common.helper;
using C.B.Models.Contracts;
using C.B.Models.Data;
using C.B.MySql.Data;
using C.B.MySql.Repository.EntityRepositories;

namespace C.B.MySql.Repository.Services {
    public class EditorService {
        private IMapper _mapper;
        private EventTypeRepository _eventTypeRepository;
        private EventInfoRepository _eventInfoRepository;
        private NoticeRepository _noticeRepository;
        private NewsInfoRepository _newsInfoRepository;
        private MessageRepository _messageRepository;
        private ExpertInfoRepository _expertInfoRepository;
        private DocumentRepository _documentRepository;

        public EditorService (IMapper mapper) {
            _eventTypeRepository = new EventTypeRepository ();
            _eventInfoRepository = new EventInfoRepository ();

            _noticeRepository = new NoticeRepository ();
            _newsInfoRepository = new NewsInfoRepository ();
            _messageRepository = new MessageRepository ();
            _expertInfoRepository = new ExpertInfoRepository ();
            _documentRepository = new DocumentRepository ();

            _mapper = mapper;
        }

        public EditorModel GetEditorModel (string type, int typeId = 0, int id = 0) {
            System.Console.WriteLine ($"service GetEditorModel ====>>  type：{type}, id:{id}, typeId:{typeId}");
            EditorModel model = null;
            switch (type) {
                case "event":
                    model = GetEventModel (typeId, id);
                    break;
                case "expert":
                    model = GetExpertModel (id);
                    break;
                case "news":
                    model = GetNewsModel (id);
                    break;
                case "notice":
                    model = GetNoticeModel (id);
                    break;
            }
            return model;
        }

        #region -  read  -

        private EditorModel GetEventModel (int typeId = 0, int id = 0) {
            var model = new EditorModel ();
            if (id != 0) {
                var eventInfo = _eventInfoRepository.FirstOrDefault (id);
                if (eventInfo != null) {
                    model = _mapper.Map<EditorModel> (eventInfo);
                    typeId = eventInfo.EventId;
                    var doc = _documentRepository.FirstOrDefault (model.documentId);
                    model.document = _mapper.Map<DocumentModel> (doc);
                }
            }
            if (typeId != 0) {
                var type = _eventTypeRepository.FirstOrDefault (typeId);
                if (type != null) {
                    model.TypeId = typeId;
                    model.TypeName = type.Name;
                }
            }
            return model;
        }

        private EditorModel GetExpertModel (int id = 0) {
            var model = new EditorModel ();

            if (id != 0) {
                var expert = _expertInfoRepository.FirstOrDefault (id);
                if (expert != null) {
                    model = _mapper.Map<EditorModel> (expert);
                    var doc = _documentRepository.FirstOrDefault (model.documentId);
                    model.document = _mapper.Map<DocumentModel> (doc);
                }
            }
            return model;
        }

        private EditorModel GetNewsModel (int id = 0) {
            var model = new EditorModel ();

            if (id != 0) {
                var news = _newsInfoRepository.FirstOrDefault (id);
                if (news != null) {
                    model = _mapper.Map<EditorModel> (news);
                    var doc = _documentRepository.FirstOrDefault (model.documentId);
                    model.document = _mapper.Map<DocumentModel> (doc);
                }
            }
            return model;
        }

        private EditorModel GetNoticeModel (int id = 0) {
            var model = new EditorModel ();

            if (id != 0) {
                var notice = _noticeRepository.FirstOrDefault (id);
                if (notice != null) {
                    model = _mapper.Map<EditorModel> (notice);
                    var doc = _documentRepository.FirstOrDefault (model.documentId);
                    model.document = _mapper.Map<DocumentModel> (doc);
                }
            }
            return model;
        }

        #endregion

        public bool ModifyEditor (EditorModel model) {
            var document = SaveDocument (model);
            var result = false;
            switch (model.EditType) {
                case "event":
                    result = ModifyEventInfo (model, document);
                    break;
                case "message":
                    result = ModifyMessageInfo (model);
                    break;
                case "notice":
                    result = ModifyNoticeInfo (model, document);
                    break;
                case "expert":
                    result = ModifyExpertInfo (model, document);
                    break;
                case "news":
                    result = ModifyNewsInfo (model, document);
                    break;
            };
            return result;
        }

        private Document SaveDocument (EditorModel model) {
            var document = new Document () {
                Id = (long) IdGenerator.GetLongId (),
                Title = model.Title,
                SubTitle = model.SubTitle,
                Author = model.Author,
                PublishTime = DateTime.Now,
                DocType = model.DocUrl.IsNotEmpty () ? 2 : 1,
            };
            if (model.DocUrl.IsEmpty ())
                document.Content = model.Content;
            else {
                var apiClient = new HttpClientHelper ();
                var baseUrl = Common.Config.AppSettingConfig.Get ("FWork_Office_API");

                var request = new DocRequest { filePath = model.DocUrl };
                var response = apiClient.DoPostPut<object> (System.Net.Http.HttpMethod.Post, baseUrl, request);
                // var url = $"{baseUrl}?filePath={model.DocUrl}";
                // var response = apiClient.GetString (url);
                var result = response.DesJson<BaseResponse<DocResponse>> ();
                if (result.Success) {
                    document.Content = result.Data.content;
                    document.SimpleContent = result.Data.content;
                    document.Url = $"../Sources/html/{result.Data.htmlPath.Replace('\\','/')}";
                }
            }
            var r = _documentRepository.Insert (document);
            System.Console.WriteLine ($"------> insert document , {r}");
            return document;
        }

        #region -  modify   -

        private bool ModifyEventInfo (EditorModel m, Document document) {
            var model = new EventInfo {
                Id = m.Id,
                EventId = m.TypeId,
                Title = document.Title,
                Content = document.Content,
                Author = document.Author,

                ThumbId = m.ImageId,
                ThumbUrl = m.ImageUrl,

                IsShow = m.IsShow ? 1 : 0,
                IsTop = m.IsTop ? 1 : 0,
                DocumentId = document.Id,
            };
            var result = 0;
            if (model.Id > 0)
                result = _eventInfoRepository.Update (model);
            else
                result = _eventInfoRepository.Insert (model);
            return result > 0;
        }
        private bool ModifyNewsInfo (EditorModel m, Document document) {
            var news = _newsInfoRepository.FirstOrDefault (m.Id);
            if (news == null) news = new NewsInfo ();

            news.Id = m.Id;
            news.NewsType = (NewsType) m.NewsType;
            news.Title = document.Title;
            news.Content = document.Content;
            news.Author = document.Author;

            news.PubOrg = m.PubOrg;
            news.ThumbId = m.ImageId;
            news.ThumUrl = m.ImageUrl.IsEmpty () ? news.ThumUrl : m.ImageUrl;
            news.VideoId = m.VideoId;
            news.VideoUrl = m.VideoUrl.IsEmpty () ? news.VideoUrl : m.VideoUrl;

            news.IsShow = m.IsShow ? 1 : 0;
            news.IsTop = m.IsTop ? 1 : 0;
            news.IsRoll = m.IsRoll ? 1 : 0;
            news.SortNo = DateTime.Now.ToOADate ();
            news.DocumentId = document.Id;

            var result = 0;
            if (news.Id > 0)
                result = _newsInfoRepository.Update (news);
            else
                result = _newsInfoRepository.Insert (news);
            return result > 0;
        }
        private bool ModifyMessageInfo (EditorModel m) {
            var model = new Message {
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
                SortNo = DateTime.Now.ToOADate (),
            };
            var result = 0;
            if (model.Id > 0)
                result = _messageRepository.Update (model);
            else
                result = _messageRepository.Insert (model);
            return result > 0;
        }
        private bool ModifyNoticeInfo (EditorModel m, Document document) {
            var model = new Notice {
                Id = m.Id,
                Title = document.Title,
                Content = document.Content,
                Author = document.Author,
                IsShow = m.IsShow ? 1 : 0,
                IsTop = m.IsTop ? 1 : 0,
                IsRoll = m.IsRoll ? 1 : 0, //?????????????????????????????????????
                PubTime = DateTime.Now,
                PubOrg = "PubOrg",
                SortNo = DateTime.Now.ToOADate (),
                DocumentId = document.Id,
            };
            var result = 0;
            if (model.Id > 0)
                result = _noticeRepository.Update (model);
            else
                result = _noticeRepository.Insert (model);
            return result > 0;
        }
        private bool ModifyExpertInfo (EditorModel m, Document document) {
            var model = new ExpertInfo {
                Id = m.Id,
                Type = m.ExpertType,
                Title = document.Title,
                Content = document.Content,
                Author = document.Author,
                IsShow = m.IsShow ? 1 : 0,
                SortNo = DateTime.Now.ToOADate (),
                PicFileId = m.ImageId,
                PicUrl = m.ImageUrl,
                DocumentId = document.Id,
            };
            var result = 0;
            if (model.Id > 0)
                result = _expertInfoRepository.Update (model);
            else
                result = _expertInfoRepository.Insert (model);
            return result > 0;
        }

        #endregion

        public bool DeleteEditorModel (string type, int id = 0) {
            System.Console.WriteLine ($"service DeleteEditorModel ====>>  type：{type}, id:{id}");
            var result = 0;
            switch (type) {
                case "event":
                    result = _eventInfoRepository.Delete (id);
                    break;
                case "expert":
                    result = _expertInfoRepository.Delete (id);
                    break;
                case "news":
                    result = _newsInfoRepository.Delete (id);
                    break;
                case "notice":
                    result = _noticeRepository.Delete (id);
                    break;
            }
            return result > 0;
        }

    }
}