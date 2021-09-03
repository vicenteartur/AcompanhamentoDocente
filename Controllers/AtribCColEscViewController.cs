using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class AtribCColEscViewController : Controller
    {
        private readonly IAtribCCColEscViewModel _atribuicao;

        public AtribCColEscViewController()
        {
            _atribuicao = new AtribcCColEscViewModelService();
        }
        // GET: AtribCColEscViewModelController
        public async Task<ActionResult> Index(int? id, int? esc)
        {
            var escola = await _atribuicao.MontarEsc((int)esc);
            var admin = await _atribuicao.MontarAdmin((int)id);
            var lista = await _atribuicao.ListaAtribuicao((int)id, (int)esc);
            ViewData["escol"] = escola;
            ViewData["admin"] = admin;

            return View(lista);

        }

        // GET: AtribCColEscViewModelController
        public async Task<ActionResult> ListaProfessor(int? id, int? esc)
        {
            var escola = new TbEscola();
            escola = await _atribuicao.MontarEsc((int)esc);
            var admin = new TbColaborador();
            admin = await _atribuicao.MontarAdmin((int)id);
            var lista = await _atribuicao.ListaProfessores((int)esc);
            ViewData["escol"] = escola;
            ViewData["admin"] = admin;

            return View(lista);

        }

        // GET: AtribCColEscViewModelController/Details/5
        public async Task<ActionResult> Details(int? id, int? esc, int? atribu)
        {
            var escola = await _atribuicao.MontarEsc((int)esc);
            var admin = await _atribuicao.MontarAdmin((int)id);
            var detalhes = await _atribuicao.Detalhes((int)atribu);
            detalhes.NomeAdministrador = admin.Nome;
            detalhes.CargoAdministrador = admin.CodigoCargoNavigation.Cargo;
            detalhes.CodigoAdministrador = admin.Codigo;
            ViewData["escol"] = escola;
            ViewData["admin"] = admin;
            return View(detalhes);
        }

        // GET: AtribCColEscViewModelController/Create
        public async Task<ActionResult> Create(int? id, int? esc, int? col)
        {
            var escola = await _atribuicao.MontarEsc((int)esc);
            var admin = await _atribuicao.MontarAdmin((int)id);
            var colab = await _atribuicao.MontarAdmin((int)col);
            var atribuicao = await _atribuicao.BuscaAtrib((int)col, (int)esc);
            var atrib = new AtribCCColEscViewModel()
            {
                CodigoColaborador = colab.Codigo,
                Nome = colab.Nome,
                Cargo = colab.CodigoCargoNavigation.Cargo,
                CodigoAtribuicaoColaboradorEscola = atribuicao.Codigo,
                ano = new List<SelectListItem>(),
                CCurricular = new List<SelectListItem>()

            };


            ViewData["CodigoModalidade"] = _atribuicao.ListaModalidade();
            ViewData["escol"] = escola;
            ViewData["admin"] = admin;

            return View(atrib);
        }

        // POST: AtribCColEscViewModelController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int? id, int? esc, [Bind("Codigo,CodigoAtribuicaoColaboradorEscola,CodigoModalidade,CodigoCC,CodigoAno")] AtribCCColEscViewModel atribu)
        {
            if (ModelState.IsValid)
            {
                await _atribuicao.Inserir(atribu);

                return RedirectToAction("Index", new { id = id, esc = esc });
            }
            else
            {
                var escola = await _atribuicao.MontarEsc((int)esc);
                var admin = await _atribuicao.MontarAdmin((int)id);
                var atrib = new AtribCCColEscViewModel();


                ViewData["CodigoModalidade"] = _atribuicao.ListaModalidade();
                ViewData["escol"] = escola;
                ViewData["admin"] = admin;
                return View(atrib);
            }
        }


        // GET: AtribCColEscViewModelController/Edit/5
        public async Task<ActionResult> Edit(int? id, int? esc, int? atribu)
        {
            var escola = await _atribuicao.MontarEsc((int)esc);
            var admin = await _atribuicao.MontarAdmin((int)id);
            var atrib = await _atribuicao.Detalhes((int)atribu);
            var ano = await _atribuicao.ListaAno(atrib.CodigoAno, atrib.CodigoModalidade);
            atrib.ano = ano;
            var cc = await _atribuicao.ListaCCurricular(atrib.CodigoCC, atrib.CodigoModalidade);
            atrib.CCurricular = cc;
            ViewData["escol"] = escola;
            ViewData["admin"] = admin;

            return View(atrib);
        }

        // POST: AtribCColEscViewModelController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, int? esc, [Bind("Codigo,CodigoAtribuicaoColaboradorEscola,CodigoModalidade,CodigoCC,CodigoAno")] AtribCCColEscViewModel atribu)
        {


            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _atribuicao.Atualizar(atribu);

                }
                catch (Exception)
                {
                    if (!_atribuicao.TbAtribExists(atribu.Codigo))
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
            var escola = await _atribuicao.MontarEsc((int)esc);
            var admin = await _atribuicao.MontarAdmin((int)id);
            var atrib = await _atribuicao.Detalhes((int)atribu.Codigo);
            var ano = await _atribuicao.ListaAno(atrib.CodigoAno, atrib.CodigoModalidade);
            atrib.ano = ano;
            var cc = await _atribuicao.ListaCCurricular(atrib.CodigoCC, atrib.CodigoModalidade);
            atrib.CCurricular = cc;
            ViewData["escol"] = escola;
            ViewData["admin"] = admin;
            return View(atrib);
        }


        // GET: AtribCColEscViewModelController/Delete/5
        public async Task<ActionResult> Delete(int? id, int? esc, int? atribu)
        {
            var escola = await _atribuicao.MontarEsc((int)esc);
            var admin = await _atribuicao.MontarAdmin((int)id);
            var detalhes = await _atribuicao.Detalhes((int)atribu);
            detalhes.CodigoAdministrador = admin.Codigo;
            detalhes.NomeAdministrador = admin.Nome;
            detalhes.CargoAdministrador = admin.CodigoCargoNavigation.Cargo;
            detalhes.CodigoAdministrador = admin.Codigo;


            return View(detalhes);
        }

        // POST: AtribCColEscViewModelController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id, int? esc, int? atribu)
        {
            var atrib = await _atribuicao.Detalhes((int)atribu);

            try
            {
                await _atribuicao.Deletar(atrib);

                return RedirectToAction("Index", new { id = id, esc = esc });
            }
            catch
            {
                return View(atrib);
            }
        }

        public async Task<JsonResult> ListaCC(int id)
        {
            var lista = new List<SelectListItem>();
            lista = await _atribuicao.ListaCCurricular(0, id);

            return new JsonResult(new { Resultado = lista });
        }

        public async Task<JsonResult> ListaAno(int id)
        {
            var lista = new List<SelectListItem>();
            lista = await _atribuicao.ListaAno(0, id);

            return new JsonResult(new { Resultado = lista });
        }

        public async Task<ActionResult> Avaliar(int? id, int? esc, int? atribu)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (esc == null)
            {
                return NotFound();
            }

            if (atribu == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "AvaliacaoView", new { id = id, esc = esc, atrib = atribu });

        }
    }
}
