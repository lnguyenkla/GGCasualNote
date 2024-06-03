using GGCasualNote.Models;
using Microsoft.EntityFrameworkCore;

namespace GGCasualNote.Repositories;

public class MoveListTimestampRepository : BaseRepository
{
    public MoveListTimestampRepository(GgNoteContext context) : base(context)
    {
        
    }

    public async Task<DateTime?> GetMostRecentTimestamp(string characterId)
    {
        var queryResult = await dbContext.MoveListTimestamps
            .Where(t => t.CharacterId.Equals(characterId))
            .OrderByDescending(t => t)
            .FirstOrDefaultAsync();

        return queryResult?.LastUpdated;
    }

    public async Task<MoveListTimestamp> CreateTimestamp(MoveListTimestamp timestamp)
    {
        await dbContext.AddAsync(timestamp);
        await dbContext.SaveChangesAsync();

        return timestamp;
    }
}