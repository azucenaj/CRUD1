using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CRUD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD1Controller : ControllerBase
    {
        
        private static List<CRUD1> heroes = new List<CRUD1>
            {
                    new CRUD1() { Id = 1,
                        Name = "Spiderman",
                        FirstName = "Peter",
                        LastName = "Parker",
                        Place = "New York"},

                    new CRUD1() { Id = 2,
                        Name = "Ironman",
                        FirstName = "Tony",
                        LastName = "Stark",
                        Place= "Belgium" }

            };
        
        private readonly DataContext _context;

        public CRUD1Controller(DataContext context) { 
            _context = context; 
        
        }


        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<CRUD1>>> Get()
        {
            return Ok(await _context.CRUD1.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CRUD1>> Get(int id)
        {
            var hero = _context.CRUD1.FindAsync(id);
            if (hero == null)
                return BadRequest(" Hero not available");
            return Ok(hero);
        }
        [HttpPost]       
        public async Task<ActionResult<List<CRUD1>>>  AddHero(CRUD1 hero)
        {
            _context.CRUD1.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.CRUD1.ToListAsync());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<CRUD1>>> UpdateHero(int id,CRUD1 request)
        {
            var dbhero = await _context.CRUD1.FindAsync(id);
            if (dbhero == null)
                return BadRequest("Hero not available");
            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;   
            dbhero.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.CRUD1.ToListAsync());
        }
       
       

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CRUD1>>> Delete(int id)
        {
            var dbhero = _context.CRUD1.FindAsync(id);
            if (dbhero == null)
                return BadRequest("Hero not available");
            
            _context.CRUD1.Remove(await dbhero);
            await _context.SaveChangesAsync();
            return Ok(await _context.CRUD1.ToListAsync());
        }
    }
}
    


