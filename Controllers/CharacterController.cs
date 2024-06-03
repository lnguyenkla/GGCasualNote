using GGCasualNote.Models;
using GGCasualNote.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GGCasualNote.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly CharacterRepository _charRepo;
    
    public CharacterController(CharacterRepository charRepo)
    {
        _charRepo = charRepo;
    }

    [HttpGet]
    public async Task<IEnumerable<Character>> Get()
    {
        return await _charRepo.GetAllCharacter();
    }

    [HttpPost]
    public async Task<Character> Post(Character character)
    {
        return await _charRepo.CreateCharacter(character);
    }
}