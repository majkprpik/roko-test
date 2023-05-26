using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace roko_test.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly DataContext _context;

    public UsersController(ILogger<WeatherForecastController> logger, DataContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<User> Get(int id)
    {
        var user = await _context.Users
             .Where(u => u.Id == id)
             .FirstOrDefaultAsync();

        return user;
    }

    [HttpPost]
    public async Task<bool> Post([FromBody] CreateUser userFromApi)
    {
        var user = new User();
        user.Username = userFromApi.Username;
        user.FirstName = userFromApi.FirstName;
        user.LastName = userFromApi.LastName;
        user.Email = userFromApi.Email;
        user.DateOfBirth = userFromApi.DateOfBirth.ToUniversalTime();
        user.PhoneNumber = userFromApi.PhoneNumber;
        user.Address = userFromApi.Address;
        user.City = userFromApi.City;
        user.State = userFromApi.State;
        user.Country = userFromApi.Country;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }
}
