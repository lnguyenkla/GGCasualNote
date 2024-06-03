using GGCasualNote.Models;
using Microsoft.EntityFrameworkCore;

namespace GGCasualNote.Repositories;

public class MoveRepository : BaseRepository
{
    public MoveRepository(GgNoteContext context) : base(context)
    {
        
    }

    public async Task<List<Move>> GetAllMoves(string characterId)
    {
        return await dbContext.Moves.Where(m => m.CharacterId.Equals(characterId)).ToListAsync();
    }
    
    public async Task<List<Move>> CreateMoves(List<Move> moves)
    {
        await dbContext.AddRangeAsync(moves);
        await dbContext.SaveChangesAsync();

        return moves;
    }

    public async Task<List<Move>> DeleteMoves(string characterId)
    {
        var moves = await dbContext.Moves.Where(m => m.CharacterId.Equals(characterId)).ToListAsync();
        dbContext.Moves.RemoveRange(moves);

        await dbContext.SaveChangesAsync();

        return moves;
    }
}