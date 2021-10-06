using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class AvaliacaoViewController : Controller
    {

        private readonly IAvaliacaoViewModel _avaliacao;

        public AvaliacaoViewController()
        {
            _avaliacao = new AvaliacaoViewModelService();
        }

        // GET: AvaliacaoViewController
        public async Task<ActionResult> Index(int? id, int? esc)
        {
            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);

            return View(await _avaliacao.ListaAvaliacoes((int)id, (int)esc));
        }

        public async Task<ActionResult> AvFinalizadas(int? id, int? esc)
        {
            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);
            var lista = await _avaliacao.ListaAvaliacoesFinalizadas((int)esc);
            return View(lista);
        }

        // GET: AvaliacaoViewController/Details/5
        public async Task<ActionResult> Details(int? id, int? esc, int? aval, int? atrib)
        {
            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);

            return View(await _avaliacao.Detalhes((int)id, (int)atrib, (int)aval));
        }

        // GET: AvaliacaoViewController/Create
        public async Task<ActionResult> Create(int? id, int? esc, int? atrib)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (atrib == null)
            {
                return NotFound();
            }

            var avaliacao = new AvaliacaoViewModel
            {
                CodigoColaboradorAvaliador = (int)id,
                CodigoACECCA = (int)atrib
            };
            var aval = await _avaliacao.Inserir(avaliacao);


            return RedirectToAction("Edit", new { id = id, esc = esc, atrib = atrib, aval = aval.Codigo });
        }

        // GET: AvaliacaoViewController/Edit/5
        public async Task<ActionResult> Edit(int? id, int? esc, int? atrib, int? aval)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (esc == null)
            {
                return NotFound();
            }

            if (atrib == null)
            {
                return NotFound();
            }

            if (aval == null)
            {
                return NotFound();
            }

            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);
            ViewData["atrib"] = aval;

            return View(await _avaliacao.Detalhes((int)id, (int)atrib, (int)aval));
        }

        // POST: AvaliacaoViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, int? esc, int? atrib, int? aval, [Bind("Codigo,CodigoColaboradorAvaliador,CodigoACECCA,dataavaliacao")] AvaliacaoViewModel avaliacao)
        {
            var admin = await _avaliacao.MontarAdmin((int)id);

            if (ModelState.IsValid)
            {
                try
                {
                    var ataval = new TbAvaliacao
                    {
                        Codigo = avaliacao.Codigo,
                        Datarealizacao = avaliacao.dataavaliacao,
                        CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola = avaliacao.CodigoACECCA,
                        CodigoColaboradorAvaliador = avaliacao.CodigoColaboradorAvaliador,
                        Finalizada = 1
                    };

                    await _avaliacao.Atualizar(ataval);

                }
                catch (Exception)
                {
                    if (!_avaliacao.AvaliacaoExists(avaliacao.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = id, esc = esc });
            }
            return View(await _avaliacao.Detalhes((int)id, (int)atrib, (int)aval));
        }

        // GET: AvaliacaoViewController/Delete/5
        public async Task<ActionResult> Delete(int? id, int? esc, int? atrib, int? aval)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (esc == null)
            {
                return NotFound();
            }

            if (atrib == null)
            {
                return NotFound();
            }

            if (aval == null)
            {
                return NotFound();
            }

            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);
            ViewData["atrib"] = aval;

            return View(await _avaliacao.Detalhes((int)id, (int)atrib, (int)aval));
        }

        // POST: AvaliacaoViewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id, int? esc, int? Codigo)
        {

            if (id == null)
            {
                return NotFound();
            }

            if (esc == null)
            {
                return NotFound();
            }

            if (Codigo == null)
            {
                return NotFound();
            }

            try
            {
                await _avaliacao.Deletar((int)Codigo);
                return RedirectToAction("Index", new { id = id, esc = esc });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> AtualizaCriAv(int? id)
        {
            if (id != null)
            {
                TbCriterioAvaliado critavl = await _avaliacao.MontarCritAv((int)id);
                return PartialView(critavl);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void AtualizaCriAv([Bind("Codigo,CodigoAvaliacao,CodigoCriterioAvaliacao,Conceito,Comentario")] TbCriterioAvaliado critavaliado)
        {

            _avaliacao.AtualizaCritAv(critavaliado);

        }

        public async Task<ActionResult> Avaliar(int? id, int? esc)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (esc == null)
            {
                return NotFound();
            }


            return RedirectToAction("Index", "AtribCColEscView", new { id = id, esc = esc });

        }

        public async Task<ActionResult> ListaAbertaAvaliacaoAtribuicao(int? id, int? esc, int? atrib)
        {
            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);

            return View(await _avaliacao.ListaAvaliacoesAtribuicao((int)id, (int)esc,(int)atrib));
        }

    }
}
