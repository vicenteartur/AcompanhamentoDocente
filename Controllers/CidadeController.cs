using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class CidadeController : Controller
    {
        private readonly ICidade _cidade;

        public CidadeController()
        {
            _cidade = new CidadeService();
        }

        // GET: Cidade
        public async Task<IActionResult> Index(int? id)
        {
            var admin = await _cidade.MontarAdmin((int)id);

            ViewData["admin"] = admin;

            return View(await _cidade.ListaCidade());
        }

        // GET: Cidade/Details/5
        public async Task<IActionResult> Details(int? id, int cidade)
        {
            var admin = await _cidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            var tbcidade = await _cidade.Detalhes(cidade);

            if (tbcidade == null)
            {
                return NotFound();
            }

            return View(tbcidade);
        }

        // GET: Cidade/Create
        public async Task<IActionResult> Create(int? id)
        {
            var admin = await _cidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            ViewData["CodigoEstado"] = _cidade.ListaEstado();
            return View();
        }

        // POST: Cidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Codigo,Cidade,CodigoEstado")] TbCidade tbCidade)
        {
            var admin = await _cidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (ModelState.IsValid)
            {
                await _cidade.Inserir(tbCidade);

                return RedirectToAction("Index", new { id = id });
            }

            ViewData["CodigoEstado"] = _cidade.ListaEstado();
            return View(tbCidade);
        }

        // GET: Cidade/Edit/5
        public async Task<IActionResult> Edit(int? id, int? cidade)
        {
            var admin = await _cidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            var tbCidade = await _cidade.Detalhes((int)cidade);
            if (tbCidade == null)
            {
                return NotFound();
            }
            ViewData["CodigoEstado"] = _cidade.ListaEstadoUp(tbCidade);
            return View(tbCidade);
        }

        // POST: Cidade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Cidade,CodigoEstado")] TbCidade tbCidade)
        {

            var admin = await _cidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id != tbCidade.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cidade.Atualizar(tbCidade);
                }
                catch (Exception)
                {
                    if (!_cidade.TbCidadeExists(tbCidade.Codigo))
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
            ViewData["CodigoEstado"] = _cidade.ListaEstadoUp(tbCidade);
            return View(tbCidade);
        }

        // GET: Cidade/Delete/5
        public async Task<IActionResult> Delete(int? id, int? cidade)
        {
            var admin = await _cidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            var tbCidade = await _cidade.Detalhes((int)cidade);
            if (tbCidade == null)
            {
                return NotFound();
            }

            return View(tbCidade);
        }

        // POST: Cidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? codigo)
        {
            var tbCidade = await _cidade.Detalhes((int)codigo);
            await _cidade.Deletar(tbCidade);
            return RedirectToAction("Index", new { id = id });
        }

    }
}
