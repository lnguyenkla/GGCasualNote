using System.ComponentModel.DataAnnotations.Schema;

namespace GGCasualNote.Models;
using System;
using System.Collections.Generic;

public class ComboNote
{
    public int ComboNoteId { get; set; }
    public string NoteContext { get; set; }
    public string ComboString { get; set; }
    public string FootageUrl { get; set; }
    
    public string CharacterId { get; set; }

    // public List<Character> Characters { get; } = new();
}