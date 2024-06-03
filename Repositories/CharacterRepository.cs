using GGCasualNote.Models;
using Microsoft.EntityFrameworkCore;

namespace GGCasualNote.Repositories;

public class CharacterRepository : BaseRepository
{
    public CharacterRepository(GgNoteContext context) : base(context)
    {
    }

    public async Task<Character> CreateCharacter(Character character)
    {
        await dbContext.Characters.AddAsync(character);
        await dbContext.SaveChangesAsync();

        return character;
    }

    public async Task<IEnumerable<Character>> GetAllCharacter()
    {
        return await dbContext.Characters
            // .Include(c => c.CharacterId)
            // .Include(c => c.Name)
            .ToListAsync();
    }
}