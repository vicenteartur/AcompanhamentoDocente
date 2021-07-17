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
    public class EstadoController : Controller
    {
        private readonly dbContext _context;

        public EstadoController(dbContext context)
        {
            _context = context;
        }

        // GET: Estado
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbEstados.ToListAsync());
        }

        // GET: Estado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEstado = await _context.TbEstados
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbEstado == null)
            {
                return NotFound();
            }

            return View(tbEstado);
        }

        // GET: Estado/Create
        public IActionResult Create()
        {
            return View();
        }

        



        // POST: Estado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Estado,Sigla")] TbEstado tbEstado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbEstado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbEstado);
        }

        // GET: Estado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEstado = await _context.TbEstados.FindAsync(id);
            if (tbEstado == null)
            {
                return NotFound();
            }
            return View(tbEstado);
        }

        // POST: Estado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Estado,Sigla")] TbEstado tbEstado)
        {
            if (id != tbEstado.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbEstado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbEstadoExists(tbEstado.Codigo))
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
            return View(tbEstado);
        }

        // GET: Estado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEstado = await _context.TbEstados
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbEstado == null)
            {
                return NotFound();
            }

            return View(tbEstado);
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbEstado = await _context.TbEstados.FindAsync(id);
            _context.TbEstados.Remove(tbEstado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbEstadoExists(int id)
        {
            return _context.TbEstados.Any(e => e.Codigo == id);
        }
    }
}
