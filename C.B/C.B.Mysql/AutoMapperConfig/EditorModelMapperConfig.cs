

using AutoMapper;
using C.B.Models.Data;
using C.B.MySql.Data;

namespace C.B.MySql.AutoMapperConfig
{


    public class EditorModelMapperConfig : Profile
    {
        public EditorModelMapperConfig()
        {
            CreateMap<EventInfo, EditorModel>();
            CreateMap<ExpertInfo, EditorModel>();
            CreateMap<NewsInfo, EditorModel>();
            CreateMap<Notice, EditorModel>();

        }
    }


}