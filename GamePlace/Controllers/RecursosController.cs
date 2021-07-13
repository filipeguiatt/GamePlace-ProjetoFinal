using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamePlace.Data;
using GamePlace.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GamePlace.Controllers
{
    public class RecursosController : Controller
    {

        /// <summary>
        /// atributo que referencia a Base de Dados do projeto
        /// </summary>
        private readonly GamePlaceDb _context;

        /// <summary>
        /// Atributo que guarda nele os dados do Servidor
        /// </summary>
        private readonly IWebHostEnvironment _dadosServidor;

        /// <summary>
        /// Atributo que irá receber todos os dados referentes à
        /// pessoa q se autenticou no sistema
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        public RecursosController(GamePlaceDb context,
         IWebHostEnvironment dadosServidor,
         UserManager<IdentityUser> userManager)
        {
            _context = context;
            _dadosServidor = dadosServidor;
            _userManager = userManager;
        }

        // GET: Recursos
        [AllowAnonymous] // esta anotação anula o efeito do [Authorize]
        public async Task<IActionResult> Index(){

            var gamePlaceDb = _context.Recursos.Include(r => r.Jogo);
            return View(await gamePlaceDb.ToListAsync());
        }

        // GET: Recursos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recursos = await _context.Recursos
                .Include(r => r.Jogo)
                .FirstOrDefaultAsync(m => m.IdRecursos == id);
            if (recursos == null)
            {
                return NotFound();
            }

            return View(recursos);
        }

        // GET: Recursos/Create
        public IActionResult Create()
        {
            ViewData["JogoFK"] = new SelectList(_context.Jogos, "IdJogo", "Nome");
            return View();
        }

        // POST: Recursos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRecursos,NomeRecurso,JogoFK")] Recursos recursos, List<IFormFile> fotoJogoRecurso)
        {

            
            // avaliar se existe ficheiro
            if (fotoJogoRecurso == null)
            {
                // se aqui entro, não há foto
                // notificar o utilizador que há um erro
                ModelState.AddModelError("", "Deve selecionar uma fotografia...");

                // devolver o controlo à View
                // prepara os dados a serem enviados para a View
                // para a Dropdown
                // ViewData["CaoFK"] = new SelectList(_db.Caes.OrderBy(c => c.Nome), "Id", "Nome");
                ViewData["JogoFK"] = new SelectList(_context.Jogos, "IdJogo", "Nome");
                return View();
            }
            
            var tamanhoLista = fotoJogoRecurso.Count;

            foreach (var imag in fotoJogoRecurso)
            {

                    // há ficheiro. Mas, será do tipo correto (jpg/jpeg, png)?
                    if (imag.ContentType == "image/png" || imag.ContentType == "image/jpeg")
                    {
                        // o ficheiro é bom

                        // definir o nome do ficheiro
                        string nomeFoto = "";
                        Guid g;
                        g = Guid.NewGuid();
                        nomeFoto = recursos.JogoFK + "_" + g.ToString();
                        string extensaoFoto = Path.GetExtension(imag.FileName).ToLower();
                        nomeFoto = nomeFoto + extensaoFoto;

                        // associar ao objeto 'foto' o nome do ficheiro
                        recursos.NomeRecurso = nomeFoto;
                    }
                    else
                    {
                        // se aqui chego, há ficheiro, mas não foto
                        // se aqui entro, não há foto
                        // notificar o utilizador que há um erro
                        ModelState.AddModelError("", "Deve selecionar uma fotografia...");

                        // devolver o controlo à View
                        // prepara os dados a serem enviados para a View
                        // para a Dropdown
                        //  ViewData["CaoFK"] = new SelectList(_db.Caes.OrderBy(c => c.Nome), "Id", "Nome");
                        ViewData["JogoFK"] = new SelectList(_context.Jogos, "IdJogo", "Nome");
                        return View();
                    }
                    if (recursos.JogoFK > 0)
                    {
                        try
                        {
                            if (ModelState.IsValid)
                            {
                            // se o Estado do Modelo for válido 
                            // ie., se os dados do objeto 'foto' estiverem de acordo com as regras definidas
                            // no modelo (classe Fotografias)

                            // adicionar os dados da 'foto' à base de dados

                                _context.Recursos.Add(recursos);

                            // consolidar as alterações na base de dados
                            // COMMIT
                            await _context.SaveChangesAsync();

                            // vou guardar o ficheiro no disco rígido do servidor
                            // determinar onde guardar o ficheiro
                            string caminhoAteAoFichFoto = _dadosServidor.WebRootPath;
                                caminhoAteAoFichFoto = Path.Combine(caminhoAteAoFichFoto, "fotos", recursos.NomeRecurso);
                                // guardar o ficheiro no Disco Rígido
                                using var stream = new FileStream(caminhoAteAoFichFoto, FileMode.Create);
                                imag.CopyTo(stream);
                            }
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("", "Ocorreu um erro com a introdução dos dados da Fotografia." + e );
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Não se esqueça de escolher um jogo...");
                    }
            }
            
            // redireciona a execução do código para a método Index
            return RedirectToAction(nameof(Index), "Jogos");
        }

        // GET: Recursos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recursos = await _context.Recursos.FindAsync(id);
            if (recursos == null)
            {
                return NotFound();
            }
            ViewData["JogoFK"] = new SelectList(_context.Jogos, "IdJogo", "Nome", recursos.JogoFK);
            return View(recursos);
        }

        // POST: Recursos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRecursos,NomeRecurso,JogoFK")] Recursos recursos)
        {
            if (id != recursos.IdRecursos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recursos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecursosExists(recursos.IdRecursos))
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
            ViewData["JogoFK"] = new SelectList(_context.Jogos, "IdJogo", "Nome", recursos.JogoFK);
            return View(recursos);
        }

        // GET: Recursos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recursos = await _context.Recursos
                .Include(r => r.Jogo)
                .FirstOrDefaultAsync(m => m.IdRecursos == id);
            if (recursos == null)
            {
                return NotFound();
            }

            return View(recursos);
        }

        // POST: Recursos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recursos = await _context.Recursos.FindAsync(id);
            _context.Recursos.Remove(recursos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecursosExists(int id)
        {
            return _context.Recursos.Any(e => e.IdRecursos == id);
        }
    }
}
