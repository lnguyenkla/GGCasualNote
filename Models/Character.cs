using System.ComponentModel.DataAnnotations.Schema;

namespace GGCasualNote.Models;

public class Character
{
    public string CharacterId { get; set; }
    public string Name { get; set; }

    [NotMapped]
    public List<ComboNote> ComboNotes { get; } = new();
    public List<MatchupNote> MatchupNotes { get; } = new();
    public List<Move> Moves { get; } = new();
    public List<MoveListTimestamp> MoveListTimestamps { get; } = new();
}