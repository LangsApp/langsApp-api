using AutoMapper;
using LangApp.Core.Models;
using LangApp.BLL.LangCode.DTOs;

namespace LangApp.BLL.LangCode.Mapping;

public class LangCodeProfile : Profile
{
    public LangCodeProfile()
    {
        CreateMap<Languages, CreateLangCodeDTO>();
        CreateMap<CreateLangCodeDTO, Languages>();
    }
}
