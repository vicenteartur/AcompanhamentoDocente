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
    public class CargoController : Controller
    {
        private readonly dbAcompanhamentoContext _context;

        public CargoController(dbAcompanhamentoContext context)
        {
            _context = context;
        }

        // GET: Cargo
        public async Task<IActionResult> Index()
        {
            return View(await _context.TbCargos.ToListAsync());
        }

        // GET: Cargo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCargo = await _context.TbCargos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbCargo == null)
            {
                return NotFound();
            }

            return View(tbCargo);
        }

        // GET: Cargo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cargo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Cargo,NiveldeAcesso")] TbCargo tbCargo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCargo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tbCargo);
        }

        // GET: Cargo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCargo = await _context.TbCargos.FindAsync(id);
            if (tbCargo == null)
            {
                return NotFound();
            }
            return View(tbCargo);
        }

        // POST: Cargo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Cargo,NiveldeAcesso")] TbCargo tbCargo)
        {
            if (id != tbCargo.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCargo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCargoExists(tbCargo.Codigo))
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
            return View(tbCargo);
        }

        // GET: Cargo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCargo = await _context.TbCargos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbCargo == null)
            {
                return NotFound();
            }

            return View(tbCargo);
        }

        // POST: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbCargo = await _context.TbCargos.FindAsync(id);
            _context.TbCargos.Remove(tbCargo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbCargoExists(int id)
        {
            return _context.TbCargos.Any(e => e.Codigo == id);
        }
    }
}
