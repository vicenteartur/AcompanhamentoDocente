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
    public class ComponenteCurricularController : Controller
    {
        private readonly dbAcompanhamentoContext _context;

        public ComponenteCurricularController(dbAcompanhamentoContext context)
        {
            _context = context;
        }

        // GET: ComponenteCurricular
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbComponenteCurriculars.ToListAsync());
        }

        // GET: ComponenteCurricular/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbComponenteCurricular = await _context.TbComponenteCurriculars
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbComponenteCurricular == null)
            {
                return NotFound();
            }

            return View(tbComponenteCurricular);
        }

        // GET: ComponenteCurricular/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComponenteCurricular/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,ComponenteCurricular,SubArea,Ativa")] TbComponenteCurricular tbComponenteCurricular)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbComponenteCurricular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbComponenteCurricular);
        }

        // GET: ComponenteCurricular/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbComponenteCurricular = await _context.TbComponenteCurriculars.FindAsync(id);
            if (tbComponenteCurricular == null)
            {
                return NotFound();
            }
            return View(tbComponenteCurricular);
        }

        // POST: ComponenteCurricular/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,ComponenteCurricular,SubArea,Ativa")] TbComponenteCurricular tbComponenteCurricular)
        {
            if (id != tbComponenteCurricular.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbComponenteCurricular);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbComponenteCurricularExists(tbComponenteCurricular.Codigo))
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
            return View(tbComponenteCurricular);
        }

        // GET: ComponenteCurricular/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbComponenteCurricular = await _context.TbComponenteCurriculars
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbComponenteCurricular == null)
            {
                return NotFound();
            }

            return View(tbComponenteCurricular);
        }

        // POST: ComponenteCurricular/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbComponenteCurricular = await _context.TbComponenteCurriculars.FindAsync(id);
            _context.TbComponenteCurriculars.Remove(tbComponenteCurricular);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbComponenteCurricularExists(int id)
        {
            return _context.TbComponenteCurriculars.Any(e => e.Codigo == id);
        }
    }
}
