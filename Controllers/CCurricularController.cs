using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Services;

namespace AcompanhamentoDocente.Controllers
{
    public class CCurricularController : Controller
    {
        private readonly ICCurricular _componente;

        public CCurricularController()
        {
            
                _componente = new CCurricularService();
            
        }

        // GET: CCurricular
        public async Task<IActionResult> Index(int? id)
        {
            var admin = await _componente.MontarAdmin((int)id);

            ViewData["admin"] = admin;

            return View(await _componente.ListaComponente());
        }

        // GET: CCurricular/Details/5
        public async Task<IActionResult> Details(int? id, int? comp)
        {
            var admin = await _componente.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            if (id == null)
            {
                return NotFound();
            }

            var ccurricular = await _componente.Detalhes((int)comp);

            if (ccurricular == null)
            {
                return NotFound();
            }

            return View(ccurricular);
        }

        // GET: CCurricular/Create
        public async Task<IActionResult> Create(int? id)
        {
            var admin = await _componente.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View();
        }

        // POST: CCurricular/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Codigo,ComponenteCurricular,SubArea,Ativa")] TbComponenteCurricular tbComponenteCurricular)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbComponenteCurricular);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbComponenteCurricular);
        }

        // GET: CCurricular/Edit/5
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

        // POST: CCurricular/Edit/5
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

        // GET: CCurricular/Delete/5
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

        // POST: CCurricular/Delete/5
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
