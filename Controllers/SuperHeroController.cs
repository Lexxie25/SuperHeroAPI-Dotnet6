using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heros = new List<SuperHero>
        { //commented out so that I can see that it was on it when the data base updates 
            //new SuperHero {
            //    Id = 1,
            //    Name ="Spider Man",
            //    FirstName = "Peter",
            //    Lastname = "Parker",
            //    Place = "New York City"
            //},
            // new SuperHero {
            //    Id = 2,
            //    Name ="Iron Man",
            //    FirstName = "Tony",
            //    Lastname = "Stark",
            //    Place = "Long Island"
            //}
        };
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await context.SuperHeros.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await context.SuperHeros.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero Not found.");
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            context.SuperHeros.Add(hero);
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeros.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await context.SuperHeros.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero Not found.");
            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.Lastname = request.Lastname;
            dbHero.Place = request.Place;

            await context.SaveChangesAsync();

            return Ok(await context.SuperHeros.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await context.SuperHeros.FindAsync(id);
            if (dbHero == null)
                return BadRequest("Hero Not found.");
            context.SuperHeros.Remove(dbHero);
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeros.ToListAsync());
        }
    }
}
