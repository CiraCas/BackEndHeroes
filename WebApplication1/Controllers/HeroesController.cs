using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly WebApplication1Context _context;

        public HeroesController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Heroe>>> GetHeroe()
        {
            return await _context.Heroe.ToListAsync();
        }

        // GET: api/Heroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Heroe>> GetHeroe(int id)
        {
            var heroe = await _context.Heroe.FindAsync(id);

            if (heroe == null)
            {
                return NotFound();
            }

            return heroe;
        }

        // GET: api/Heroes/name/b
        [HttpGet("name/{letra}")]
        public async Task<ActionResult<IEnumerable<Heroe>>> GetHeroeByName(string letra)
        {
            var heroe = await _context.Heroe.Where(h => h.name.Contains(letra)).ToListAsync();

            if (heroe == null)
            {
                return NotFound();
            }

            return heroe;
        }


        // PUT: api/Heroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHeroe(int id, Heroe heroe)
        {
            if (id != heroe.id)
            {
                return BadRequest();
            }

            _context.Entry(heroe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Heroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Heroe>> PostHeroe(Heroe heroe)
        {
            _context.Heroe.Add(heroe);
            try { await _context.SaveChangesAsync(); 
            }catch (Exception x )
            {

            }

            //return CreatedAtAction("GetHeroe", new { id = heroe.id }, heroe);
            return CreatedAtAction(nameof(GetHeroe), new { id = heroe.id }, heroe);
        }

        // DELETE: api/Heroes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHeroe(int id)
        {
            var heroe = await _context.Heroe.FindAsync(id);
            if (heroe == null)
            {
                return NotFound();
            }

            _context.Heroe.Remove(heroe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeroeExists(int id)
        {
            return _context.Heroe.Any(e => e.id == id);
        }
    }
}
