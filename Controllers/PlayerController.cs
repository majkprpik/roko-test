using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roko_test.Data;
using roko_test.Entities;
using roko_test.DTO;

namespace roko_test.Controllers;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly DataContext _context;

    public PlayerController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<PlayerDto> Get(int id)
    {
        var player = await _context.Players
             .Where(u => u.Id == id)
             .FirstOrDefaultAsync();

        var playerDto = new PlayerDto();
        playerDto.FirstName = player.FirstName;
        playerDto.LastName = player.LastName;

        return playerDto;
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
