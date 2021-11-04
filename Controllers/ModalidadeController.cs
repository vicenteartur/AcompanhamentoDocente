using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class ModalidadeController : Controller
    {
        private readonly IModalidade _modalidade;

        public ModalidadeController()
        {
            _modalidade = new ModalidadeService();
        }

        // GET: Modalidade
        public async Task<IActionResult> Index(int? id)
        {
            var admin = await _modalidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View(await _modalidade.ListaModalidade());
        }

        // GET: Modalidade/Details/5
        public async Task<IActionResult> Details(int? id, int? mod)
        {
            var admin = await _modalidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            var tbModalidade = await _modalidade.Detalhes((int)mod);

            if (tbModalidade == null)
            {
                return NotFound();
            }

            return View(tbModalidade);
        }

        // GET: Modalidade/Create
        public async Task<IActionResult> Create(int? id)
        {
            var admin = await _modalidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View();
        }

        // POST: Modalidade/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Codigo,Modalidade")] TbModalidade tbModalidade)
        {
            var admin = await _modalidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (ModelState.IsValid)
            {
                await _modalidade.Inserir(tbModalidade);

                return RedirectToAction("Index", new { id = id });
            }
            return View(tbModalidade);
        }

        // GET: Modalidade/Edit/5
        public async Task<IActionResult> Edit(int? id, int? mod)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbModalidade = await _modalidade.Detalhes((int)mod);

            if (tbModalidade == null)
            {
                return NotFound();
            }
            var admin = await _modalidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View(tbModalidade);
        }

        // POST: Modalidade/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,Modalidade")] TbModalidade tbModalidade)
        {
            if (id != tbModalidade.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _modalidade.Atualizar(tbModalidade);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_modalidade.TbModalidadeExists(tbModalidade.Codigo))
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
            var admin = await _modalidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View(tbModalidade);
        }

        // GET: Modalidade/Delete/5
        public async Task<IActionResult> Delete(int? id, int? mod)
        {
            var admin = await _modalidade.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            if (id == null)
            {
                return NotFound();
            }

            var tbModalidade = await _modalidade.Detalhes((int)mod);

            if (tbModalidade == null)
            {
                return NotFound();
            }

            return View(tbModalidade);
        }

        // POST: Modalidade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int codigo)
        {
            var tbModalidade = await _modalidade.Detalhes((int)codigo);
            await _modalidade.Deletar(tbModalidade);

            return RedirectToAction("Index", new { id = id });
        }
    }
}
