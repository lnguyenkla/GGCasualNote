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
    
    [HttpGet("id")]
    public async Task<ActionResult<IEnumerable<ComboNote>>> Get(int charId)
    {
        return await _context.ComboNotes.Where(cn => cn.CharacterId == charId).ToListAsync();
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
    {
        return await _context.Characters.ToListAsync();
    }
    
    [HttpPost]
    public ActionResult<string> Post(Character character)
    {
        _context.Characters.Add(character);
        _context.SaveChanges();
    
        return new ActionResult<string>("OK");
    }
    
    [HttpPut("create-move-list")]
    public ActionResult<IEnumerable<string>> UpdateMoveList(string character)
    {
        var html = $@"https://www.dustloop.com/w/GGST/{character}";
        HtmlWeb web = new HtmlWeb();
        var htmlDoc = web.Load(html);

        var normalMoveSection = htmlDoc.DocumentNode.SelectNodes("//section[@id='citizen-section-2']").First();

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
        
        return new ActionResult<IEnumerable<string>>(moveList);
    }
}