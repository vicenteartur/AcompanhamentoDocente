using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AcompanhamentoDocente.Services
{
    public class ColaboradorViewModelService : IColaboradorViewModel
    {

        private dbContext db = new dbContext();

        public async Task<TbColaborador> localizaColaborador(int codigo)
        {
            var colaborador = await db.TbColaboradors.FindAsync(codigo);
            return colaborador;
        }

        private async Task<TbEscola> localizaescola(int codigo)
        {
            var escola = await db.TbEscolas.FindAsync(codigo);
            return escola;
        }

        public async Task<List<SelectListItem>> ListaCargos(int CodigoAdministrador, int CodigoCargo)
        {

            var colab = await (from col in db.TbColaboradors
                               join c in db.TbCargos on col.CodigoCargo equals c.Codigo
                               where col.Codigo == CodigoAdministrador
                               select c.NiveldeAcesso).FirstAsync();

            if (CodigoCargo != 0)
            {
                var lista = new List<SelectListItem>();
                var cargo = await db.TbCargos.Where(cg => cg.NiveldeAcesso <= colab).ToListAsync<TbCargo>();

                try
                {
                    foreach (var item in cargo)
                    {
                        var option = new SelectListItem()
                        {
                            Text = item.Cargo,
                            Value = item.Codigo.ToString(),
                            Selected = (item.Codigo == CodigoCargo)
                        };

                        lista.Add(option);

                    }
                }
                catch
                {

                    throw;
                }

                return lista;

            }

            else
            {
                var lista = new List<SelectListItem>();
                var cargo = await db.TbCargos.Where(cg => cg.NiveldeAcesso <= colab).ToListAsync<TbCargo>();

                try
                {
                    foreach (var item in cargo)
                    {
                        var option = new SelectListItem()
                        {
                            Text = item.Cargo,
                            Value = item.Codigo.ToString(),

                        };

                        lista.Add(option);

                    }
                }
                catch
                {

                    throw;
                }

                return lista;
            }
        }

        public async Task<List<SelectListItem>> ListaEscolas(int CodigoAdministrador, int CodigoEscola)
        {
                
            var escola = await(from e in db.TbEscolas
                               join at in db.TbAtribuicaoColaboradorEscolas on e.Codigo equals at.CodigoEscola
                               where at.CodigoColaborador == CodigoAdministrador
                               select  e).ToListAsync<TbEscola>();

                if (CodigoEscola != 0)
                {
                    var lista = new List<SelectListItem>();
                    

                    try
                    {
                        foreach (var item in escola)
                        {
                            var option = new SelectListItem()
                            {
                                Text = item.Escola,
                                Value = item.Codigo.ToString(),
                                Selected = (item.Codigo == CodigoEscola)
                            };

                            lista.Add(option);

                        }
                    }
                    catch
                    {

                        throw;
                    }

                    return lista;

                }

                else
                {
                    var lista = new List<SelectListItem>();
                    
                    try
                    {
                        foreach (var item in escola)
                        {
                            var option = new SelectListItem()
                            {
                                Text = item.Escola,
                                Value = item.Codigo.ToString(),

                            };

                            lista.Add(option);

                        }
                    }
                    catch
                    {

                        throw;
                    }

                    return lista;
                }

        }

        public async Task<ColaboradorViewModel> MontarColaborador(int CodigoAdministrador, int CodigoEscola, TbColaborador colaborador )
        {
            if (colaborador == null)
            {

                var cargo = await ListaCargos(CodigoAdministrador, 0);
                var escola = await ListaEscolas(CodigoAdministrador, 0);

                var consulta = (from e in db.TbEscolas
                                join at in db.TbAtribuicaoColaboradorEscolas
                                on e.Codigo equals at.CodigoEscola
                                join col in db.TbColaboradors
                                on at.CodigoColaborador equals col.Codigo
                                join cg in db.TbCargos
                                on col.CodigoCargo equals cg.Codigo
                                where e.Codigo == CodigoEscola && at.CodigoColaborador == CodigoAdministrador
                                select new ColaboradorViewModel
                                {
                                    CodigoAdministrador = col.Codigo,
                                    NomeAdministrador = col.Nome,
                                    CodigoCargoAdministrador = Convert.ToInt32(col.CodigoCargo),
                                    CargoAdministrador = cg.Cargo,
                                    cargo = cargo,
                                    CodigoEscola = e.Codigo,
                                    NomeEscola = e.Escola,
                                    escola = escola
                                }).FirstAsync();

                
                return await consulta;
            }
            else
            {
                
                var cargo = await ListaCargos(CodigoAdministrador, (int)colaborador.CodigoCargo);
                var esc = await localizaescola(CodigoEscola);
                var escola = await ListaEscolas(CodigoAdministrador, CodigoEscola);

                var consulta = (from e in db.TbEscolas
                                join at in db.TbAtribuicaoColaboradorEscolas
                                on e.Codigo equals at.CodigoEscola
                                join c in db.TbColaboradors
                                on at.CodigoColaborador equals c.Codigo
                                join cg in db.TbCargos
                                on c.CodigoCargo equals cg.Codigo
                                where e.Codigo == CodigoEscola && at.CodigoColaborador == CodigoAdministrador
                                select new ColaboradorViewModel
                                {
                                    Codigo = colaborador.Codigo,
                                    Nome = colaborador.Nome,
                                    Email = colaborador.Email,
                                    Ativo = colaborador.Ativo,
                                    CodigoCargo = Convert.ToInt32(colaborador.CodigoCargo),
                                    Cargo = colaborador.CodigoCargoNavigation.Cargo,
                                    CodigoEscola = esc.Codigo,
                                    NomeEscola = esc.Escola,
                                    CodigoAdministrador = c.Codigo,
                                    NomeAdministrador = c.Nome,
                                    CodigoCargoAdministrador = Convert.ToInt32(c.CodigoCargo),
                                    CargoAdministrador = cg.Cargo,
                                    cargo = cargo,
                                    escola = escola
                                }).FirstAsync();


                return await consulta;
            }
        }

        public async Task<List<ColaboradorViewModel>> ColaboradorAtivo(int CodigoAdministrador, int CodigoEscola )
        {
            
            var admin = await (from ad in db.TbColaboradors
                               join cgad in db.TbCargos
                               on ad.CodigoCargo equals cgad.Codigo
                               where ad.Codigo == CodigoAdministrador
                               select cgad.NiveldeAcesso).FirstAsync();


            var consulta = await (from e in db.TbEscolas
                           join at in db.TbAtribuicaoColaboradorEscolas
                           on e.Codigo equals at.CodigoEscola
                           join c in db.TbColaboradors
                           on at.CodigoColaborador equals c.Codigo
                           join cg in db.TbCargos
                           on c.CodigoCargo equals cg.Codigo
                           where c.Ativo != 0 && at.CodigoEscola == CodigoEscola && cg.NiveldeAcesso <= admin
                           select new ColaboradorViewModel { Codigo = c.Codigo, Nome = c.Nome, Email = c.Email, Cargo = cg.Cargo, CodigoAdministrador = CodigoAdministrador, CodigoEscola = CodigoEscola } ).ToListAsync();

            return consulta;
        }

        public async Task<List<TbColaborador>> ColaboradorInativo(int CodigoAdministrador, int CodigoEscola)
        {
            var admin = await(from ad in db.TbColaboradors
                              join cgad in db.TbCargos
                              on ad.CodigoCargo equals cgad.Codigo
                              where ad.Codigo == CodigoAdministrador
                              select cgad.NiveldeAcesso).FirstAsync();


            var consulta = await(from e in db.TbEscolas
                                 join at in db.TbAtribuicaoColaboradorEscolas
                                 on e.Codigo equals at.CodigoEscola
                                 join c in db.TbColaboradors
                                 on at.CodigoColaborador equals c.Codigo
                                 join cg in db.TbCargos
                                 on c.CodigoCargo equals cg.Codigo
                                 where c.Ativo != 1 && at.CodigoEscola == CodigoEscola && cg.NiveldeAcesso <= admin
                                 select c).ToListAsync<TbColaborador>();

            return consulta;
        }

        public async Task InserirColaborador(ColaboradorViewModel colaborador)
        {
            var col = new TbColaborador()
            {
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                CodigoCargo = colaborador.CodigoCargo,
                Ativo = 1
            };

            await db.TbColaboradors.AddAsync(col);
            await db.SaveChangesAsync();

            var colab = await db.TbColaboradors.Where(c => c.Email == col.Email).FirstAsync();
            

            var atribuicao = new TbAtribuicaoColaboradorEscola() 
            { 
                CodigoColaborador = colab.Codigo,
                CodigoEscola = colaborador.CodigoEscola,
                Ativa = 1
            };

            await db.TbAtribuicaoColaboradorEscolas.AddAsync(atribuicao);
            await db.SaveChangesAsync();

        }

        public async Task AtualizarColaborador(ColaboradorViewModel colaborador)
        {
            var col = new TbColaborador()
            {
                Codigo = colaborador.Codigo,
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                CodigoCargo = colaborador.CodigoCargo,
                Ativo = colaborador.Ativo
            };

            db.TbColaboradors.Update(col);
            await db.SaveChangesAsync();
        }
        public async Task RemoverColaborador(ColaboradorViewModel colaborador)
        {
            var removerartibuicao = new List<TbAtribuicaoColaboradorEscola>();
            removerartibuicao = await (from at in db.TbAtribuicaoColaboradorEscolas
                                       where at.CodigoColaborador == colaborador.Codigo
                                       select at).ToListAsync();

            foreach (var item in removerartibuicao)
            {
                var apagaratribuicao = new TbAtribuicaoColaboradorEscola();
                {
                    apagaratribuicao.Codigo = item.Codigo;
                    apagaratribuicao.CodigoColaborador = item.CodigoColaborador;
                    apagaratribuicao.CodigoEscola = item.CodigoEscola;
                    apagaratribuicao.Ativa = item.Ativa;
                }

                db.TbAtribuicaoColaboradorEscolas.Remove(apagaratribuicao);
                await db.SaveChangesAsync();
            }
            var col = new TbColaborador()
            {
                Codigo = colaborador.Codigo,
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                CodigoCargo = colaborador.CodigoCargo,
                Ativo = colaborador.Ativo
            };

            db.TbColaboradors.Remove(col);
            await db.SaveChangesAsync();
        }
        public async Task AtivarColaborador(ColaboradorViewModel colaborador)
        {
            var col = new TbColaborador()
            {
                Codigo = colaborador.Codigo,
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                CodigoCargo = colaborador.CodigoCargo,
                Ativo = 1
            };

            db.TbColaboradors.Update(col);
            await db.SaveChangesAsync();
        }

        public async Task InativarColaborador(ColaboradorViewModel colaborador)
        {
            var col = new TbColaborador()
            {
                Codigo = colaborador.Codigo,
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                CodigoCargo = colaborador.CodigoCargo,
                Ativo = 0
            };

            db.TbColaboradors.Update(col);
            await db.SaveChangesAsync();
        }

    }
}
