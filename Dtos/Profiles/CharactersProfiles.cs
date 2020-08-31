using AutoMapper;
using makemagic.Models;

namespace makemagic.Dtos.Profiles
{
  public class CharactersProfiles : Profile
  {
    public CharactersProfiles()
    {
      CreateMap<Character, CharacterReadDto>();
      CreateMap<CharacterCreateDto, Character>();
      CreateMap<CharacterUpdateDto, Character>();
      CreateMap<Character, CharacterUpdateDto>();
    }
  }
}