using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamePlace.Data;

namespace GamePlace.Models
{
    public class ComprasController : Controller
    {
        private readonly GamePlaceDb _context;

        public ComprasController(GamePlaceDb context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var gamePlaceDb = _context.Compras.Include(c => c.Jogo).Include(c => c.Utilizador);
            return View(await gamePlaceDb.ToListAsync());
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras
                .Include(c => c.Jogo)
                .Include(c => c.Utilizador)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compras == null)
            {
                return NotFound();
            }

            return View(compras);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["JogoFK"] = new SelectList(_context.Jogos, "IdJogo", "Nome");
            ViewData["UtilizadorFK"] = new SelectList(_context.UtilizadorRegistado, "Id", "Id");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,Data,ChaveAtivacao,JogoFK,UtilizadorFK")] Compras compras)
        {

            string chave = "";
            Guid g;
            g = Guid.NewGuid();
            chave = g.ToString();
            compras.ChaveAtivacao = chave;

            if (ModelState.IsValid)
            {
                _context.Add(compras);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }




            ViewData["JogoFK"] = new SelectList(_context.Jogos, "IdJogo", "Nome", compras.JogoFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.UtilizadorRegistado, "Id", "Id", compras.UtilizadorFK);
            return View(compras);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras.FindAsync(id);
            if (compras == null)
            {
                return NotFound();
            }
            ViewData["JogoFK"] = new SelectList(_context.Jogos, "IdJogo", "IdJogo", compras.JogoFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.UtilizadorRegistado, "Id", "CodPostal", compras.UtilizadorFK);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompra,Data,ChaveAtivacao,JogoFK,UtilizadorFK")] Compras compras)
        {
            if (id != compras.IdCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compras);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComprasExists(compras.IdCompra))
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
            ViewData["JogoFK"] = new SelectList(_context.Jogos, "IdJogo", "IdJogo", compras.JogoFK);
            ViewData["UtilizadorFK"] = new SelectList(_context.UtilizadorRegistado, "Id", "CodPostal", compras.UtilizadorFK);
            return View(compras);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras
                .Include(c => c.Jogo)
                .Include(c => c.Utilizador)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compras == null)
            {
                return NotFound();
            }

            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compras = await _context.Compras.FindAsync(id);
            _context.Compras.Remove(compras);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComprasExists(int id)
        {
            return _context.Compras.Any(e => e.IdCompra == id);
        }
    }
}
