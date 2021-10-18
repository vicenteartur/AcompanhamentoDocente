using AcompanhamentoDocente.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Services;

namespace AcompanhamentoDocente.Controllers
{
    public class HomeController : Controller
    {
        
        
        private readonly IHome _acesso;

        public HomeController()
        {
            
            _acesso = new HomeService();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            
            return View();
        }

        public async Task<IActionResult> DashBoard(int id)
        {
            var col = await _acesso.MontarAdmin(id);
            ViewData["admin"] = col;
            var lista = await _acesso.ListaEscolasAtivas(id);
            return View(lista);
        }

        public async Task<IActionResult> GerenciarEscola(int id, int esc)
        {

            var rel = await _acesso.RelatorioGeral(esc);
            var col = await _acesso.MontarAdmin(id);
            ViewData["admin"] = col;
            var escola = await _acesso.MontarEscola(esc, id);
            ViewData["escola"] = escola;
            return View(rel);
        }

        public async Task<IActionResult> DetalhesSubArea(int id, int esc, string sub)
        {

            var rel = await _acesso.RelatorioSubArea(esc, sub);


            ViewData["admin"] = await _acesso.MontarAdmin(id);
            ViewData["escola"] = await _acesso.MontarEscola(esc, id);
            return View(rel);
        }

        public async Task<IActionResult> DetalhesDisciplina(int id, int esc, int ccc)
        {

            var rel = await _acesso.RelatorioDisciplina(esc, ccc);


            ViewData["admin"] = await _acesso.MontarAdmin(id);
            ViewData["escola"] = await _acesso.MontarEscola(esc, id);
            return View(rel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult Colaborador (int? id)
        //{
        //    return RedirectToAction("Index", "Colaborador", new { id = id }); ;
        //}

    }
}
