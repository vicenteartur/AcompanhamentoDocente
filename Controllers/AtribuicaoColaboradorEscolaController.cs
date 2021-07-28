using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcompanhamentoDocente.Models;

namespace AcompanhamentoDocente.Controllers
{
    public class AtribuicaoColaboradorEscolaController : Controller
    {
        private readonly dbAcompanhamentoContext _context;

        public AtribuicaoColaboradorEscolaController(dbAcompanhamentoContext context)
        {
            _context = context;
        }

        // GET: AtribuicaoColaboradorEscola
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.TbAtribuicaoColaboradorEscolas.Include(t => t.CodigoColaboradorNavigation).Include(t => t.CodigoEscolaNavigation);
            return View(await dbContext.ToListAsync());
        }

        // GET: AtribuicaoColaboradorEscola/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAtribuicaoColaboradorEscola = await _context.TbAtribuicaoColaboradorEscolas
                .Include(t => t.CodigoColaboradorNavigation)
                .Include(t => t.CodigoEscolaNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbAtribuicaoColaboradorEscola == null)
            {
                return NotFound();
            }

            return View(tbAtribuicaoColaboradorEscola);
        }

        // GET: AtribuicaoColaboradorEscola/Create
        public IActionResult Create()
        {
            ViewData["CodigoColaborador"] = new SelectList(_context.TbColaboradors, "Codigo", "Email");
            ViewData["CodigoEscola"] = new SelectList(_context.TbEscolas, "Codigo", "Bairro");
            return View();
        }

        // POST: AtribuicaoColaboradorEscola/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,CodigoEscola,CodigoColaborador,Ativa")] TbAtribuicaoColaboradorEscola tbAtribuicaoColaboradorEscola)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbAtribuicaoColaboradorEscola);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoColaborador"] = new SelectList(_context.TbColaboradors, "Codigo", "Email", tbAtribuicaoColaboradorEscola.CodigoColaborador);
            ViewData["CodigoEscola"] = new SelectList(_context.TbEscolas, "Codigo", "Bairro", tbAtribuicaoColaboradorEscola.CodigoEscola);
            return View(tbAtribuicaoColaboradorEscola);
        }

        // GET: AtribuicaoColaboradorEscola/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAtribuicaoColaboradorEscola = await _context.TbAtribuicaoColaboradorEscolas.FindAsync(id);
            if (tbAtribuicaoColaboradorEscola == null)
            {
                return NotFound();
            }
            ViewData["CodigoColaborador"] = new SelectList(_context.TbColaboradors, "Codigo", "Email", tbAtribuicaoColaboradorEscola.CodigoColaborador);
            ViewData["CodigoEscola"] = new SelectList(_context.TbEscolas, "Codigo", "Bairro", tbAtribuicaoColaboradorEscola.CodigoEscola);
            return View(tbAtribuicaoColaboradorEscola);
        }

        // POST: AtribuicaoColaboradorEscola/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,CodigoEscola,CodigoColaborador,Ativa")] TbAtribuicaoColaboradorEscola tbAtribuicaoColaboradorEscola)
        {
            if (id != tbAtribuicaoColaboradorEscola.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbAtribuicaoColaboradorEscola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbAtribuicaoColaboradorEscolaExists(tbAtribuicaoColaboradorEscola.Codigo))
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
            ViewData["CodigoColaborador"] = new SelectList(_context.TbColaboradors, "Codigo", "Email", tbAtribuicaoColaboradorEscola.CodigoColaborador);
            ViewData["CodigoEscola"] = new SelectList(_context.TbEscolas, "Codigo", "Bairro", tbAtribuicaoColaboradorEscola.CodigoEscola);
            return View(tbAtribuicaoColaboradorEscola);
        }

        // GET: AtribuicaoColaboradorEscola/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAtribuicaoColaboradorEscola = await _context.TbAtribuicaoColaboradorEscolas
                .Include(t => t.CodigoColaboradorNavigation)
                .Include(t => t.CodigoEscolaNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbAtribuicaoColaboradorEscola == null)
            {
                return NotFound();
            }

            return View(tbAtribuicaoColaboradorEscola);
        }

        // POST: AtribuicaoColaboradorEscola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbAtribuicaoColaboradorEscola = await _context.TbAtribuicaoColaboradorEscolas.FindAsync(id);
            _context.TbAtribuicaoColaboradorEscolas.Remove(tbAtribuicaoColaboradorEscola);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbAtribuicaoColaboradorEscolaExists(int id)
        {
            return _context.TbAtribuicaoColaboradorEscolas.Any(e => e.Codigo == id);
        }
    }
}
