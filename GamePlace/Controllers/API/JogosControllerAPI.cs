using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamePlace.Data;
using GamePlace.Models;

namespace GamePlace.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogosControllerAPI : ControllerBase
    {
        private readonly GamePlaceDb _context;

        public JogosControllerAPI(GamePlaceDb context)
        {
            _context = context;
        }

        // GET: api/JogosControllerAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIJogosViewModel>>> GetJogos()
        {
            return await _context.Jogos.Select(j => new APIJogosViewModel
            {
                IdJogo = j.IdJogo,
                Nome = j.Nome,
                FotoCapa = j.FotoCapa,
                Descricao = j.Descricao
            }).OrderBy(j => j.IdJogo)
                .ToListAsync();
        }

        // GET: api/JogosControllerAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Jogos>> GetJogos(int id)
        {
            var jogos = await _context.Jogos.FindAsync(id);

            if (jogos == null)
            {
                return NotFound();
            }

            return jogos;
        }

        // PUT: api/JogosControllerAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJogos(int id, Jogos jogos)
        {
            if (id != jogos.IdJogo)
            {
                return BadRequest();
            }

            _context.Entry(jogos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JogosExists(id))
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

        // POST: api/JogosControllerAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jogos>> PostJogos(Jogos jogos)
        {
            _context.Jogos.Add(jogos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJogos", new { id = jogos.IdJogo }, jogos);
        }

        // DELETE: api/JogosControllerAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJogos(int id)
        {
            var jogos = await _context.Jogos.FindAsync(id);
            if (jogos == null)
            {
                return NotFound();
            }

            _context.Jogos.Remove(jogos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool JogosExists(int id)
        {
            return _context.Jogos.Any(e => e.IdJogo == id);
        }
    }
}
