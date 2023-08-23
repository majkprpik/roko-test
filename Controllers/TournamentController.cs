using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roko_test.Data;
using roko_test.Entities;
using roko_test.DTO;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace roko_test.Controllers;

[ApiController]
[Route("[controller]")]
public class TournamentController : ControllerBase
{
    private readonly DataContext _context;

    public TournamentController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<TournamentDTO>> Get()
    {
       var tournaments = await _context.Tournaments.ToListAsync();

       var TournamentDtos = tournaments.Select(tournament => new TournamentDTO{
            Name = tournament.Name,
            Date = tournament.Date
       }).ToList();

       return TournamentDtos;
    }
    [HttpGet("{id}")]
    public async Task<Tournament> GetTournamentInfo(int id)
    {
        var tournament = await _context.Tournaments.Include(t => t.Clubs).ThenInclude(c=>c.Players).FirstOrDefaultAsync(c => c.Id == id);
        
        //var clubsOnTournament = await _context.Clubs.Where(c => c.Id == id).ToListAsync();
        
        if(tournament == null){
            return null;
       
        }
        
        var clubsInTournament = tournament.Clubs.ToList();
        foreach (var club in clubsInTournament)
        {
            var playersInClub = club.Players.ToList();
        }
       
       var Tournament = new Tournament
       {
            Id = id,
            Name = tournament.Name,
            Date = tournament.Date,
            Clubs = clubsInTournament
            };

       return Tournament;
    }

    // [HttpPost]
    // public async Task<bool> Post([FromBody] CreateUser userFromApi)
    // {
    //     var user = new User();
    //     user.Username = userFromApi.Username;
    //     user.FirstName = userFromApi.FirstName;
    //     user.LastName = userFromApi.LastName;
    //     user.Email = userFromApi.Email;
    //     user.DateOfBirth = userFromApi.DateOfBirth.ToUniversalTime();
    //     user.PhoneNumber = userFromApi.PhoneNumber;
    //     user.Address = userFromApi.Address;
    //     user.City = userFromApi.City;
    //     user.State = userFromApi.State;
    //     user.Country = userFromApi.Country;
    //     _context.Users.Add(user);
    //     await _context.SaveChangesAsync();

    //     return true;
    // }
}
