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
            ViewData["colaborador"] = col;
            var lista = await _acesso.ListaEscolasAtivas(id);
            return View(lista);
        }

        public async Task<IActionResult> GerenciarEscola(int id, int esc)
        {
            var col = await _acesso.MontarAdmin(id);
            ViewData["colaborador"] = col;
            var escola = await _acesso.MontarEscola(esc, id);
            ViewData["escola"] = escola;
            return View();
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
