using AutoMapper;
using LangApp.Core.Models;
using LangApp.BLL.Words.DTOs;


namespace LangApp.BLL.Words.Mapping;

public class WordsProfile : Profile
{
    public WordsProfile()
    {
        CreateMap<BaseWord, CreateBaseWordsDTO>();
        CreateMap<CreateBaseWordsDTO, BaseWord>();
        CreateMap<BaseWord, AddWordsByCategoryDTO>();
    }
}
