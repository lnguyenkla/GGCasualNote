using GGCasualNote.Models;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GGCasualNote.Controllers;

[ApiController]
[Route("[controller]")]
public class GgNoteController : ControllerBase
{
    private readonly GgNoteContext _context;
    
    public GgNoteController(GgNoteContext context)
    {
        _context = context;
    }
    
    [HttpGet("get-combo-notes")]
    public async Task<ActionResult<IEnumerable<ComboNote>>> Get(string charId)
    {
        return await _context.ComboNotes.Where(cn => cn.CharacterId.Equals(charId)).ToListAsync();
    }

    [HttpPost("create-combo-note")]
    public async Task<ActionResult<string>> CreateComboNote(ComboNote comboNote)
    {
        await _context.ComboNotes.AddAsync(comboNote);
        await _context.SaveChangesAsync();

        return new ActionResult<string>("Note created");
    }
    
    [HttpPut("update-combo-note")]
    public ActionResult<string> UpdateComboNote(ComboNote comboNote)
    {
        _context.ComboNotes.Update(comboNote);
        _context.SaveChanges();

        return new ActionResult<string>("Note updated");
    }

    [HttpDelete("delete-combo-note")]
    public ActionResult<string> DeleteComboNote(int comboNoteId)
    {
        var deletingNote = _context.ComboNotes.Where(n => n.ComboNoteId == comboNoteId).FirstOrDefault();

        _context.ComboNotes.Remove(deletingNote);
        _context.SaveChanges();

        return new ActionResult<string>($"Deleted note {comboNoteId} of {deletingNote.CharacterId}");
    }
    
    
    [HttpGet("get-all-characters")]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
    {
        return await _context.Characters.ToListAsync();
    }
    
    [HttpPost("create-characters")]
    public ActionResult<string> Post(Character character)
    {
        _context.Characters.Add(character);
        _context.SaveChanges();
    
        return new ActionResult<string>("OK");
    }

    [HttpGet("get-move-list")]
    public ActionResult<IEnumerable<Move>> GetMoveList(string characterId)
    {
        return _context.Moves.Where(m => m.CharacterId.Equals(characterId)).ToList();
    }

    [HttpGet("move-list-last-updated")]
    public ActionResult<DateTime> GetLastUpdatedTime(string characterId)
    {
        return _context.MoveListTimestamps.Where(t => t.CharacterId.Equals(characterId)).OrderByDescending(t => t)
            .FirstOrDefault().LastUpdated;
    }
    
    [HttpPut("update-move-list")]
    public ActionResult<IEnumerable<string>> UpdateMoveList(string characterId)
    {
        var html = $@"https://www.dustloop.com/w/GGST/{characterId}";
        HtmlWeb web = new HtmlWeb();
        var htmlDoc = web.Load(html);

        var normalMoveSection = htmlDoc.DocumentNode.SelectNodes("//section[@id='citizen-section-2']").FirstOrDefault();

        var excludedMoves = new List<string>() { 
            "Wild Assault", "Orange", "j.4D", "j.6D", "236D", "6D", "4D",
            "5P", "2P", "j.P",
            "5K", "2K", "j.K",
            "5S", "2S", "j.S",
            "5H", "2H", "j.H",
            "5D", "2D", "j.D",
            "f.S", "c.S", "P", "S", "K", "H", "6P"
        };
        var moveList = new HashSet<string>();
        
        // Add all P moves
        moveList = moveList.Union(normalMoveSection.SelectNodes("//span[@class='colorful-text-1']").Select(e => e.InnerText.Trim())
            .ToHashSet()).ToHashSet();
        // Add all K moves
        moveList = moveList.Union(normalMoveSection.SelectNodes("//span[@class='colorful-text-2']").Select(e => e.InnerText.Trim())
            .ToHashSet()).ToHashSet();
        // Add all S moves
        moveList = moveList.Union(normalMoveSection.SelectNodes("//span[@class='colorful-text-3']").Select(e => e.InnerText.Trim())
            .ToHashSet()).ToHashSet();
        // Add all H moves
        moveList = moveList.Union(normalMoveSection.SelectNodes("//span[@class='colorful-text-4']").Select(e => e.InnerText.Trim())
            .ToHashSet()).ToHashSet();
        // Add all D moves
        moveList = moveList.Union(normalMoveSection.SelectNodes("//span[@class='colorful-text-5']").Select(e => e.InnerText.Trim())
            .ToHashSet()).ToHashSet();
        // Add all special moves
        moveList = moveList.Union(htmlDoc.DocumentNode.SelectNodes("//span[@class='input-badge']").Select(e => e.InnerText.Trim()).ToHashSet()).ToHashSet();
        // Exclude universal moves
        moveList = moveList.Except(excludedMoves).ToHashSet();

        if (moveList.Count == 0)
        {
            return new ActionResult<IEnumerable<string>>(moveList);
        }

        var deletingMoves = _context.Moves.Where(m => m.CharacterId.Equals(characterId)).ToList();
        _context.Moves.RemoveRange(deletingMoves);
        _context.SaveChanges();
        
        var addingMoves = moveList.Select(moveInput => new Move() { CharacterId = characterId, Input = moveInput }).ToList();
        _context.AddRange(addingMoves);

        _context.MoveListTimestamps.Add(new MoveListTimestamp()
        {
            CharacterId = characterId,
            LastUpdated = DateTime.Now.ToLocalTime()
        });
        
        _context.SaveChanges();

        return new ActionResult<IEnumerable<string>>(moveList);
    }
}