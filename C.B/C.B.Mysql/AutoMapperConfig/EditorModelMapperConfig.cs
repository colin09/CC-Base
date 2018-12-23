using AutoMapper;
using C.B.Models.Data;
using C.B.MySql.Data;
using C.B.Common.Mapping;

namespace C.B.MySql.AutoMapperConfig
{


    public class EditorModelMapperConfig : Profile, IProfile
    {

        // protected override void Configure()
        // {

        // }

        public EditorModelMapperConfig()
        {
            CreateMap<EventInfo, EditorModel>()
                .ForMember(dest => dest.EditType, opt => opt.Ignore())
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(f => f.EventId))
                .ForMember(dest => dest.TypeName, opt => opt.Ignore())
                .ForMember(dest => dest.NewsType, opt => opt.Ignore())
                // .ForMember(dest => dest.ThumbId, opt => opt.Ignore())
                // .ForMember(dest => dest.ThumbUrl, opt => opt.Ignore())
                .ForMember(dest => dest.FileId, opt => opt.Ignore())
                .ForMember(dest => dest.FileUrl, opt => opt.Ignore())
                .ForMember(dest => dest.State, opt => opt.Ignore());


            CreateMap<ExpertInfo, EditorModel>()
                .ForMember(dest => dest.EditType, opt => opt.Ignore())
                .ForMember(dest => dest.TypeId, opt => opt.Ignore())
                .ForMember(dest => dest.TypeName, opt => opt.Ignore())
                .ForMember(dest => dest.NewsType, opt => opt.Ignore())
                .ForMember(dest => dest.ThumbId, opt => opt.Ignore())
                .ForMember(dest => dest.ThumbUrl, opt => opt.Ignore())
                .ForMember(dest => dest.FileId, opt => opt.Ignore())
                .ForMember(dest => dest.FileUrl, opt => opt.Ignore())
                .ForMember(dest => dest.State, opt => opt.Ignore());


            CreateMap<NewsInfo, EditorModel>()
                .ForMember(dest => dest.EditType, opt => opt.Ignore())
                .ForMember(dest => dest.TypeId, opt => opt.Ignore())
                .ForMember(dest => dest.TypeName, opt => opt.Ignore())
                //.ForMember(dest => dest.NewsType, opt => opt.MapFrom(f=>f.NewsType))
                .ForMember(dest => dest.ThumbId, opt => opt.Ignore())
                .ForMember(dest => dest.ThumbUrl, opt => opt.Ignore())
                .ForMember(dest => dest.FileId, opt => opt.Ignore())
                .ForMember(dest => dest.FileUrl, opt => opt.Ignore())
                .ForMember(dest => dest.State, opt => opt.Ignore());



            CreateMap<Notice, EditorModel>()
                .ForMember(dest => dest.EditType, opt => opt.Ignore())
                .ForMember(dest => dest.TypeId, opt => opt.Ignore())
                .ForMember(dest => dest.TypeName, opt => opt.Ignore())
                .ForMember(dest => dest.NewsType, opt => opt.Ignore())
                .ForMember(dest => dest.ThumbId, opt => opt.Ignore())
                .ForMember(dest => dest.ThumbUrl, opt => opt.Ignore())
                .ForMember(dest => dest.FileId, opt => opt.Ignore())
                .ForMember(dest => dest.FileUrl, opt => opt.Ignore())
                .ForMember(dest => dest.State, opt => opt.Ignore());


        }
    }


}