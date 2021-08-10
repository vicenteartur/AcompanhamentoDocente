using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class AnoController : Controller
    {
        private readonly IAno _ano;

        public AnoController()
        {
            _ano = new AnoService();
        }

        // GET: Ano
        public async Task<IActionResult> Index(int? id)
        {
            var admin = await _ano.MontarAdmin((int)id);
            var alunos = await _ano.Index();
            ViewData["admin"] = admin;
            return View(alunos);
        }

        // GET: Ano/Details/5
        public async Task<IActionResult> Details(int? id, int Codigo)
        {
            var admin = await _ano.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            if (Codigo == null)
            {
                return NotFound();
            }

            var tbAno = await _ano.Details(Codigo);
            if (tbAno == null)
            {
                return NotFound();
            }

            return View(tbAno);
        }

        // GET: Ano/Create
        public async Task<IActionResult> Create(int? id)
        {
            var admin = await _ano.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View();
        }

        // POST: Ano/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id,[Bind("Codigo,Ano,Turma,Modalidade,Periodo")] TbAno tbAno)
        {
            var admin = await _ano.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            if (ModelState.IsValid)
            {
                _ano.Create(tbAno);
                
                return RedirectToAction("Index", new { id = id});
            }
            return View(tbAno);
        }

        // GET: Ano/Edit/5
        public async Task<IActionResult> Edit(int? id, int Codigo)
        {
            var admin = await _ano.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (Codigo == null)
            {
                return NotFound();
            }

            var tbAno = await _ano.Details(Codigo);
            if (tbAno == null)
            {
                return NotFound();
            }
            return View(tbAno);
        }

        // POST: Ano/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Ano,Turma,Modalidade,Periodo")] TbAno tbAno)
        {
            var admin = await _ano.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ano.Edit(id, tbAno);
                    
                }
                catch (Exception)
                {
                    if (!_ano.TbAnoExists(tbAno.Codigo))
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
            return View(tbAno);
        }

        // GET: Ano/Delete/5
        public async Task<IActionResult> Delete(int? id, int Codigo)
        {
            var admin = await _ano.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            if (Codigo == null)
            {
                return NotFound();
            }

            var tbAno = await _ano.Details(Codigo);
            if (tbAno == null)
            {
                return NotFound();
            }

            return View(tbAno);
        }

        // POST: Ano/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? Codigo)
        {
            var tbAno = await _ano.Details(Codigo);
            _ano.Delete((TbAno)tbAno);
            return RedirectToAction("Index", new { id = id });
        }

        
    }
}
