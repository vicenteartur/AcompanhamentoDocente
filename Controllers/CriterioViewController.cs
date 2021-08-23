using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class CriterioViewController : Controller
    {

        private readonly ICriterioViewModel _criterio;

        public CriterioViewController()
        {
            _criterio = new CriterioViewService();
        }


        public async Task<ActionResult> Index(int? id)
        {
            var admin = await _criterio.MontarAdmin((int)id);
            List<TbComponenteCurricular> compcur = await _criterio.ListaCompCur();
            ViewData["admin"] = admin;
            return View(compcur);
        }

        // GET: CriterioView
        public async Task<ActionResult> ListaCriterio(int? id, int? ccur)
        {
            var admin = await _criterio.MontarAdmin((int)id);
            List<CriterioViewModel> criterio = await _criterio.ListaCriterios((int)ccur);
            ViewData["admin"] = admin;
            return View(criterio);
        }

        // GET: CriterioView/Details/5
        public async Task<ActionResult> Details(int? id, int? criterio)
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

        // GET: CriterioView/Create
        public async Task<ActionResult> Create(int? id)
        {
            var admin = await _criterio.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            ViewData["CodigoClassificacaoCriterio"] = _criterio.Classificacao();
            ViewData["CodigoCCurricular"] = _criterio.CompCurri();


            return View();
        }

        // POST: CriterioView/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id,int[] CCurricular, [Bind("Codigo,Criterio,CodigoClassificacaoCriterio,Ativa")] CriterioViewModel criteriov)
        {
            
            if (ModelState.IsValid)
            {
                var lista = new List<CriterioViewModel>();
                foreach (var item in CCurricular)
                {
                    var litem = new CriterioViewModel()
                    {
                       Criterio =  criteriov.Criterio,
                       CodigoCCUrricular = item,
                       CodigoClassificacaoCriterio = criteriov.CodigoClassificacaoCriterio,
                       Ativa = 1
                    };
                    lista.Add(litem);
                }
                await _criterio.Inserir(lista);
                return RedirectToAction("Index", new { id = id });
            }

            var admin = await _criterio.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            ViewData["CodigoClassificacaoCriterio"] = _criterio.Classificacao();
            ViewData["CodigoCCurricular"] = _criterio.CompCurri();
            return View(criteriov);
        }
    

        // GET: CriterioView/Edit/5
        public async Task<ActionResult> Edit(int? id, int? criterio)
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
            var comp = _criterio.UpCompCurri(tbCriterioAvaliacao.Codigo);
            ViewData["admin"] = admin;
            ViewData["CodigoClassificacaoCriterio"] = _criterio.ClassificacaoUp(tbCriterioAvaliacao.Codigo);
            ViewData["CodigoCCurricular"] = comp;

            return View(tbCriterioAvaliacao);
        }

        // POST: CriterioView/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, int[] CCurricular, [Bind("Codigo,Criterio,CodigoClassificacaoCriterio,Ativa")] CriterioViewModel criteriov)
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
                    var lista = new List<CriterioViewModel>();
                    foreach (var item in CCurricular)
                    {
                        var litem = new CriterioViewModel()
                        {
                            Criterio = criteriov.Criterio,
                            CodigoCCUrricular = item,
                            CodigoClassificacaoCriterio = criteriov.CodigoClassificacaoCriterio,
                            Ativa = 1
                        };
                        lista.Add(litem);
                    }
                    await _criterio.Inserir(lista);
                    return RedirectToAction("Index", new { id = id });

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

        // GET: CriterioView/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CriterioView/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
