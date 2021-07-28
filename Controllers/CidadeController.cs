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
    public class CidadeController : Controller
    {
        private readonly dbAcompanhamentoContext _context;

        public CidadeController(dbAcompanhamentoContext context)
        {
            _context = context;
        }

        // GET: Cidade
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.TbCidades.Include(t => t.CodigoEstadoNavigation);
            return View(await dbContext.ToListAsync());
        }

        public async Task<IActionResult> ListaCidade(int? id)
        {
           

            var dbContext = _context.TbCidades
                .Include(t => t.CodigoEstadoNavigation)
                .Where(l => l.CodigoEstado == id);
                           //
                           //from q in _context.TbCidades
                           //orderby q.Cidade
                           //
                           //where q.CodigoEstado == _codigoestado
                           //select q;

            
                return View(await dbContext.ToListAsync());
            }
            
    


// GET: Cidade/Details/5
public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCidade = await _context.TbCidades
                .Include(t => t.CodigoEstadoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbCidade == null)
            {
                return NotFound();
            }

            return View(tbCidade);
        }

       
        // GET: Cidade/Create
        public IActionResult Create()
        {
            ViewData["CodigoEstado"] = new SelectList(_context.TbEstados, "Codigo", "Estado");
            return View();
        }

        // POST: Cidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Cidade,CodigoEstado")] TbCidade tbCidade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbCidade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoEstado"] = new SelectList(_context.TbEstados, "Codigo", "Estado", tbCidade.CodigoEstado);
            return View(tbCidade);
        }

        // GET: Cidade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCidade = await _context.TbCidades.FindAsync(id);
            if (tbCidade == null)
            {
                return NotFound();
            }
            ViewData["CodigoEstado"] = new SelectList(_context.TbEstados, "Codigo", "Estado", tbCidade.CodigoEstado);
            return View(tbCidade);
        }

        // POST: Cidade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Cidade,CodigoEstado")] TbCidade tbCidade)
        {
            if (id != tbCidade.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbCidade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbCidadeExists(tbCidade.Codigo))
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
            ViewData["CodigoEstado"] = new SelectList(_context.TbEstados, "Codigo", "Estado", tbCidade.CodigoEstado);
            return View(tbCidade);
        }

        // GET: Cidade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCidade = await _context.TbCidades
                .Include(t => t.CodigoEstadoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbCidade == null)
            {
                return NotFound();
            }

            return View(tbCidade);
        }

        // POST: Cidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbCidade = await _context.TbCidades.FindAsync(id);
            _context.TbCidades.Remove(tbCidade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbCidadeExists(int id)
        {
            return _context.TbCidades.Any(e => e.Codigo == id);
        }
    }
}
