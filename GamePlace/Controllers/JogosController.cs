using GamePlace.Data;
using GamePlace.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GamePlace.Controllers
{
    public class JogosController : Controller
    {
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

        public JogosController(GamePlaceDb context,
         IWebHostEnvironment dadosServidor,
         UserManager<IdentityUser> userManager)
        {
            _context = context;
            _dadosServidor = dadosServidor;
            _userManager = userManager;
        }

        // GET: Jogos
        public async Task<IActionResult> Index()
        {
            var gamePlaceDb = _context.Jogos.Include(r => r.IdJogo);
            return View(await _context.Jogos.ToListAsync());
        }

        // GET: Jogos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

                var jogos = await _context.Jogos
                                       .Where(a => a.IdJogo == id)
                                       .Include(a => a.ListaRecursos)
                                       .FirstOrDefaultAsync();
            
           
            if (jogos == null)
            {
                return NotFound();
            }

            return View(jogos);
        }

        // GET: Jogos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jogos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJogo,Nome,FotoCapa,Descricao,Tamanho,AnoLancamento,Genero,Preco,Classificacao,FaixaEtaria")] Jogos jogos, IFormFile fotoJogo)
        {
            if (fotoJogo == null)
            {
                // se aqui entro, não há foto
                // notificar o utilizador que há um erro
                ModelState.AddModelError("", "Deve selecionar uma fotografia...");

                // devolver o controlo à View
                // prepara os dados a serem enviados para a View
                // para a Dropdown
                // ViewData["CaoFK"] = new SelectList(_db.Caes.OrderBy(c => c.Nome), "Id", "Nome");
                ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogo", "Nome");
                return View();
            }

            // há ficheiro. Mas, será do tipo correto (jpg/jpeg, png)?
            if (fotoJogo.ContentType == "image/png" || fotoJogo.ContentType == "image/jpeg")
            {
                // o ficheiro é bom

                // definir o nome do ficheiro
                string nomeFoto = "";
                Guid g;
                g = Guid.NewGuid();
                nomeFoto = jogos.IdJogo + "_" + g.ToString();
                string extensaoFoto = Path.GetExtension(fotoJogo.FileName).ToLower();
                nomeFoto = nomeFoto + extensaoFoto;

                // associar ao objeto 'foto' o nome do ficheiro
                jogos.FotoCapa = nomeFoto;
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
                ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogo", "Nome");
                return View();
            }
            if (jogos.IdJogo >= 0)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        // se o Estado do Modelo for válido 
                        // ie., se os dados do objeto 'foto' estiverem de acordo com as regras definidas
                        // no modelo (classe Fotografias)

                        // adicionar os dados da 'foto' à base de dados
                        _context.Add(jogos);

                        // consolidar as alterações na base de dados
                        // COMMIT
                        await _context.SaveChangesAsync();

                        // vou guardar o ficheiro no disco rígido do servidor
                        // determinar onde guardar o ficheiro
                        string caminhoAteAoFichFoto = _dadosServidor.WebRootPath;
                        caminhoAteAoFichFoto = Path.Combine(caminhoAteAoFichFoto, "fotos", jogos.FotoCapa);
                        // guardar o ficheiro no Disco Rígido
                        using var stream = new FileStream(caminhoAteAoFichFoto, FileMode.Create);
                        await fotoJogo.CopyToAsync(stream);

                        // redireciona a execução do código para a método Index
                        return RedirectToAction(nameof(Create), "Recursos");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro com a introdução dos dados da Fotografia.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Não se esqueça de escolher um jogo...");
            }

            //     ViewData["CaoFK"] = new SelectList(_db.Caes.OrderBy(c => c.Nome), "Id", "Nome", foto.CaoFK);
            ViewData["IdJogo"] = new SelectList(_context.Jogos, "IdJogo", "Nome");

            return View(jogos);
        }

        // GET: Jogos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogos = await _context.Jogos.FindAsync(id);
            if (jogos == null)
            {
                return NotFound();
            }
            return View(jogos);
        }

        // POST: Jogos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJogo,Nome,FotoCapa,Descricao,Tamanho,AnoLancamento,Genero,Preco,Classificacao,FaixaEtaria")] Jogos jogos)
        {
            if (id != jogos.IdJogo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogosExists(jogos.IdJogo))
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
            return View(jogos);
        }

        // GET: Jogos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogos = await _context.Jogos
                .FirstOrDefaultAsync(m => m.IdJogo == id);
            if (jogos == null)
            {
                return NotFound();
            }

            return View(jogos);
        }

        // POST: Jogos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogos = await _context.Jogos.FindAsync(id);
            _context.Jogos.Remove(jogos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogosExists(int id)
        {
            return _context.Jogos.Any(e => e.IdJogo == id);
        }
    }
}
