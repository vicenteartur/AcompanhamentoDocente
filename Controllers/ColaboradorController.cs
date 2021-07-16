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
    public class ColaboradorController : Controller
    {
        private readonly dbContext _context;

        public ColaboradorController(dbContext context)
        {
            _context = context;
        }

        // GET: Colaborador
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.TbColaboradors.Include(t => t.CodigoCargoNavigation);
            return View(await dbContext.ToListAsync());
        }

        // GET: Colaborador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbColaborador = await _context.TbColaboradors
                .Include(t => t.CodigoCargoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbColaborador == null)
            {
                return NotFound();
            }

            return View(tbColaborador);
        }

        // GET: Colaborador/Create
        public IActionResult Create()
        {
            ViewData["CodigoCargo"] = new SelectList(_context.TbCargos, "Codigo", "Cargo");
            return View();
        }

        // POST: Colaborador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Nome,Email,Senha,CodigoCargo,Ativo")] TbColaborador tbColaborador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbColaborador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCargo"] = new SelectList(_context.TbCargos, "Codigo", "Cargo", tbColaborador.CodigoCargo);
            return View(tbColaborador);
        }

        // GET: Colaborador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbColaborador = await _context.TbColaboradors.FindAsync(id);
            if (tbColaborador == null)
            {
                return NotFound();
            }
            ViewData["CodigoCargo"] = new SelectList(_context.TbCargos, "Codigo", "Cargo", tbColaborador.CodigoCargo);
            return View(tbColaborador);
        }

        // POST: Colaborador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Nome,Email,Senha,CodigoCargo,Ativo")] TbColaborador tbColaborador)
        {
            if (id != tbColaborador.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbColaborador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbColaboradorExists(tbColaborador.Codigo))
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
            ViewData["CodigoCargo"] = new SelectList(_context.TbCargos, "Codigo", "Cargo", tbColaborador.CodigoCargo);
            return View(tbColaborador);
        }

        // GET: Colaborador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbColaborador = await _context.TbColaboradors
                .Include(t => t.CodigoCargoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbColaborador == null)
            {
                return NotFound();
            }

            return View(tbColaborador);
        }

        // POST: Colaborador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbColaborador = await _context.TbColaboradors.FindAsync(id);
            _context.TbColaboradors.Remove(tbColaborador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbColaboradorExists(int id)
        {
            return _context.TbColaboradors.Any(e => e.Codigo == id);
        }
    }
}
