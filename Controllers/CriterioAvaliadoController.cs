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
    public class CriterioAvaliadoController : Controller
    {
        private readonly dbContext _context;

        public CriterioAvaliadoController(dbContext context)
        {
            _context = context;
        }

        // GET: CriterioAvaliado
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.TbCriterioAvaliados.Include(t => t.CodigoAvaliacaoNavigation).Include(t => t.CodigoCriterioAvaliacaoNavigation);
            return View(await dbContext.ToListAsync());
        }

        // GET: CriterioAvaliado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCriterioAvaliado = await _context.TbCriterioAvaliados
                .Include(t => t.CodigoAvaliacaoNavigation)
                .Include(t => t.CodigoCriterioAvaliacaoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbCriterioAvaliado == null)
            {
                return NotFound();
            }

            return View(tbCriterioAvaliado);
        }

        // GET: CriterioAvaliado/Create
        public IActionResult Create()
        {
            ViewData["CodigoAvaliacao"] = new SelectList(_context.TbAvaliacaos, "Codigo", "Codigo");
            ViewData["CodigoCriterioAvaliacao"] = new SelectList(_context.TbCriterioAvaliacaos, "Codigo", "Criterio");
            return View();
        }

        // POST: CriterioAvaliado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,CodigoCriterioAvaliacao,Conceito,Comentario,CodigoAvaliacao")] TbCriterioAvaliado tbCriterioAvaliado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCriterioAvaliado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoAvaliacao"] = new SelectList(_context.TbAvaliacaos, "Codigo", "Codigo", tbCriterioAvaliado.CodigoAvaliacao);
            ViewData["CodigoCriterioAvaliacao"] = new SelectList(_context.TbCriterioAvaliacaos, "Codigo", "Criterio", tbCriterioAvaliado.CodigoCriterioAvaliacao);
            return View(tbCriterioAvaliado);
        }

        // GET: CriterioAvaliado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCriterioAvaliado = await _context.TbCriterioAvaliados.FindAsync(id);
            if (tbCriterioAvaliado == null)
            {
                return NotFound();
            }
            ViewData["CodigoAvaliacao"] = new SelectList(_context.TbAvaliacaos, "Codigo", "Codigo", tbCriterioAvaliado.CodigoAvaliacao);
            ViewData["CodigoCriterioAvaliacao"] = new SelectList(_context.TbCriterioAvaliacaos, "Codigo", "Criterio", tbCriterioAvaliado.CodigoCriterioAvaliacao);
            return View(tbCriterioAvaliado);
        }

        // POST: CriterioAvaliado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,CodigoCriterioAvaliacao,Conceito,Comentario,CodigoAvaliacao")] TbCriterioAvaliado tbCriterioAvaliado)
        {
            if (id != tbCriterioAvaliado.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCriterioAvaliado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCriterioAvaliadoExists(tbCriterioAvaliado.Codigo))
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
            ViewData["CodigoAvaliacao"] = new SelectList(_context.TbAvaliacaos, "Codigo", "Codigo", tbCriterioAvaliado.CodigoAvaliacao);
            ViewData["CodigoCriterioAvaliacao"] = new SelectList(_context.TbCriterioAvaliacaos, "Codigo", "Criterio", tbCriterioAvaliado.CodigoCriterioAvaliacao);
            return View(tbCriterioAvaliado);
        }

        // GET: CriterioAvaliado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCriterioAvaliado = await _context.TbCriterioAvaliados
                .Include(t => t.CodigoAvaliacaoNavigation)
                .Include(t => t.CodigoCriterioAvaliacaoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbCriterioAvaliado == null)
            {
                return NotFound();
            }

            return View(tbCriterioAvaliado);
        }

        // POST: CriterioAvaliado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbCriterioAvaliado = await _context.TbCriterioAvaliados.FindAsync(id);
            _context.TbCriterioAvaliados.Remove(tbCriterioAvaliado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbCriterioAvaliadoExists(int id)
        {
            return _context.TbCriterioAvaliados.Any(e => e.Codigo == id);
        }
    }
}
