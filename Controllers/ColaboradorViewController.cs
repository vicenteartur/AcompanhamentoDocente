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
    public class ColaboradorViewController : Controller

        {

        private readonly IColaboradorViewModel _colabview;

        public ColaboradorViewController()
        {
            _colabview = new ColaboradorViewModelService();
        }


        // GET: ColaboradorViewController
        public async Task<ActionResult> Index(int id, int esc)
        {
            var model = new List<ColaboradorViewModel>();
            model = await _colabview.ColaboradorAtivo(id, esc);
            ViewData["id"] = id;
            ViewData["esc"] = esc;
            return View(model);
        }

        // GET: ColaboradorViewController/Details/5
        public async Task<ActionResult> Details(int id, int esc, int col)
        {
            var colaborador = await _colabview.localizaColaborador(col);
            

            return View( await _colabview.MontarColaborador(id,esc,colaborador));
        }

        // GET: ColaboradorViewController/Create
        public async Task<ActionResult> Create(int id, int esc)
        {
            return View(await _colabview.MontarColaborador(id, esc, null));
        }

        // POST: ColaboradorViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Codigo,Nome,Email,Ativo,CodigoCargo,Cargo,CodigoEscola,CodigoAdministrador")] ColaboradorViewModel colaborador)
        {
            int esc = colaborador.CodigoEscola;

            if (ModelState.IsValid)
            {

                try
                {
                    await _colabview.InserirColaborador(colaborador);
                    return RedirectToAction("Index", new { id = id, esc });

                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction("Index", new { id = id, esc });
            }
            
            var colab = await _colabview.MontarColaborador((int)id, esc, null);
            return View(colaborador);
        }

        // GET: ColaboradorViewController/Edit/5
        public async Task<ActionResult> Edit(int id, int esc, int col)
        {
            var colaborador = await _colabview.localizaColaborador(col);
            return View(await _colabview.MontarColaborador(id, esc, colaborador));
        }

        // POST: ColaboradorViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Codigo,Nome,Email,Ativo,CodigoCargo,Cargo,CodigoEscola,CodigoAdministrador")] ColaboradorViewModel colaborador)
        {

            int esc = colaborador.CodigoEscola;

            if (id != colaborador.CodigoAdministrador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    await _colabview.AtualizarColaborador(colaborador);
                    return RedirectToAction("Index", new { id = id, esc });
                    
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction("Index", new { id = id, esc });
            }
            var col = await _colabview.localizaColaborador(colaborador.Codigo);
            var colab = await _colabview.MontarColaborador((int)id, esc, col);
            return View(colaborador);

        }

        // GET: ColaboradorViewController/Delete/5
        public async Task<IActionResult> Delete(int id, int esc, int col)
        {
            var colaborador = await _colabview.localizaColaborador(col);
            return View(await _colabview.MontarColaborador(id, esc, colaborador));
        }

        // POST: ColaboradorViewController/Delete/5
        [HttpPost]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("Codigo,Nome,Email,Ativo,CodigoCargo,Cargo,CodigoEscola,CodigoAdministrador")] ColaboradorViewModel colaborador)
        {
            try
            {

                var removercolab = await _colabview.localizaColaborador(colaborador.Codigo);
                var montarremovido = await _colabview.MontarColaborador(colaborador.CodigoAdministrador, colaborador.CodigoEscola, removercolab);
                await _colabview.RemoverColaborador(montarremovido);

                return RedirectToAction("Index", new { id = montarremovido.CodigoAdministrador, esc = montarremovido.CodigoEscola });
            }
            catch
            {
                return View(colaborador);
            }
            return RedirectToAction("Index", new { id = colaborador.CodigoAdministrador, esc = colaborador.CodigoEscola });
        }

        public async Task<IActionResult> ListaColab(int id, int esc, string email)
        {
            var model = new List<ColaboradorViewModel>();
            
            if (email != null)
            
            {
                
                model = await _colabview.EstenderJornada(id, esc, email);
            }
            
            ViewData["id"] = id;
            ViewData["esc"] = esc;
            return View(model);
        }

        public async Task<IActionResult> EstendeAtribuicao(int id, int esc,int col)
        {
            

            
            await _colabview.AtribuirColaboradorEstendido(col, esc);



            return RedirectToAction("Index", new { id = id, esc = esc});
        }
    }
}
