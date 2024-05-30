namespace GGCasualNote.Models;

public class Move
{
    public int MoveId { get; set; }
    public string CharacterId { get; set; }
    public string Input { get; set; }
    public string? Name { get; set; }
}