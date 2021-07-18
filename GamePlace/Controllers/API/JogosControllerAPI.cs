using GamePlace.Data;
using GamePlace.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GamePlace.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogosControllerAPI : ControllerBase
    {
        private readonly GamePlaceDb _context;

        private readonly IWebHostEnvironment _dadosServidor;

        public JogosControllerAPI(GamePlaceDb context, IWebHostEnvironment dadosServidor)
        {
            _context = context;
            _dadosServidor = dadosServidor;
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
        public async Task<IActionResult> PutJogos(int id, Jogos jogo, IFormFile fotoJogo)
        {
            if (id != jogo.IdJogo)
            {
                return BadRequest();
            }


            if (fotoJogo != null)
            {

                // há ficheiro. Mas, será do tipo correto (jpg/jpeg, png)?
                if (fotoJogo.ContentType == "image/png" || fotoJogo.ContentType == "image/jpeg")
                {

                    // associar ao objeto 'foto' o nome do ficheiro
                    jogo.FotoCapa = fotoJogo.FileName;




                    // vou guardar o ficheiro no disco rígido do servidor
                    // determinar onde guardar o ficheiro
                    string caminhoAteAoFichFoto = _dadosServidor.WebRootPath;
                    caminhoAteAoFichFoto = Path.Combine(caminhoAteAoFichFoto, "fotos", jogo.FotoCapa);
                    // guardar o ficheiro no Disco Rígido
                    using var stream = new FileStream(caminhoAteAoFichFoto, FileMode.Create);
                    fotoJogo.CopyTo(stream);


                }

            }
            else
            {
                Jogos jogosAux = _context.Jogos.Find(jogo.IdJogo);

                _context.Entry<Jogos>(jogosAux).State = EntityState.Detached;


                jogo.FotoCapa = jogosAux.FotoCapa;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogosExists(jogo.IdJogo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return CreatedAtAction("GetJogos", new { id = jogo.IdJogo }, jogo);

            _context.Entry(jogo).State = EntityState.Modified;

            return NoContent();
        }

        // POST: api/JogosControllerAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Jogos>> PostJogos([FromForm]Jogos jogo, IFormFile UpFoto)
        {


                jogo.FotoCapa = "";


            jogo.FotoCapa = UpFoto.FileName;
            // vou guardar o ficheiro no disco rígido do servidor
            // determinar onde guardar o ficheiro
            string caminhoAteAoFichFoto = _dadosServidor.WebRootPath;
            caminhoAteAoFichFoto = Path.Combine(caminhoAteAoFichFoto, "fotos", UpFoto.FileName);
            // guardar o ficheiro no Disco Rígido
            var stream = new FileStream(caminhoAteAoFichFoto, FileMode.Create);
            await UpFoto.CopyToAsync(stream);
            

            // adicionar os dados da 'foto' à base de dados
            _context.Add(jogo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJogos", new { id = jogo.IdJogo }, jogo);
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