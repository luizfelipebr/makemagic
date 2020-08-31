using System.Collections.Generic;
using System.Threading.Tasks;
using makemagic.Models;

namespace makemagic.Data
{
  public interface ICharacterRepository
  {
    Task<int> SaveChangesAsync();
    Task<List<Character>> GetAllCharactersAsync();
    Task<Character> GetCharacterByIdAsync(int id);
    Task<List<Character>> GetAllCharactersByHouseAsync(string house);
    Task CreateCharacterAsync(Character character);
    void UpdateCharacter(int id, Character character);
    void DeleteCharacter(Character character);
  }
}