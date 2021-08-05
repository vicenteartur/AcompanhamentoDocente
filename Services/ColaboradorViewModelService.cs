using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Services
{
    public class ColaboradorViewModelService : IColaboradorViewModel
    {

        private dbContext db = new dbContext();

        public async Task<List<SelectListItem>> ListaCargos(int CodigoAdministrador, ColaboradorViewModel colaborador)
        {

            var colab = await (from col in db.TbColaboradors
                               join c in db.TbCargos on col.CodigoCargo equals c.Codigo
                               where col.Codigo == CodigoAdministrador
                               select c.NiveldeAcesso).FirstAsync();

            if (colaborador != null)
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
                            Selected = (item.Codigo == colaborador.CodigoCargo)
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

        public async Task<List<SelectListItem>> ListaEscolas(int CodigoAdministrador, ColaboradorViewModel colaborador)
        {
                
            var escola = await(from e in db.TbEscolas
                               join at in db.TbAtribuicaoColaboradorEscolas on e.Codigo equals at.CodigoEscola
                               where at.CodigoColaborador == CodigoAdministrador
                               select  e).ToListAsync<TbEscola>();

                if (colaborador != null)
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
                                Selected = (item.Codigo == colaborador.CodigoEscola)
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

        public Task<TbColaborador> MontarColaborador(int CodigoColaborador, int CodigoEscola, int CodigoAdministrador)
        {
            throw new NotImplementedException();
        }

        public Task<List<TbColaborador>> ColaboradorAtivo(int CodigoColaborador, int CodigoAdministrador)
        {
            throw new NotImplementedException();
        }

        public Task<List<TbColaborador>> ColaboradorInativo(int CodigoColaborador, int CodigoAdministrador)
        {
            throw new NotImplementedException();
        }

        public Task InserirColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }
        public Task RemoverColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }
        public Task AtivarColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }

        public Task InativarColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }

    }
}
