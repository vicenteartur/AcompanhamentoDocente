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
    public class AtrCCAnoColEscController : Controller
    {
        private readonly dbContext _context;

        public AtrCCAnoColEscController(dbContext context)
        {
            _context = context;
        }

        // GET: AtrCCAnoColEsc
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas.Include(t => t.CodigoAnoNavigation).Include(t => t.CodigoAtribuicaoColaboradorEscolaNavigation).Include(t => t.CodigoComponenteCurricularNavigation);
            return View(await dbContext.ToListAsync());
        }

        // GET: AtrCCAnoColEsc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAtribuicaoComponenteCurricularAnoColaboradorEscola = await _context.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                .Include(t => t.CodigoAnoNavigation)
                .Include(t => t.CodigoAtribuicaoColaboradorEscolaNavigation)
                .Include(t => t.CodigoComponenteCurricularNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbAtribuicaoComponenteCurricularAnoColaboradorEscola == null)
            {
                return NotFound();
            }

            return View(tbAtribuicaoComponenteCurricularAnoColaboradorEscola);
        }

        // GET: AtrCCAnoColEsc/Create
        public IActionResult Create()
        {
            ViewData["CodigoAno"] = new SelectList(_context.TbAnos, "Codigo", "Ano");
            ViewData["CodigoAtribuicaoColaboradorEscola"] = new SelectList(_context.TbAtribuicaoColaboradorEscolas, "Codigo", "Codigo");
            ViewData["CodigoComponenteCurricular"] = new SelectList(_context.TbComponenteCurriculars, "Codigo", "ComponenteCurricular");
            return View();
        }

        // POST: AtrCCAnoColEsc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,CodigoAtribuicaoColaboradorEscola,CodigoComponenteCurricular,CodigoAno,Ativa")] TbAtribuicaoComponenteCurricularAnoColaboradorEscola tbAtribuicaoComponenteCurricularAnoColaboradorEscola)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbAtribuicaoComponenteCurricularAnoColaboradorEscola);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoAno"] = new SelectList(_context.TbAnos, "Codigo", "Ano", tbAtribuicaoComponenteCurricularAnoColaboradorEscola.CodigoAno);
            ViewData["CodigoAtribuicaoColaboradorEscola"] = new SelectList(_context.TbAtribuicaoColaboradorEscolas, "Codigo", "Codigo", tbAtribuicaoComponenteCurricularAnoColaboradorEscola.CodigoAtribuicaoColaboradorEscola);
            ViewData["CodigoComponenteCurricular"] = new SelectList(_context.TbComponenteCurriculars, "Codigo", "ComponenteCurricular", tbAtribuicaoComponenteCurricularAnoColaboradorEscola.CodigoComponenteCurricular);
            return View(tbAtribuicaoComponenteCurricularAnoColaboradorEscola);
        }

        // GET: AtrCCAnoColEsc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAtribuicaoComponenteCurricularAnoColaboradorEscola = await _context.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas.FindAsync(id);
            if (tbAtribuicaoComponenteCurricularAnoColaboradorEscola == null)
            {
                return NotFound();
            }
            ViewData["CodigoAno"] = new SelectList(_context.TbAnos, "Codigo", "Ano", tbAtribuicaoComponenteCurricularAnoColaboradorEscola.CodigoAno);
            ViewData["CodigoAtribuicaoColaboradorEscola"] = new SelectList(_context.TbAtribuicaoColaboradorEscolas, "Codigo", "Codigo", tbAtribuicaoComponenteCurricularAnoColaboradorEscola.CodigoAtribuicaoColaboradorEscola);
            ViewData["CodigoComponenteCurricular"] = new SelectList(_context.TbComponenteCurriculars, "Codigo", "ComponenteCurricular", tbAtribuicaoComponenteCurricularAnoColaboradorEscola.CodigoComponenteCurricular);
            return View(tbAtribuicaoComponenteCurricularAnoColaboradorEscola);
        }

        // POST: AtrCCAnoColEsc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,CodigoAtribuicaoColaboradorEscola,CodigoComponenteCurricular,CodigoAno,Ativa")] TbAtribuicaoComponenteCurricularAnoColaboradorEscola tbAtribuicaoComponenteCurricularAnoColaboradorEscola)
        {
            if (id != tbAtribuicaoComponenteCurricularAnoColaboradorEscola.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbAtribuicaoComponenteCurricularAnoColaboradorEscola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbAtribuicaoComponenteCurricularAnoColaboradorEscolaExists(tbAtribuicaoComponenteCurricularAnoColaboradorEscola.Codigo))
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
            ViewData["CodigoAno"] = new SelectList(_context.TbAnos, "Codigo", "Ano", tbAtribuicaoComponenteCurricularAnoColaboradorEscola.CodigoAno);
            ViewData["CodigoAtribuicaoColaboradorEscola"] = new SelectList(_context.TbAtribuicaoColaboradorEscolas, "Codigo", "Codigo", tbAtribuicaoComponenteCurricularAnoColaboradorEscola.CodigoAtribuicaoColaboradorEscola);
            ViewData["CodigoComponenteCurricular"] = new SelectList(_context.TbComponenteCurriculars, "Codigo", "ComponenteCurricular", tbAtribuicaoComponenteCurricularAnoColaboradorEscola.CodigoComponenteCurricular);
            return View(tbAtribuicaoComponenteCurricularAnoColaboradorEscola);
        }

        // GET: AtrCCAnoColEsc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAtribuicaoComponenteCurricularAnoColaboradorEscola = await _context.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                .Include(t => t.CodigoAnoNavigation)
                .Include(t => t.CodigoAtribuicaoColaboradorEscolaNavigation)
                .Include(t => t.CodigoComponenteCurricularNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbAtribuicaoComponenteCurricularAnoColaboradorEscola == null)
            {
                return NotFound();
            }

            return View(tbAtribuicaoComponenteCurricularAnoColaboradorEscola);
        }

        // POST: AtrCCAnoColEsc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbAtribuicaoComponenteCurricularAnoColaboradorEscola = await _context.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas.FindAsync(id);
            _context.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas.Remove(tbAtribuicaoComponenteCurricularAnoColaboradorEscola);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbAtribuicaoComponenteCurricularAnoColaboradorEscolaExists(int id)
        {
            return _context.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas.Any(e => e.Codigo == id);
        }
    }
}
