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
    public class AnoController : Controller
    {
        private readonly dbAcompanhamentoContext _context;

        public AnoController(dbAcompanhamentoContext context)
        {
            _context = context;
        }

        // GET: Ano
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbAnos.ToListAsync());
        }

        // GET: Ano/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAno = await _context.TbAnos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbAno == null)
            {
                return NotFound();
            }

            return View(tbAno);
        }

        // GET: Ano/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ano/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Ano,Turma,Modalidade,Periodo")] TbAno tbAno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbAno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbAno);
        }

        // GET: Ano/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAno = await _context.TbAnos.FindAsync(id);
            if (tbAno == null)
            {
                return NotFound();
            }
            return View(tbAno);
        }

        // POST: Ano/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Ano,Turma,Modalidade,Periodo")] TbAno tbAno)
        {
            if (id != tbAno.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbAno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbAnoExists(tbAno.Codigo))
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
            return View(tbAno);
        }

        // GET: Ano/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbAno = await _context.TbAnos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbAno == null)
            {
                return NotFound();
            }

            return View(tbAno);
        }

        // POST: Ano/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbAno = await _context.TbAnos.FindAsync(id);
            _context.TbAnos.Remove(tbAno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbAnoExists(int id)
        {
            return _context.TbAnos.Any(e => e.Codigo == id);
        }
    }
}
