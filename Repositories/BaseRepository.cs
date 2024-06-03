using GGCasualNote.Models;

namespace GGCasualNote.Repositories;

public class BaseRepository
{
    protected readonly GgNoteContext dbContext;

    protected BaseRepository(GgNoteContext context)
    {
        dbContext = context;
    }
}