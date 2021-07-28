using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class EscolaViewController : Controller
    {
        private dbContext db = new dbContext();

        private List<SelectListItem> upcidade(int codigo, int codigoEstado)
        {
            var lista = new List<SelectListItem>();
            var cidades = (from c in db.TbCidades where c.CodigoEstado == codigoEstado select c).ToList<TbCidade>();

            try
            {
                foreach (var item in cidades)
                {
                    var option = new SelectListItem()
                    {
                        Text = item.Cidade,
                        Value = item.Codigo.ToString(),
                        Selected = (item.Codigo == codigo)
                    };

                    lista.Add(option);

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return lista;
        }

        private List<SelectListItem> upestados(int codigo)
        {
            var lista = new List<SelectListItem>();
            var estados = db.TbEstados.ToList();

            try
            {
                foreach (var item in estados)
                {
                    var option = new SelectListItem()
                    {
                        Text = item.Sigla,
                        Value = item.Codigo.ToString(),
                        Selected = (item.Codigo == codigo)
                    };

                    lista.Add(option);

                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return lista;
        }

        private TbColaborador carregaColaborador(int codigo)
        {
            try
            {
                var perfil = new TbColaborador();
                perfil = (from c in db.TbColaboradors where (c.Codigo == codigo) select c).First<TbColaborador>();

                return perfil;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        /*Listagem das escolas cadastradas de acordo com o nível de acesso.Para adm nivel 4, aparecem todas*/

        // GET: Escola
        public async Task<ActionResult> Index(int? id)
        {
            var model = new List<EscolaViewModel>();

            try
            {
                
                
                


                var consulta =  (from e in db.TbEscolas 
                                join c in db.TbCidades on e.CodigoCidade equals c.Codigo
                                join st in db.TbEstados on c.CodigoEstado equals st.Codigo
                                join atcoles in db.TbAtribuicaoColaboradorEscolas on e.Codigo equals atcoles.CodigoEscola
                                join col in db.TbColaboradors on atcoles.CodigoColaborador equals col.Codigo
                                orderby e.Escola
                                where atcoles.Ativa != 0 && col.Ativo != 0 && col.Codigo == Convert.ToInt32(id)
                                select  new { Codigo = e.Codigo, Escola = e.Escola, INEP = e.Inep ,nomeCidade = c.Cidade, sigla = st.Sigla, CodigoColaborador = col.Codigo}).ToList();
                var pcolaborador = new TbColaborador();
                pcolaborador = (from col in db.TbColaboradors where col.Codigo == Convert.ToInt32(id) select col).First();
                
                ViewData["Colaborador"] = pcolaborador;
                foreach (var item in consulta)
                {

                    model.Add(new EscolaViewModel{Codigo = item.Codigo, Escola = item.Escola, nomeCidade = item.nomeCidade, sigla = item.sigla, INEP = item.INEP });

                }

                

            }
            catch (Exception ex)
            {

                throw;
            }


            

            return View(model);
        }

        /*Segunda parte da inserçao de escola. Prepara o model para a View, trazendo informações do colaborador que está efetuando o cadastro
         lista de estados*/

        // GET: EscolaView
        public ActionResult Create(int id)
        {
            var cod = id;
            
            if (cod == 0)
            {
                return View();
            }
            else
            {
                var model = new EscolaViewModel();
                var perfil = new TbColaborador();
                perfil = (from c in db.TbColaboradors where (c.Codigo == id) select c).First<TbColaborador>();
                model.CodigoColaborador = perfil.Codigo;
                model.colaborador = perfil;
            return View(model);
            }

            
        }

        /*Segunda parte da inserçao de escola. Faz o cadastro da escola, atribui ao adm que a cadastrou e atribui aos demais adm nivel 4*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(include: "Codigo,Escola,Rua,Bairro,CodigoCidade,INEP,Ativa,CodigoColaborador")] EscolaViewModel novaescola)
        {
            
            if (novaescola is null)
            {
                throw new ArgumentNullException(nameof(novaescola));
            }
            if (ModelState.IsValid)
            {

                /*insere nova escola na tabela escola*/

                var escola = novaescola;
                var inserir = new TbEscola();
                inserir.Codigo = escola.Codigo;
                inserir.Escola = escola.Escola;
                inserir.Rua = escola.Rua;
                inserir.Bairro = escola.Bairro;
                inserir.CodigoCidade = Convert.ToInt32(escola.CodigoCidade);
                inserir.Inep = Convert.ToInt32(escola.INEP);
                inserir.Ativa = escola.Ativa;
                db.TbEscolas.Add(inserir);

                await db.SaveChangesAsync();


                /*insere atribuicao de escola para o primeiro admin que cadastrou*/
                var responsavel = Convert.ToInt32(escola.CodigoColaborador);
                var codigoescola = (from e in db.TbEscolas where (e.Inep == inserir.Inep) select e.Codigo);
                var codescola = codigoescola.First();
                var atnovaescola = new TbAtribuicaoColaboradorEscola();
                atnovaescola.CodigoColaborador = responsavel;
                atnovaescola.CodigoEscola = codescola;
                atnovaescola.Ativa = 1;
                await db.TbAtribuicaoColaboradorEscolas.AddAsync(atnovaescola);
                await db.SaveChangesAsync();

                
                var admin = new List<ColaboradorViewModel>();
                admin =  await (from c in db.TbColaboradors
                         join cg in db.TbCargos
                         on c.CodigoCargo equals cg.Codigo
                         select  new ColaboradorViewModel { Codigo = c.Codigo, NiveldeAcesso = cg.NiveldeAcesso }).ToListAsync();
                           
                /*insere atribuicao de escola para os prox admin*/
                
                foreach (var item in admin)
                {
                    if (item.Codigo != responsavel && item.NiveldeAcesso >= 4)
                    {
                        var atnovoadmin = new TbAtribuicaoColaboradorEscola();
                        {
                            atnovoadmin.CodigoColaborador = item.Codigo;
                            atnovoadmin.CodigoEscola = codescola;
                            atnovoadmin.Ativa = 1;
                        }
                        await db.TbAtribuicaoColaboradorEscolas.AddAsync(atnovoadmin);
                        await db.SaveChangesAsync();
                    }
                }
                

            }

            //string direcao = "Index/" + novaescola.CodigoColaborador;



            return RedirectToAction("Index",new { id= novaescola.CodigoColaborador });


        }

        /*Lista cidades conforme opção selecionada Select de estado na view */


        public JsonResult ListaCidade(int id)
        {
            var lista = new List<CidadeViewModel>();

            try
            {

                var consulta = (from c  in db.TbCidades join 
                               e in db.TbEstados on c.CodigoEstado equals e.Codigo
                               where (e.Codigo == id) orderby c.Cidade select c).ToList();

                foreach (var item in consulta)
                { 
                    lista.Add(new CidadeViewModel() { Codigo = item.Codigo, Cidade = item.Cidade});
                }

                
                                      
            }
            catch (Exception ex)
            {

                throw;
            }

            return new JsonResult(new{ Resultado = lista });
        }


        /*Ediçao dos Dados da Escola */

        public ActionResult Edit(int? id, int? col)
        {
            if (id == null)
            {
                return View(id);
            }

            var escola = (from e in db.TbEscolas
                          join c in db.TbCidades
                          on e.CodigoCidade equals c.Codigo
                          where e.Codigo == id
                          select new { Codigo = e.Codigo, Escola = e.Escola, Rua = e.Rua, Bairro = e.Bairro, INEP = e.Inep, Ativa = e.Ativa ,CodigoCidade = e.CodigoCidade, CodigoEstado = c.CodigoEstado }).First();

            var model = new EscolaViewModel();

            model.Codigo = escola.Codigo;
            model.Escola = escola.Escola;
            model.Rua = escola.Rua;
            model.Bairro = escola.Bairro;
            model.INEP = escola.INEP;
            model.CodigoCidade = escola.CodigoCidade;
            model.cidade = upcidade(escola.CodigoCidade, escola.CodigoEstado);
            model.estado = upestados(escola.CodigoEstado);
            model.Ativa = escola.Ativa;
            model.CodigoColaborador = Convert.ToInt32(col);
            
            ViewData["colaborador"] = carregaColaborador(Convert.ToInt32(col));

            if (model == null)
            {
                return View(id);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind(include: "Codigo,Escola,Rua,Bairro,CodigoCidade,INEP,Ativa,CodigoColaborador")] EscolaViewModel atualizaescola)
        {
            var col = Convert.ToInt32(atualizaescola.CodigoColaborador); 
            var escola = atualizaescola;
            var atualizar = new TbEscola();
            atualizar.Codigo = escola.Codigo;
            atualizar.Escola = escola.Escola;
            atualizar.Rua = escola.Rua;
            atualizar.Bairro = escola.Bairro;
            atualizar.CodigoCidade = Convert.ToInt32(escola.CodigoCidade);
            atualizar.Inep = Convert.ToInt32(escola.INEP);
            atualizar.Ativa = escola.Ativa;


            if (id != atualizar.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.TbEscolas.Update(atualizar);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbEscolasExists(atualizar.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = atualizaescola.CodigoColaborador });
            }

            var model = new EscolaViewModel();

            model.Codigo = escola.Codigo;
            model.Escola = escola.Escola;
            model.Rua = escola.Rua;
            model.Bairro = escola.Bairro;
            model.INEP = escola.INEP;
            model.CodigoCidade = escola.CodigoCidade;
            model.cidade = upcidade(escola.CodigoCidade, escola.CodigoEstado);
            model.estado = upestados(escola.CodigoEstado);
            model.Ativa = escola.Ativa;
            model.CodigoColaborador = Convert.ToInt32(col);

            ViewData["colaborador"] = carregaColaborador(Convert.ToInt32(col));

            if (model == null)
            {
                return View(id);
            }

            return View(model);


        }

        private bool TbEscolasExists(int id)
        {
            return db.TbEscolas.Any(e => e.Codigo == id);
        }

    }
}