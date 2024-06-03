using GGCasualNote.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GGCasualNote.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoveListTimestampController
{
    private readonly MoveListTimestampRepository _timestampRepo;
    
    public MoveListTimestampController(MoveListTimestampRepository timestampRepo)
    {
        _timestampRepo = timestampRepo;
    }

    [HttpGet]
    public async Task<DateTime?> Get(string characterId)
    {
        return await _timestampRepo.GetMostRecentTimestamp(characterId);
    }
}