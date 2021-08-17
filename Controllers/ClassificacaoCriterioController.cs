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
    public class ClassificacaoCriterioController : Controller
    {
        private readonly IClassificacaoCriterio _classificacao;

        public ClassificacaoCriterioController()
        {
            _classificacao = new ClassificacaoCriterioService();
        }

        // GET: ClassificacaoCriterio
        public async Task<IActionResult> Index(int? id)
        {
            var admin = await _classificacao.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View(await _classificacao.ListaClassificacao());
        }

        // GET: ClassificacaoCriterio/Details/5
        public async Task<IActionResult> Details(int? id, int? classificacao)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _classificacao.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            var tbClassificacaoCriterio = await _classificacao.Detalhes((int)classificacao);
            if (tbClassificacaoCriterio == null)
            {
                return NotFound();
            }

            return View(tbClassificacaoCriterio);
        }

        // GET: ClassificacaoCriterio/Create
        public async Task<IActionResult> Create(int? id)
        {
            var admin = await _classificacao.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View();
        }

        // POST: ClassificacaoCriterio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Codigo,Classificacao")] TbClassificacaoCriterio tbClassificacaoCriterio)
        {
            var admin = await _classificacao.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            if (ModelState.IsValid)
            {
                await _classificacao.Inserir(tbClassificacaoCriterio);

                return RedirectToAction("Index", new { id = id });
            }
            return View(tbClassificacaoCriterio);
        }

        // GET: ClassificacaoCriterio/Edit/5
        public async Task<IActionResult> Edit(int? id, int? classificacao)
        {
            var admin = await _classificacao.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            var tbClassificacaoCriterio = await _classificacao.Detalhes((int)classificacao);
            if (tbClassificacaoCriterio == null)
            {
                return NotFound();
            }
            return View(tbClassificacaoCriterio);
        }

        // POST: ClassificacaoCriterio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Codigo,Classificacao")] TbClassificacaoCriterio tbClassificacaoCriterio)
        {
            var admin = await _classificacao.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _classificacao.Atualizar(tbClassificacaoCriterio);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_classificacao.TbClassificacaoExists(tbClassificacaoCriterio.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = id });
            }
            return View(tbClassificacaoCriterio);
        }

        // GET: ClassificacaoCriterio/Delete/5
        public async Task<IActionResult> Delete(int? id, int? classificacao)
        {
            var admin = await _classificacao.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            var tbClassificacaoCriterio = await _classificacao.Detalhes((int)classificacao);
            
            if (tbClassificacaoCriterio == null)
            {
                return NotFound();
            }

            return View(tbClassificacaoCriterio);
        }

        // POST: ClassificacaoCriterio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int codigo)
        {
            var tbClassificacoCriterio = await _classificacao.Detalhes((int)codigo);
            await _classificacao.Deletar(tbClassificacoCriterio);

            return RedirectToAction("Index", new { id = id });
        }
    }
}
