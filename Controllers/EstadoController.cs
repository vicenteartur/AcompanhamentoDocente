using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class EstadoController : Controller
    {
        private readonly IEstado _estado;

        public EstadoController()
        {
            _estado = new EstadoService();
        }

        
        // GET: Estado
        public async Task<IActionResult> Index(int? id)
        {
            var admin = await _estado.MontarAdmin((int)id);
            
            ViewData["admin"] = admin;
            
            return View(await _estado.ListaEstado());
        }

        // GET: Estado/Details/5
        public async Task<IActionResult> Details(int? id, int Estado)
        {
            var admin = await _estado.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            if (id == null)
            {
                return NotFound();
            }

            var tbEstado = await _estado.Detalhes(Estado);

            if (tbEstado == null)
            {
                return NotFound();
            }

            return View(tbEstado);
        }

        // GET: Estado/Create
        public async Task<IActionResult> Create(int? id)
        {
            var admin = await _estado.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View();
        }

        // POST: Estado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Codigo,Estado,Sigla")] TbEstado tbEstado)
        {
            var admin = await _estado.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (ModelState.IsValid)
            {
                await _estado.Inserir(tbEstado);

                return RedirectToAction("Index", new { id = id });
            }
            return View(tbEstado);
        }

        // GET: Estado/Edit/5
        public async Task<IActionResult> Edit(int? id, int? estado)
        {
            var admin = await _estado.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            var tbEstado = await _estado.Detalhes((int)estado);

            if (tbEstado == null)
            {
                return NotFound();
            }
            return View(tbEstado);
        }

        // POST: Estado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Codigo,Estado,Sigla")] TbEstado tbEstado)
        {
            var admin = await _estado.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id != null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _estado.Atualizar(tbEstado);
                    
                }
                catch (Exception)
                {
                    if (!_estado.TbEstadoExists(tbEstado.Codigo))
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
            return View(tbEstado);
        }

        // GET: Estado/Delete/5
        public async Task<IActionResult> Delete(int? id, int estado)
        {
            var admin = await _estado.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            var tbEstado = await _estado.Detalhes((int) estado);
            if (tbEstado == null)
            {
                return NotFound();
            }

            return View(tbEstado);
        }

        // POST: Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? codigo)
        {
            var tbEstado = await _estado.Detalhes((int)codigo);
            await _estado.Deletar(tbEstado);

            return RedirectToAction("Index", new { id = id });
        }

      }
}
