using GGCasualNote.Models;
using Microsoft.EntityFrameworkCore;

namespace GGCasualNote.Repositories;

public class ComboNoteRepository : BaseRepository
{
    public ComboNoteRepository(GgNoteContext context) : base(context)
    {
        
    }

    public async Task<IEnumerable<ComboNote>> GetComboNotes(string characterId)
    {
        return await dbContext.ComboNotes.Where(cn => cn.CharacterId.Equals(characterId)).ToListAsync();
    }

    public async Task<ComboNote> CreateComboNote(ComboNote comboNote)
    {
        await dbContext.ComboNotes.AddAsync(comboNote);
        await dbContext.SaveChangesAsync();

        return comboNote;
    }

    public async Task<ComboNote> UpdateComboNote(ComboNote comboNote)
    {
        dbContext.ComboNotes.Update(comboNote);
        await dbContext.SaveChangesAsync();

        return comboNote;
    }

    public async Task<string> DeleteComboNote(int comboNoteId)
    {
        var deletingNote = dbContext.ComboNotes.FirstOrDefault(n => n.ComboNoteId == comboNoteId);

        if (deletingNote == null)
        {
            return $"Cannot find note ID {comboNoteId}";    
        }
        
        dbContext.ComboNotes.Remove(deletingNote);
        await dbContext.SaveChangesAsync();

        return $"Deleted note {comboNoteId} of character {deletingNote.CharacterId}";
    }
}