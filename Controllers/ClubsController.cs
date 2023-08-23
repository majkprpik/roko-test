using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using roko_test.Data;
using roko_test.Entities;
using roko_test.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace roko_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClubController : ControllerBase
    {
        private readonly DataContext _context;

        public ClubController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Club>> Get()
        {
            var clubs = await _context.Clubs.ToListAsync();

            var clubDtos = clubs.Select(club => new Club
            {
                Name = club.Name,
                City = club.City
            }).ToList();

            return clubDtos;
        }

        [HttpGet("{id}")]
        public async Task<Club> Get(int id)
        {
            var club = await _context.Clubs.FirstOrDefaultAsync(c => c.Id == id);

            if (club == null)
            {
                return null; // Return a 404 Not Found response if the club is not found
            }

            var clubDto = new Club
            {
                Name = club.Name,
                City = club.City
            };

            return clubDto;
        }

    }
}
