using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AcompanhamentoDocente.Controllers
{
    public class CriterioAvaliacaoController : Controller
    {
        private readonly ICriterioAvaliacao _criterio;

        public CriterioAvaliacaoController()
        {
            _criterio = new CriterioAvaliacaoService();
        }

        // GET: CriterioAvaliacao
        public async Task<IActionResult> Index(int? id)
        {
            var admin = await _criterio.MontarAdmin((int)id);

            ViewData["admin"] = admin;

            return View(await _criterio.ListaCriterios());
        }

        // GET: CriterioAvaliacao/Details/5
        public async Task<IActionResult> Details(int? id, int?criterio)
        {
            var admin = await _criterio.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            if (id == null)
            {
                return NotFound();
            }

            var tbcriterio = await _criterio.Detalhes((int)criterio);

            if (tbcriterio == null)
            {
                return NotFound();
            }

            return View(tbcriterio);
        }

        // GET: CriterioAvaliacao/Create
        public async Task<IActionResult> Create(int? id)
        {
            var admin = await _criterio.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            ViewData["CodigoClassificacaoCriterio"] = _criterio.Classificacao();
            return View();
        }

        // POST: CriterioAvaliacao/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id,[Bind("Codigo,Criterio,CodigoClassificacaoCriterio,Ativa")] TbCriterioAvaliacao tbCriterioAvaliacao)
        {
            var admin = await _criterio.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (ModelState.IsValid)
            {
                await _criterio.Inserir(tbCriterioAvaliacao);
                return RedirectToAction("Index", new { id = id });
            }

            ViewData["CodigoClassificacaoCriterio"] = _criterio.ClassificacaoUp(tbCriterioAvaliacao.CodigoClassificacaoCriterio);
            return View(tbCriterioAvaliacao);
        }

        // GET: CriterioAvaliacao/Edit/5
        public async Task<IActionResult> Edit(int? id, int? criterio)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCriterioAvaliacao = await _criterio.Detalhes((int)criterio);

            if (tbCriterioAvaliacao == null)
            {
                return NotFound();
            }

            var admin = await _criterio.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            ViewData["CodigoClassificacaoCriterio"] = _criterio.ClassificacaoUp(tbCriterioAvaliacao.CodigoClassificacaoCriterio);
            
            return View(tbCriterioAvaliacao);
        }

        // POST: CriterioAvaliacao/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Codigo,Criterio,CodigoClassificacaoCriterio,Ativa")] TbCriterioAvaliacao tbCriterioAvaliacao)
        {
            var admin = await _criterio.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _criterio.Atualizar(tbCriterioAvaliacao);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_criterio.TbCriterioExists(tbCriterioAvaliacao.Codigo))
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
            ViewData["CodigoClassificacaoCriterio"] = _criterio.ClassificacaoUp(tbCriterioAvaliacao.CodigoClassificacaoCriterio);
            return View(tbCriterioAvaliacao);
        }

        // GET: CriterioAvaliacao/Delete/5
        public async Task<IActionResult> Delete(int? id, int? criterio)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbCriterioAvaliacao = await _criterio.Detalhes((int)criterio);

            if (tbCriterioAvaliacao == null)
            {
                return NotFound();
            }

            return View(tbCriterioAvaliacao);
        }

        // POST: CriterioAvaliacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, int? Codigo)
        {
            var tbCriterioAvaliacao = await _criterio.Detalhes((int)Codigo);
            await _criterio.Deletar(tbCriterioAvaliacao);

            return RedirectToAction("Index", new { id = id });
        }
    }
}
