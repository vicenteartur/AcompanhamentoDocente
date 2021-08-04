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
    public class EscolaViewModelService : IEscolaViewModel
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
            if (CodigoEstado!= 0)
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
                                select new { Codigo = e.Codigo, Escola = e.Escola, Rua = e.Rua, Bairro = e.Bairro, INEP = e.Inep, 
                                Ativa = e.Ativa, CodigoCidade = e.CodigoCidade, CodigoEstado = c.CodigoEstado, nomeCidade = c.Cidade, sigla = st.Sigla }).FirstAsync();
                var colaborador = await db.TbColaboradors.Where(t => t.Codigo == CodigoColaborador).FirstAsync();
                var escola = new EscolaViewModel();

                escola.Codigo =             consulta.Codigo;
                escola.Escola =             consulta.Escola;
                escola.Rua =                consulta.Rua;
                escola.Bairro =             consulta.Bairro;
                escola.INEP =               consulta.INEP;
                escola.CodigoCidade =       consulta.CodigoCidade;
                escola.cidade =             await ListaCidades(consulta.CodigoEstado, consulta.CodigoCidade);
                escola.estado =             await ListaEstados (consulta.CodigoEstado);
                escola.Ativa =              consulta.Ativa;
                escola.CodigoColaborador =  CodigoColaborador;
                escola.colaborador       =  colaborador;
                escola.nomeCidade =         consulta.nomeCidade;
                escola.sigla =              consulta.sigla;
                
                                
                return escola;
            }
            else
            {
                var colaborador = await db.TbColaboradors.Where(t => t.Codigo == CodigoColaborador).FirstAsync();
                
                var escola = new EscolaViewModel();
                escola.estado = await ListaEstados(0);
                escola.cidade = await ListaCidades(0,0);
                escola.CodigoColaborador = CodigoColaborador;
                escola.colaborador = colaborador;
                return escola;
            }
        }

        public async Task InserirEscola(EscolaViewModel Escola)
        {
            var inserir = new TbEscola();
            inserir.Codigo = Escola.Codigo;
            inserir.Escola = Escola.Escola;
            inserir.Rua = Escola.Rua;
            inserir.Bairro = Escola.Bairro;
            inserir.CodigoCidade = Escola.CodigoCidade;
            inserir.Inep = Escola.INEP;
            inserir.Ativa = 1;
            await db.TbEscolas.AddAsync(inserir);
            await db.SaveChangesAsync();

            var codigoescola = await (from e in db.TbEscolas where (e.Inep == inserir.Inep) select e.Codigo).FirstAsync();

            var codigoadmin = await (from c in db.TbColaboradors
                                     join cg in db.TbCargos
                                     on c.CodigoCargo equals cg.Codigo
                                     where cg.NiveldeAcesso >= 4
                                     select c).ToListAsync();

                foreach (var item in codigoadmin)
                {
                    var atnovoadmin = new TbAtribuicaoColaboradorEscola();
                    {
                        atnovoadmin.CodigoColaborador = item.Codigo;
                        atnovoadmin.CodigoEscola = inserir.Codigo;
                        atnovoadmin.Ativa = 1;
                    }

                await db.TbAtribuicaoColaboradorEscolas.AddAsync(atnovoadmin);
                }

            await db.SaveChangesAsync();

        }

        public async Task AtualizarEscola(EscolaViewModel Escola)
        {
            var atualizar = new TbEscola();
            atualizar.Codigo = Escola.Codigo;
            atualizar.Escola = Escola.Escola;
            atualizar.Rua = Escola.Rua;
            atualizar.Bairro = Escola.Bairro;
            atualizar.CodigoCidade = Escola.CodigoCidade;
            atualizar.Inep = Escola.INEP;
            atualizar.Ativa = Escola.Ativa;
            db.TbEscolas.Update(atualizar);
            await db.SaveChangesAsync();
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
                            select new { Codigo = e.Codigo, Escola = e.Escola, INEP = e.Inep, nomeCidade = c.Cidade, 
                                        sigla = st.Sigla, CodigoColaborador = col.Codigo }).ToListAsync();
            
            var listescolas = new List<EscolaViewModel>();

            foreach (var item in consulta)
            {
                listescolas.Add(new EscolaViewModel { Codigo = item.Codigo, Escola = item.Escola, 
                    nomeCidade = item.nomeCidade, sigla = item.sigla, INEP = item.INEP });
            }

            return listescolas;
        }

        public async Task<List<EscolaViewModel>> ListaEscolasInativas(int CodigoColaborador)
        {
            var consulta = await (from e in db.TbEscolas
                                  join c in db.TbCidades on e.CodigoCidade equals c.Codigo
                                  join st in db.TbEstados on c.CodigoEstado equals st.Codigo
                                  join atcoles in db.TbAtribuicaoColaboradorEscolas on e.Codigo equals atcoles.CodigoEscola
                                  join col in db.TbColaboradors on atcoles.CodigoColaborador equals col.Codigo
                                  join cg in db.TbCargos on col.CodigoCargo equals cg.Codigo
                                  orderby e.Escola
                                  where e.Ativa == 0 && col.Ativo != 0 && col.Codigo == CodigoColaborador
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

        public async Task RemoverEscola(EscolaViewModel Escola)
        {
            var atualizar = new TbEscola();
            atualizar.Codigo = Escola.Codigo;
            atualizar.Escola = Escola.Escola;
            atualizar.Rua = Escola.Rua;
            atualizar.Bairro = Escola.Bairro;
            atualizar.CodigoCidade = Escola.CodigoCidade;
            atualizar.Inep = Escola.INEP;
            atualizar.Ativa = 0;
            db.TbEscolas.Update(atualizar);
            await db.SaveChangesAsync();
        }
    

        public async Task ApagarEscola(EscolaViewModel Escola)
        {
            
            

            var removerartibuicao = new List<TbAtribuicaoColaboradorEscola>();
            removerartibuicao = await (from at in db.TbAtribuicaoColaboradorEscolas where at.CodigoEscola == Escola.Codigo 
                                     select at ).AsNoTracking().ToListAsync();

            foreach (var item in removerartibuicao)
                {
                    var apagaratribuicao = new TbAtribuicaoColaboradorEscola();
                    {
                    dbContext db = new dbContext();
                        apagaratribuicao.Codigo =               item.Codigo;
                        apagaratribuicao.CodigoColaborador =    item.CodigoColaborador;
                        apagaratribuicao.CodigoEscola =         item.CodigoEscola;
                        apagaratribuicao.Ativa =                item.Ativa;
                    }

                    db.TbAtribuicaoColaboradorEscolas.Remove(apagaratribuicao);
                    await db.SaveChangesAsync();
            }
                

                var remover = new TbEscola();
                remover.Codigo = Escola.Codigo;
                remover.Escola = Escola.Escola;
                remover.Rua = Escola.Rua;
                remover.Bairro = Escola.Bairro;
                remover.CodigoCidade = Escola.CodigoCidade;
                remover.Inep = Escola.INEP;
                remover.Ativa = 0;

                db.TbEscolas.Remove(remover);

                await db.SaveChangesAsync();


        }

        public void Salvar()
        {
            throw new NotImplementedException();
        }

        public async Task<TbColaborador> MontarColaborador(int CodigoColaborador)
        {
            var colaborador = new TbColaborador();
            colaborador = await db.TbColaboradors.Where(c => c.Codigo == CodigoColaborador).FirstAsync();
            return colaborador;
        }
    }
}
