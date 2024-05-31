using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace GGCasualNote.Models;

public class GgNoteContext : DbContext
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<ComboNote> ComboNotes { get; set; }
    public DbSet<MatchupNote> MatchupNotes { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<MoveListTimestamp> MoveListTimestamps { get; set; }
    
    public string DbPath { get; }

    public GgNoteContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "gg-casual-note.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}