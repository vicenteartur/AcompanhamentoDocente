using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


// ############erro de consulta.

        // GET: ColaboradorViewController
        public async Task<IActionResult> Index(int id, int esc)
        {
            var model = new List<ColaboradorViewModel>();
            model = await _colabview.ColaboradorAtivo(id, esc);
            //ViewData["escola"] = await _colabview.ListaEscolas(id,0);
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
        public ActionResult Create(int? id, [Bind("Codigo,Escola,Rua,Bairro,CodigoCidade,Inep,Ativa")] EscolaViewModel evm)
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

        // GET: ColaboradorViewController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ColaboradorViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ColaboradorViewController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ColaboradorViewController/Delete/5
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
