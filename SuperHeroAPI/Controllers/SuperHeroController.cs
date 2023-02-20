using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heros = new List<SuperHero>
            {
                new SuperHero
                {
                    Id = 1,
                    Name = "SpoiderMon",
                    FirstName = "Peter",
                    LastName = "Parkour",
                    Place = "New York City"
                },
                new SuperHero
                {
                    Id = 2,
                    Name = "IRonMan",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "CaveMan"
                }
            };
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> get(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero Not Found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeros.Add(hero);
            await _context.SaveChangesAsync();
            
            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero Request)
        {
            var DbHero = await _context.SuperHeros.FindAsync(Request.Id);
            if (DbHero == null)
                return BadRequest("Hero Not Found");

            DbHero.Name = Request.Name;
            DbHero.FirstName = Request.FirstName;
            DbHero.LastName = Request.LastName;
            DbHero.Place = Request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await _context.SuperHeros.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero Not Found");

            _context.SuperHeros.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeros.ToListAsync());
        }
    }
}
