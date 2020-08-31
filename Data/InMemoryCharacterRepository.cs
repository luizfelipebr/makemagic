using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using makemagic.Models;
using Microsoft.EntityFrameworkCore;

namespace makemagic.Data
{
  public class InMemoryCharacterRepository : ICharacterRepository
  {
    private readonly MakeMagicContext _context;

    public InMemoryCharacterRepository(MakeMagicContext context)
    {
      _context = context;
    }

    public async Task CreateCharacterAsync(Character character)
    {
      if (character == null)
      {
        throw new ArgumentNullException(nameof(character));
      }

      await _context.Characters.AddAsync(character);
    }

    public void DeleteCharacter(Character character)
    {
      if (character == null)
      {
        throw new ArgumentNullException(nameof(character));
      }

      _context.Characters.Remove(character);
    }

    public async Task<List<Character>> GetAllCharactersAsync()
    {
      return await _context.Characters.ToListAsync();
    }

    public async Task<Character> GetCharacterByIdAsync(int id)
    {
      return await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Character>> GetAllCharactersByHouseAsync(string house)
    {
      return await _context.Characters.Where(c => c.House == house).ToListAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
      return await _context.SaveChangesAsync();
    }

    public async void UpdateCharacter(int id, Character character)
    {
      if (character == null)
      {
        throw new ArgumentNullException(nameof(character));
      }
      var characterFromRepo = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

      _context.Characters.Update(characterFromRepo);
    }
  }
}