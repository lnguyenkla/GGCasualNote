using GGCasualNote.Models;
using GGCasualNote.Repositories;
using GGCasualNote.Services;
using Microsoft.AspNetCore.Mvc;

namespace GGCasualNote.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoveController
{
    private readonly MoveRepository _moveRepo;
    private readonly ScrapService _scrapService;
    
    public MoveController(MoveRepository moveRepo,
        ScrapService scrapService)
    {
        _moveRepo = moveRepo;
        _scrapService = scrapService;
    }

    [HttpGet]
    public async Task<IEnumerable<Move>> Get(string characterId)
    {
        return await _scrapService.GetLastestMoveList(characterId);
    }

    [HttpPut]
    public async Task<HashSet<string>> Put(string characterId)
    {
        return await _scrapService.ScrapMoveList(characterId);
    }
}