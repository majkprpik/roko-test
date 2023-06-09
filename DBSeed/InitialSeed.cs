using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using roko_test.Entities;
using roko_test.Data;

namespace roko_test.Seed;
public static class DefaultSeeds
{

    public static async Task<bool> SeedAsync(DataContext dataContext)
    {
        // add clubs
        string clubsJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "clubs.json");
        List<Club> clubsList = JsonConvert.DeserializeObject<List<Club>>(clubsJSON);
        dataContext.Clubs.AddRange(clubsList);

        await dataContext.SaveChangesAsync();

        // add players
        string playersJSON = File.ReadAllText(@"Seed" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar + "players.json");
        List<Player> playersList = JsonConvert.DeserializeObject<List<Player>>(playersJSON);
        
        int i = 20;
        foreach (var player in playersList)
        {
            player.DateOfBirth = player.DateOfBirth.ToUniversalTime();
            dataContext.Add(player);
            dataContext.Entry(player).Property("ClubId").CurrentValue = ++i;
        }

        await dataContext.SaveChangesAsync();
        
        return true;
    }
}
