using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{


    public class EscolaViewController : Controller
    {
        private readonly IEscolaViewModel _escolaview;
        public EscolaViewController()
        {
            _escolaview = new EscolaViewModelService();
        }
        public async Task<IActionResult> Index(int? id)
        {
            var escola = new List<EscolaViewModel>();
            escola = await _escolaview.ListaEscolasAtivas((int)id);
            var colaborador = await _escolaview.MontarColaborador((int)id);
            ViewData["colaborador"] = colaborador;
            return View(escola);
        }

        public async Task<IActionResult> Inativas(int? id)
        {
            var escola = new List<EscolaViewModel>();
            escola = await _escolaview.ListaEscolasInativas((int)id);
            var colaborador = await _escolaview.MontarColaborador((int)id);
            ViewData["colaborador"] = colaborador;
            return View(escola);
        }

        // GET: Escola/Create
        public async Task<IActionResult> Create(int? id)
        {
            var escola = new EscolaViewModel();
            escola = await _escolaview.MontarEscola(0, (int)id);

            return View(escola);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Codigo,Escola,Rua,Bairro,CodigoCidade,Inep,Ativa")] TbEscola tbEscola)
        {


            if (ModelState.IsValid)
            {
                var inserir = new EscolaViewModel
                {
                    Codigo = tbEscola.Codigo,
                    Escola = tbEscola.Escola,
                    Rua = tbEscola.Rua,
                    Bairro = tbEscola.Bairro,
                    CodigoCidade = tbEscola.CodigoCidade,
                    INEP = tbEscola.Inep,
                    Ativa = 1,
                    CodigoColaborador = (int)id
                };
                await _escolaview.InserirEscola(inserir);

                return RedirectToAction("Index", new { id = id });
            }
            var escola = _escolaview.MontarEscola(0, (int)id);

            return View(escola);
        }


        public async Task<IActionResult> Edit(int? id, int? col)
        {
            if (id != null && col != null)
            {
                var escola = await _escolaview.MontarEscola((int)id, (int)col);
                return View(escola);
            }

            else
            {
                return NotFound();
            }

        }

        // POST: Escola/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Codigo,Escola,Rua,Bairro,CodigoCidade,INEP,Ativa,CodigoColaborador")] EscolaViewModel tbEscola)
        {
            if (id != tbEscola.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var atualizar = new EscolaViewModel
                    {
                        Codigo = tbEscola.Codigo,
                        Escola = tbEscola.Escola,
                        Rua = tbEscola.Rua,
                        Bairro = tbEscola.Bairro,
                        CodigoCidade = tbEscola.CodigoCidade,
                        INEP = tbEscola.INEP,
                        Ativa = 1,
                        CodigoColaborador = (int)tbEscola.CodigoColaborador
                    };
                    await _escolaview.AtualizarEscola(atualizar);

                    return RedirectToAction("Index", new { id = atualizar.CodigoColaborador });
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction("Index", new { id = tbEscola.CodigoColaborador });
            }
            var escola = await _escolaview.MontarEscola((int)id, tbEscola.CodigoColaborador);
            return View(escola);

        }

        public async Task<IActionResult> Details(int? id, int? col)
        {
            if (col == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var detalheescola = await _escolaview.MontarEscola((int)id, (int)col);
            if (detalheescola == null)
            {
                return NotFound();
            }

            return View(detalheescola);
        }


        public async Task<IActionResult> Delete(int? id, int? col)
        {
            if (col == null)
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var apagarescola = await _escolaview.MontarEscola((int)id, (int)col);
            if (apagarescola == null)
            {
                return NotFound();
            }

            return View(apagarescola);
        }

        // POST: Escola/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, int? CodigoColaborador)
        {
            var apagarescola = await _escolaview.MontarEscola((int)id, (int)CodigoColaborador);
            await _escolaview.ApagarEscola(apagarescola);

            return RedirectToAction("Index", new { id = CodigoColaborador });
        }

        public async Task<JsonResult> ListaCidade(int id)
        {
            var lista = new List<SelectListItem>();
            lista = await _escolaview.ListaCidades(id, 0);

            return new JsonResult(new { Resultado = lista });
        }

    }
}
