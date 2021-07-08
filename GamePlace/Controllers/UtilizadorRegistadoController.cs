using GamePlace.Data;
using GamePlace.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GamePlace.Controllers
{
    public class UtilizadorRegistadoController : Controller
    {
        private readonly GamePlaceDb _context;

        public UtilizadorRegistadoController(GamePlaceDb context)
        {
            _context = context;
        }

        // GET: UtilizadorRegistado
        public async Task<IActionResult> Index()
        {
            return View(await _context.UtilizadorRegistado.ToListAsync());
        }

        // GET: UtilizadorRegistado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizadorRegistado = await _context.UtilizadorRegistado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizadorRegistado == null)
            {
                return NotFound();
            }

            return View(utilizadorRegistado);
        }

        // GET: UtilizadorRegistado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UtilizadorRegistado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,FotoUtilizador,Morada,CodPostal,Telemovel,Email,UserNameId")] UtilizadorRegistado utilizadorRegistado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(utilizadorRegistado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(utilizadorRegistado);
        }

        // GET: UtilizadorRegistado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizadorRegistado = await _context.UtilizadorRegistado.FindAsync(id);
            if (utilizadorRegistado == null)
            {
                return NotFound();
            }
            return View(utilizadorRegistado);
        }

        // POST: UtilizadorRegistado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,FotoUtilizador,Morada,CodPostal,Telemovel,Email,UserNameId")] UtilizadorRegistado utilizadorRegistado)
        {
            if (id != utilizadorRegistado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(utilizadorRegistado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UtilizadorRegistadoExists(utilizadorRegistado.Id))
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
            return View(utilizadorRegistado);
        }

        // GET: UtilizadorRegistado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizadorRegistado = await _context.UtilizadorRegistado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizadorRegistado == null)
            {
                return NotFound();
            }

            return View(utilizadorRegistado);
        }

        // POST: UtilizadorRegistado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var utilizadorRegistado = await _context.UtilizadorRegistado.FindAsync(id);
            _context.UtilizadorRegistado.Remove(utilizadorRegistado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UtilizadorRegistadoExists(int id)
        {
            return _context.UtilizadorRegistado.Any(e => e.Id == id);
        }
    }
}
