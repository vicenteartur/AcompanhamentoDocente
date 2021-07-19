﻿
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcompanhamentoDocente.Models;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace AcompanhamentoDocente.Controllers
{
    public class EscolaController : Controller
    {
        private readonly dbContext _context;
        private readonly object consulta;

        public EscolaController(dbContext context)
        {
            _context = context;
        }

        // GET: Escola
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.TbEscolas.Include(t => t.CodigoCidadeNavigation);
            return View(await dbContext.ToListAsync());
        }

        // GET: Escola/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEscola = await _context.TbEscolas
                .Include(t => t.CodigoCidadeNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbEscola == null)
            {
                return NotFound();
            }

            return View(tbEscola);
        }

        // GET: Escola/Create
        public IActionResult Create()
        {
            ViewData["CodigoEstado"] = new SelectList(_context.TbEstados, "Codigo", "Estado");
            ViewData["CodigoCidade"] = new SelectList(_context.TbCidades, "Codigo", "Cidade");
            return View();
        }


        [HttpGet]
        public System.String ListaCidade(int? id)
        {

            
            System.Collections.Generic.List<TbCidade> dbContext = _context.TbCidades
                            .Include(t => t.CodigoEstadoNavigation)
                            .Where(l => l.CodigoEstado == id).ToList();
            List<TbCidade> lista = new List<TbCidade>();    
            foreach (var item in dbContext)
            {
                lista.Add(new TbCidade() { Codigo = item.Codigo, Cidade = item.Cidade, CodigoEstado = item.CodigoEstado });
            }

            /*
            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }
            */
            //return Json(new { Resultado = dbContext } , System.Web.Mvc.JsonRequestBehavior.AllowGet);
            return "oioi";
        }


        // POST: Escola/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Escola,Rua,Bairro,CodigoCidade,Inep,Ativa")] TbEscola tbEscola)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbEscola);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodigoCidade"] = new SelectList(_context.TbCidades, "Codigo", "Cidade", tbEscola.CodigoCidade);
            return View(tbEscola);
        }

        // GET: Escola/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEscola = await _context.TbEscolas.FindAsync(id);
            if (tbEscola == null)
            {
                return NotFound();
            }
            ViewData["CodigoCidade"] = new SelectList(_context.TbCidades, "Codigo", "Cidade", tbEscola.CodigoCidade);
            return View(tbEscola);
        }

        // POST: Escola/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Escola,Rua,Bairro,CodigoCidade,Inep,Ativa")] TbEscola tbEscola)
        {
            if (id != tbEscola.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbEscola);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbEscolaExists(tbEscola.Codigo))
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
            ViewData["CodigoCidade"] = new SelectList(_context.TbCidades, "Codigo", "Cidade", tbEscola.CodigoCidade);
            return View(tbEscola);
        }

        // GET: Escola/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbEscola = await _context.TbEscolas
                .Include(t => t.CodigoCidadeNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (tbEscola == null)
            {
                return NotFound();
            }

            return View(tbEscola);
        }

        // POST: Escola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbEscola = await _context.TbEscolas.FindAsync(id);
            _context.TbEscolas.Remove(tbEscola);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbEscolaExists(int id)
        {
            return _context.TbEscolas.Any(e => e.Codigo == id);
        }
    }
}
