using GGCasualNote.Models;
using GGCasualNote.Repositories;
using HtmlAgilityPack;

namespace GGCasualNote.Services;

public class ScrapService
{
    private readonly MoveRepository _moveRepo;
    private readonly MoveListTimestampRepository _timestampRepo;
    
    public ScrapService(MoveRepository moveRepo,
        MoveListTimestampRepository timestampRepo)
    {
        _moveRepo = moveRepo;
        _timestampRepo = timestampRepo;
    }

    public async Task<HashSet<string>> ScrapMoveList(string characterId)
    {
        var html = $@"https://www.dustloop.com/w/GGST/{characterId}";
        var web = new HtmlWeb();
        var htmlDoc = web.Load(html);

        var normalMoveSection = htmlDoc.DocumentNode.SelectNodes("//section[@id='citizen-section-2']").FirstOrDefault();

        var excludedMoves = new List<string>() { 
            "Wild Assault", "Orange", "j.4D", "j.6D", "236D", "6D", "4D",
            "5P", "2P", "j.P",
            "5K", "2K", "j.K",
            "5S", "2S", "j.S",
            "5H", "2H", "j.H",
            "5D", "2D", "j.D",
            "f.S", "c.S", "P", "S", "K", "H", "6P", "6H"
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
            return moveList;
        }

        await _moveRepo.DeleteMoves(characterId);
        
        var addingMoves = moveList.Select(moveInput => new Move() { CharacterId = characterId, Input = moveInput }).ToList();
        
        await _moveRepo.CreateMoves(addingMoves);

        await _timestampRepo.CreateTimestamp(new MoveListTimestamp()
        {
            CharacterId = characterId,
            LastUpdated = DateTime.Now.ToLocalTime()
        });

        return moveList;
    }

    public async Task<IEnumerable<Move>> GetLastestMoveList(string characterId)
    {
        bool isMoveListStale = await IsMoveListStale(characterId);

        if (isMoveListStale)
        {
            await ScrapMoveList(characterId);

            return await _moveRepo.GetAllMoves(characterId);
        }

        return await _moveRepo.GetAllMoves(characterId);
    }

    private async Task<bool> IsMoveListStale(string characterId)
    {
        DateTime? lastUpdated = await _timestampRepo.GetMostRecentTimestamp(characterId);

        if (!lastUpdated.HasValue)
        {
            return true;
        }
        
        DateTimeOffset startTime = new DateTimeOffset(lastUpdated.Value);
        DateTimeOffset endTime = DateTime.Now.ToLocalTime();

        // add three days in seconds for stale period
        const long staleThresholdInSeconds = 259200;

        bool result = (endTime.ToUnixTimeSeconds() - startTime.ToUnixTimeSeconds() > staleThresholdInSeconds)
            ? true
            : false;

        return result;
    }
}