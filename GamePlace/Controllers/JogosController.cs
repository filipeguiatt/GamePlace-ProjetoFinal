using GamePlace.Data;
using GamePlace.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
        Favoritos fav = new Favoritos();
        // GET: Jogos
        public async Task<IActionResult> Index(string searchString, string favorito, int jogoFav, Jogos jogo) 
        {
            if (favorito == "favorito")
            {
                var jogos = await _context.Jogos
                                   .Where(a => a.IdJogo.ToString() == jogoFav.ToString()).FirstOrDefaultAsync();
                fav.ListaJogos.Add(jogos);
                var gamePlaceDb = _context.Jogos.Include(r => r.IdJogo);
                //adicionar jogo aos favoritos na base de dados
                return View(await _context.Jogos.ToListAsync());
            }

            var jogoPesquisado = from m in _context.Jogos
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                jogoPesquisado = jogoPesquisado.Where(s => s.Nome.Contains(searchString));
            }
            else
            {
                var gamePlaceDb = _context.Jogos.Include(r => r.IdJogo);
                return View(await _context.Jogos.ToListAsync());
            }

            return View(await jogoPesquisado.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Nome,FotoCapa,Descricao,Tamanho,AnoLancamento,Genero,Preco,Classificacao,FaixaEtaria")]Jogos jogo, IFormFile fotoJogo, List<IFormFile> fotoJogoRecursos)
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

            // 1.
            // validar se a lista de Recurso está vazia
            // se estiver, fazer como na Capa
            if (fotoJogoRecursos == null)
            {
                // se aqui entro, não há foto
                // notificar o utilizador que há um erro
                ModelState.AddModelError("", "Deve selecionar uma recurso...");

                // devolver o controlo à View
                // prepara os dados a serem enviados para a View
                // para a Dropdown
                // ViewData["JogoFK"] = new SelectList(_db.Caes.OrderBy(c => c.Nome), "Id", "Nome");
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
                nomeFoto = jogo.IdJogo + "_" + g.ToString();
                string extensaoFoto = Path.GetExtension(fotoJogo.FileName).ToLower();
                nomeFoto = nomeFoto + extensaoFoto;

                // associar ao objeto 'foto' o nome do ficheiro
                jogo.FotoCapa = nomeFoto;
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


            try
            {
                if (ModelState.IsValid)
                {
                    // se o Estado do Modelo for válido 
                    // ie., se os dados do objeto 'foto' estiverem de acordo com as regras definidas
                    // no modelo (classe Fotografias)

                    // adicionar os dados da 'foto' à base de dados
                    _context.Add(jogo);
                    // vou guardar o ficheiro no disco rígido do servidor
                    // determinar onde guardar o ficheiro
                    string caminhoAteAoFichFoto = _dadosServidor.WebRootPath;
                    caminhoAteAoFichFoto = Path.Combine(caminhoAteAoFichFoto, "fotos", jogo.FotoCapa);
                    // guardar o ficheiro no Disco Rígido
                    using var stream = new FileStream(caminhoAteAoFichFoto, FileMode.Create);
                    await fotoJogo.CopyToAsync(stream);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocorreu um erro com a introdução dos dados da Fotografia.");
            }


            //////////////////////////////////////////////////LISTA-RECURSOS///////////////////////////////////////////////////////////////////////////////////



            // 2.
            // fazer aqui o código que está no Controller dos Recursos, para adicionar as fotos dos recursos
            // na prática é fazer até 3 vezes o que fizeram para a Foto Capa
            // em cada iteração, criar um objeto 'Recurso', adicionar-lhes os dados do recurso
            foreach (var imag in fotoJogoRecursos)
            {
                Recursos recurso = new Recursos();
                // há ficheiro. Mas, será do tipo correto (jpg/jpeg, png)?
                if (fotoJogo.ContentType == "image/png" || fotoJogo.ContentType == "image/jpeg")
                {
                    // o ficheiro é bom

                    // definir o nome do ficheiro
                    string nomeFoto = "";
                    Guid g;
                    g = Guid.NewGuid();
                    nomeFoto = jogo.IdJogo + "_" + g.ToString();
                    string extensaoFoto = Path.GetExtension(fotoJogo.FileName).ToLower();
                    nomeFoto = nomeFoto + extensaoFoto;


                    recurso.NomeRecurso = nomeFoto;
                    jogo.ListaRecursos.Add(recurso);
                    // associar ao objeto 'foto' o nome do ficheiro
                    //jogo.FotoCapa = nomeFoto;
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

                // 3. 
                // adicionar ao 'jogo' a lista de recursos


                //foreach (var recu in jogo.ListaRecursos)
                //{
                try
                {
                    if (ModelState.IsValid)
                    {
                        // se o Estado do Modelo for válido 
                        // ie., se os dados do objeto 'foto' estiverem de acordo com as regras definidas
                        // no modelo (classe Fotografias)

                        // adicionar os dados da 'foto' à base de dados
                        _context.Add(jogo);

                        // vou guardar o ficheiro no disco rígido do servidor
                        // determinar onde guardar o ficheiro
                        string caminhoAteAoFichFoto = _dadosServidor.WebRootPath;
                        caminhoAteAoFichFoto = Path.Combine(caminhoAteAoFichFoto, "fotos", recurso.NomeRecurso);
                        // guardar o ficheiro no Disco Rígido
                        using var stream = new FileStream(caminhoAteAoFichFoto, FileMode.Create);
                        imag.CopyTo(stream);

                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro com a introdução dos dados da Fotografia.");
                }
            }
            //jogo.ListaRecursos = listaRecursos;

            // consolidar as alterações na base de dados
            // COMMIT
            await _context.SaveChangesAsync();
            // redireciona a execução do código para a método Index
            return RedirectToAction(nameof(Index), "Jogos");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCompra(int jogoId)
        {

            if (!User.IsInRole("Admin"))
            {
                if (User.Identity.IsAuthenticated)
                {
                    //variável que vai buscar o Utilizador que escreveu a Review
                    var utilizador = _context.UtilizadorRegistado.Where(u => u.UserNameId == _userManager.GetUserId(User)).FirstOrDefault();
                    Guid g;
                    g = Guid.NewGuid();
                    var chave = g.ToString();

                    //Colocar nos dados da Review os daods introduzidos pelo Utilizador
                    var compra = new Compras
                    {
                        Utilizador = utilizador,
                        Data = DateTime.Now,
                        JogoFK = jogoId,
                        ChaveAtivacao = chave
                    };

                    //Adiciona a base de dados a review
                    _context.Add(compra);
                    await _context.SaveChangesAsync();
                    //Guarda as alterações feitas na base de dados
                    return RedirectToAction(nameof(Index), "Compras");
                }
                else
                {
                    ModelState.AddModelError("", "Please Login!");
                }
            }
            else
                ModelState.AddModelError("", "O Admin nao ira fazer compras!");
            return RedirectToAction(nameof(Details), new { id = jogoId });


        }
    }
}
