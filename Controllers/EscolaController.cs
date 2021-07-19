﻿
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AcompanhamentoDocente.Models;
using Newtonsoft.Json;



namespace AcompanhamentoDocente.Controllers
{
    public class EscolaController : Controller
    {
        private readonly dbContext _context;

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
        public JsonResult ListaCidade()
        {
            //Cria uma lista de Clientes
            //List<TbCidade> oCliente = new List<TbCidade>()
            //{
            //Adicionar objetos na lista
            var cidade = new TbCidade { Codigo = 1, Cidade = "Lins", CodigoEstado = 1 };
                //new TbCidade {Codigo=1,Cidade="Lins",CodigoEstado=1 },
                //new TbCidade {Codigo=1,Cidade="Lins",CodigoEstado=1 }
            //};
            string json = JsonConvert.SerializeObject(cidade);
            //string rjson = cjson.Serialize(oCliente,Formatting.Indented);
            //retorna uma lista de objetos JSON
            return Json(json, System.Web.Mvc.JsonRequestBehavior.AllowGet);
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
