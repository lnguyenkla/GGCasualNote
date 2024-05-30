namespace GGCasualNote.Models;

public class MatchupNote
{
    public int MatchupNoteId { get; set; }
    public string ContextNote { get; set; }
    
    public string CharacterId { get; set; }
    public string VsCharacterId { get; set; }
}