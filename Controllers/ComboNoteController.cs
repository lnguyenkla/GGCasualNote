using GGCasualNote.Models;
using GGCasualNote.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GGCasualNote.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComboNoteController
{
    private readonly ComboNoteRepository _comboNoteRepo;
    
    public ComboNoteController(ComboNoteRepository comboNoteRepo)
    {
        _comboNoteRepo = comboNoteRepo;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ComboNote>> Get(string characterId)
    {
        return await _comboNoteRepo.GetComboNotes(characterId);
    }

    [HttpPost]
    public async Task<ComboNote> Post(ComboNote comboNote)
    {
        return await _comboNoteRepo.CreateComboNote(comboNote);
    }

    [HttpPut]
    public async Task<ComboNote> Put(ComboNote comboNote)
    {
        return await _comboNoteRepo.UpdateComboNote(comboNote);
    }

    [HttpDelete]
    public async Task<string> Delete(int comboNoteId)
    {
        return await _comboNoteRepo.DeleteComboNote(comboNoteId);
    }
}