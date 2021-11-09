using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly dbContext db = new dbContext();

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copia os dados do RegisterViewModel para o IdentityUser
                var cl = db.TbColaboradors.Any(c => c.Email == model.Email);
                var novo = db.Users.Any(u => u.Email == model.Email);

                if (cl == false)
                {
                    ViewData["usuario"] = "usuario não encontrado";
                    return View(model);
                    
                }

                if (cl == true && novo == false)
                {
                    var user = new IdentityUser
                    {
                        UserName = model.Email,
                        Email = model.Email
                    };
                    // Armazena os dados do usuário na tabela AspNetUsers
                    
                    var colaborador = await db.TbColaboradors.Include(cg => cg.CodigoCargoNavigation).Where(c => c.Email == model.Email ).FirstAsync();

                    if (colaborador.CodigoCargoNavigation.NiveldeAcesso > 0)
                    {
                        var result = await userManager.CreateAsync(user, model.Password);


                        string regra = "";

                        if (colaborador.CodigoCargoNavigation.NiveldeAcesso == 4)
                        {
                            regra = "Admin";
                        }

                        else if (colaborador.CodigoCargoNavigation.NiveldeAcesso == 3)
                        {
                            regra = "Supervisor";
                        }

                        else if (colaborador.CodigoCargoNavigation.NiveldeAcesso == 2)
                        {
                            regra = "User";
                        }

                        else if (colaborador.CodigoCargoNavigation.NiveldeAcesso == 1)
                        {
                            regra = "Operador";
                        }

                        else if (colaborador.CodigoCargoNavigation.NiveldeAcesso == 0)
                        {
                            regra = "Visitante";
                        }

                        if (colaborador != null)
                        {
                            IdentityResult roleResult = await userManager.AddToRoleAsync(user, regra);
                        }
                        // Se o usuário foi criado com sucesso, faz o login do usuário
                        // usando o serviço SignInManager e redireciona para o Método Action Index
                        if (result.Succeeded)
                        {

                            await signInManager.SignInAsync(user, isPersistent: false);
                            return RedirectToAction("index", "home");
                        }
                    
                    // Se houver erros então inclui no ModelState
                    // que será exibido pela tag helper summary na validação
                         foreach (var error in result.Errors)
                            {
                              ModelState.AddModelError(string.Empty, error.Description);
                            
                        }
                    }
                }
            }
            
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var colaborador = await (from c in db.TbColaboradors
                                                     join cg in db.TbCargos on c.CodigoCargo equals cg.Codigo 
                                                     where c.Email == model.Email && c.Ativo == 1
                                                     select new {c.Codigo}).FirstAsync();

                    TempData["col"] = colaborador.ToString();
                    ViewData["admin"] = db.TbColaboradors.Where(c => c.Email == model.Email).FirstAsync();

                    return RedirectToAction("DashBoard", "Home",new { id = colaborador.Codigo });
                   
                }
                ModelState.AddModelError(string.Empty, "Login Inválido");
            }
            return View(model);
        }

        public async Task<IActionResult> montar(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var colaborador = await (from c in db.TbColaboradors
                                         join cg in db.TbCargos on c.CodigoCargo equals cg.Codigo
                                         where c.Email == model.Email && c.Ativo == 1
                                         select new { c.Codigo }).FirstAsync();


                ViewData["admin"] = db.TbColaboradors.Where(c => c.Email == model.Email).FirstAsync();

                return RedirectToAction("DashBoard", "Home", new { id = colaborador.Codigo });
            }
            else
            {

                ModelState.AddModelError(string.Empty, "Login Inválido");
            }

            return View(model);
        }

    }
}
