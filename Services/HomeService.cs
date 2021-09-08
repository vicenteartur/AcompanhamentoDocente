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
    public class HomeService : IHome
    {

        private dbContext db = new dbContext();


        public async Task<List<SelectListItem>> ListaEstados(int CodigoEstado)
        {
            if (CodigoEstado != 0)
            {

                var lista = new List<SelectListItem>();
                var estados = await db.TbEstados.ToListAsync();

                try
                {
                    foreach (var item in estados)
                    {
                        var option = new SelectListItem()
                        {
                            Text = item.Sigla,
                            Value = item.Codigo.ToString(),
                            Selected = (item.Codigo == CodigoEstado)
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
                var estados = await db.TbEstados.ToListAsync();

                try
                {
                    foreach (var item in estados)
                    {
                        var option = new SelectListItem()
                        {
                            Text = item.Sigla,
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

        public async Task<List<SelectListItem>> ListaCidades(int CodigoEstado, int CodigoCidade)
        {
            if (CodigoEstado != 0)
            {
                var lista = new List<SelectListItem>();
                var cidades = await (from c in db.TbCidades where c.CodigoEstado == CodigoEstado select c).ToListAsync<TbCidade>();

                try
                {
                    foreach (var item in cidades)
                    {
                        var option = new SelectListItem()
                        {
                            Text = item.Cidade,
                            Value = item.Codigo.ToString(),
                            Selected = (item.Codigo == CodigoCidade)
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
                return lista;
            }
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors.Include(c => c.CodigoCargoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public async Task<List<EscolaViewModel>> ListaEscolasAtivas(int CodigoColaborador)
        {
            var consulta = await (from e in db.TbEscolas
                                  join c in db.TbCidades on e.CodigoCidade equals c.Codigo
                                  join st in db.TbEstados on c.CodigoEstado equals st.Codigo
                                  join atcoles in db.TbAtribuicaoColaboradorEscolas on e.Codigo equals atcoles.CodigoEscola
                                  join col in db.TbColaboradors on atcoles.CodigoColaborador equals col.Codigo
                                  orderby e.Escola
                                  where atcoles.Ativa != 0 && col.Ativo != 0 && col.Codigo == CodigoColaborador
                                  select new
                                  {
                                      Codigo = e.Codigo,
                                      Escola = e.Escola,
                                      INEP = e.Inep,
                                      nomeCidade = c.Cidade,
                                      sigla = st.Sigla,
                                      CodigoColaborador = col.Codigo
                                  }).ToListAsync();

            var listescolas = new List<EscolaViewModel>();

            foreach (var item in consulta)
            {
                listescolas.Add(new EscolaViewModel
                {
                    Codigo = item.Codigo,
                    Escola = item.Escola,
                    nomeCidade = item.nomeCidade,
                    sigla = item.sigla,
                    INEP = item.INEP
                });
            }

            return listescolas;

        }

        public async Task<EscolaViewModel> MontarEscola(int CodigoEscola, int CodigoColaborador)
        {
            if (CodigoEscola != 0)
            {
                var consulta = await (from e in db.TbEscolas
                                      join c in db.TbCidades
                                      on e.CodigoCidade equals c.Codigo
                                      join st in db.TbEstados
                                      on c.CodigoEstado equals st.Codigo
                                      where e.Codigo == CodigoEscola
                                      select new
                                      {
                                          Codigo = e.Codigo,
                                          Escola = e.Escola,
                                          Rua = e.Rua,
                                          Bairro = e.Bairro,
                                          INEP = e.Inep,
                                          Ativa = e.Ativa,
                                          CodigoCidade = e.CodigoCidade,
                                          CodigoEstado = c.CodigoEstado,
                                          nomeCidade = c.Cidade,
                                          sigla = st.Sigla
                                      }).FirstAsync();
                var colaborador = await db.TbColaboradors.Where(t => t.Codigo == CodigoColaborador).FirstAsync();
                var escola = new EscolaViewModel();

                escola.Codigo = consulta.Codigo;
                escola.Escola = consulta.Escola;
                escola.Rua = consulta.Rua;
                escola.Bairro = consulta.Bairro;
                escola.INEP = consulta.INEP;
                escola.CodigoCidade = consulta.CodigoCidade;
                escola.cidade = await ListaCidades(consulta.CodigoEstado, consulta.CodigoCidade);
                escola.estado = await ListaEstados(consulta.CodigoEstado);
                escola.Ativa = consulta.Ativa;
                escola.CodigoColaborador = CodigoColaborador;
                escola.colaborador = colaborador;
                escola.nomeCidade = consulta.nomeCidade;
                escola.sigla = consulta.sigla;


                return escola;
            }
            else
            {
                var colaborador = await db.TbColaboradors.Where(t => t.Codigo == CodigoColaborador).FirstAsync();

                var escola = new EscolaViewModel();
                escola.estado = await ListaEstados(0);
                escola.cidade = await ListaCidades(0, 0);
                escola.CodigoColaborador = CodigoColaborador;
                escola.colaborador = colaborador;
                return escola;
            }
        }
    }
}
