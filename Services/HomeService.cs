using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ModelsRelatorio;
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

        public async Task<List<GraficoViewModel>> RelatorioGeral(int CodigoEscola, int ano)
        {
            var relatorio = await (from e in db.TbEscolas
                                   join at in db.TbAtribuicaoColaboradorEscolas
                                   on e.Codigo equals at.CodigoEscola
                                   join accea in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                                   on at.Codigo equals accea.CodigoAtribuicaoColaboradorEscola
                                   join aval in db.TbAvaliacaos
                                   on accea.Codigo equals aval.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola
                                   join cc in db.TbComponenteCurriculars
                                   on accea.CodigoComponenteCurricular equals cc.Codigo
                                   join m in db.TbModalidades
                                   on cc.CodigoModalidade equals m.Codigo
                                   join cav in db.TbCriterioAvaliados
                                   on aval.Codigo equals cav.CodigoAvaliacao
                                   join crt in db.TbCriterioAvaliacaos
                                   on cav.CodigoCriterioAvaliacao equals crt.Codigo
                                   join classif in db.TbClassificacaoCriterios
                                   on crt.CodigoClassificacaoCriterio equals classif.Codigo
                                   
                                   where e.Codigo == CodigoEscola && aval.Finalizada == 1 && aval.CodigoColaboradorAvaliador!= at.CodigoColaborador && aval.Datarealizacao.Year == ano

                                   orderby m.Modalidade, cc.SubArea, cc.ComponenteCurricular

                                   select new Linhas_Avaliacao
                                   { 
                                   CodigoCC = cc.Codigo,
                                   Componente = cc.ComponenteCurricular,
                                   SubArea = cc.SubArea,
                                   CodigoModalidade = m.Codigo,
                                   Modalidade = m.Modalidade,
                                   CodCriterio = cav.Codigo,
                                   Conceito = cav.Conceito,
                                   CodClassCriterio = classif.Codigo,
                                   ClassCriterio = classif.Classificacao
                                   }).ToListAsync();

            var grafico = new List<GraficoViewModel>();

            while (relatorio.Count !=0)
            {

                           
                var aux = relatorio.First();
                
                        var listaaux = relatorio.Where(r => r.SubArea == aux.SubArea && r.CodigoModalidade == aux.CodigoModalidade).ToList();
                        int contador = 0;
                        int pontos = 0;
                        
                        foreach (var itemlistaaux in listaaux)
                            {
                                contador = contador + 1;
                                pontos = pontos + itemlistaaux.Conceito;
                            }

                        var linha = new GraficoViewModel
                        {
                            CodigoComponente = aux.CodigoCC,
                            Componente = aux.Componente,
                            Modalidade = aux.Modalidade,
                            SubArea = aux.SubArea,
                            Pontuacao = pontos,
                            PontuacaoMaxima = contador
                            
                        };
                        int calc = Convert.ToInt32(255-(255*((float)pontos/contador)));
                        linha.Aprov = calc;
                        grafico.Add(linha);
                    
                        foreach (var itemrem in listaaux)
                            {

                                relatorio.Remove(itemrem);

                            }

                        if (relatorio.Count == 0)
                        {
                            break;
                        }

            }

            return grafico;

        }

        public async Task<List<GraficoViewModel>> RelatorioSubArea(int CodigoEscola, string sub, int ano)
        {
            var relatorio = await (from e in db.TbEscolas
                                   join at in db.TbAtribuicaoColaboradorEscolas
                                   on e.Codigo equals at.CodigoEscola
                                   join accea in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                                   on at.Codigo equals accea.CodigoAtribuicaoColaboradorEscola
                                   join aval in db.TbAvaliacaos
                                   on accea.Codigo equals aval.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola
                                   join cc in db.TbComponenteCurriculars
                                   on accea.CodigoComponenteCurricular equals cc.Codigo
                                   join m in db.TbModalidades
                                   on cc.CodigoModalidade equals m.Codigo
                                   join cav in db.TbCriterioAvaliados
                                   on aval.Codigo equals cav.CodigoAvaliacao
                                   join crt in db.TbCriterioAvaliacaos
                                   on cav.CodigoCriterioAvaliacao equals crt.Codigo
                                   join classif in db.TbClassificacaoCriterios
                                   on crt.CodigoClassificacaoCriterio equals classif.Codigo

                                   where e.Codigo == CodigoEscola && aval.Finalizada == 1 && aval.CodigoColaboradorAvaliador != at.CodigoColaborador 
                                   && cc.SubArea==sub && aval.Datarealizacao.Year == ano

                                   orderby m.Modalidade, cc.SubArea, cc.ComponenteCurricular

                                   select new Linhas_Avaliacao
                                   {
                                       CodigoCC = cc.Codigo,
                                       Componente = cc.ComponenteCurricular,
                                       SubArea = cc.SubArea,
                                       CodigoModalidade = m.Codigo,
                                       Modalidade = m.Modalidade,
                                       CodCriterio = cav.Codigo,
                                       Conceito = cav.Conceito,
                                       CodClassCriterio = classif.Codigo,
                                       ClassCriterio = classif.Classificacao
                                   }).ToListAsync();

            var grafico = new List<GraficoViewModel>();

            while (relatorio.Count != 0)
            {


                var aux = relatorio.First();

                var listaaux = relatorio.Where(r => r.CodigoCC == aux.CodigoCC && r.Modalidade == aux.Modalidade).ToList();
                int contador = 0;
                int pontos = 0;

                foreach (var itemlistaaux in listaaux)
                {
                    contador = contador + 1;
                    pontos = pontos + itemlistaaux.Conceito;
                }

                var linha = new GraficoViewModel
                {
                    CodigoComponente = aux.CodigoCC,
                    Componente = aux.Componente,
                    Modalidade = aux.Modalidade,
                    Pontuacao = pontos,
                    PontuacaoMaxima = contador

                };
                int calc = Convert.ToInt32(255 - (255 * ((float)pontos / contador)));
                linha.Aprov = calc;
                grafico.Add(linha);

                foreach (var itemrem in listaaux)
                {

                    relatorio.Remove(itemrem);

                }

                if (relatorio.Count == 0)
                {
                    break;
                }

            }

            return grafico;

        }

    
    public async Task<List<GraficoViewModel>> RelatorioDisciplina(int CodigoEscola, int ccc, int ano)
        {
            var relatorio = await (from e in db.TbEscolas
                                   join at in db.TbAtribuicaoColaboradorEscolas
                                   on e.Codigo equals at.CodigoEscola
                                   join accea in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                                   on at.Codigo equals accea.CodigoAtribuicaoColaboradorEscola
                                   join aval in db.TbAvaliacaos
                                   on accea.Codigo equals aval.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola
                                   join cc in db.TbComponenteCurriculars
                                   on accea.CodigoComponenteCurricular equals cc.Codigo
                                   join m in db.TbModalidades
                                   on cc.CodigoModalidade equals m.Codigo
                                   join cav in db.TbCriterioAvaliados
                                   on aval.Codigo equals cav.CodigoAvaliacao
                                   join crt in db.TbCriterioAvaliacaos
                                   on cav.CodigoCriterioAvaliacao equals crt.Codigo
                                   join classif in db.TbClassificacaoCriterios
                                   on crt.CodigoClassificacaoCriterio equals classif.Codigo

                                   where e.Codigo == CodigoEscola && aval.Finalizada == 1 && aval.CodigoColaboradorAvaliador != at.CodigoColaborador
                                   && cc.Codigo == ccc && aval.Datarealizacao.Year == ano

                                   orderby m.Modalidade, cc.SubArea, cc.ComponenteCurricular

                                   select new Linhas_Avaliacao
                                   {
                                       CodigoCC = cc.Codigo,
                                       Componente = cc.ComponenteCurricular,
                                       SubArea = cc.SubArea,
                                       CodigoModalidade = m.Codigo,
                                       Modalidade = m.Modalidade,
                                       CodCriterio = cav.Codigo,
                                       Conceito = cav.Conceito,
                                       CodClassCriterio = classif.Codigo,
                                       ClassCriterio = classif.Classificacao
                                   }).ToListAsync();

            var grafico = new List<GraficoViewModel>();

            while (relatorio.Count != 0)
            {


                var aux = relatorio.First();

                var listaaux = relatorio.Where(r => r.CodClassCriterio == aux.CodClassCriterio).ToList();
                int contador = 0;
                int pontos = 0;

                foreach (var itemlistaaux in listaaux)
                {
                    contador = contador + 1;
                    pontos = pontos + itemlistaaux.Conceito;
                }

                var linha = new GraficoViewModel
                {
                    CodigoComponente = aux.CodigoCC,
                    Componente = aux.Componente,
                    Modalidade = aux.Modalidade,
                    ClassificacaoCriterio = aux.ClassCriterio,
                    Pontuacao = pontos,
                    PontuacaoMaxima = contador

                };
                int calc = Convert.ToInt32(255 - (255 * ((float)pontos / contador)));
                linha.Aprov = calc;
                grafico.Add(linha);

                foreach (var itemrem in listaaux)
                {

                    relatorio.Remove(itemrem);

                }

                if (relatorio.Count == 0)
                {
                    break;
                }

            }

            return grafico;

        }

        public async Task<List<Planilhas_Relatorio_Av>> RelatorioXLSXDisciplina(int CodigoEscola, int ccc, int ano)
        {
            var consulta = await (from cav in db.TbCriterioAvaliados
                                   join av in db.TbAvaliacaos
                                   on cav.CodigoAvaliacao equals av.Codigo
                                   join cr in db.TbCriterioAvaliacaos
                                   on cav.CodigoCriterioAvaliacao equals cr.Codigo
                                   join accea in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                                   on av.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola equals accea.Codigo
                                   join  ace in db.TbAtribuicaoColaboradorEscolas
                                   on accea.CodigoAtribuicaoColaboradorEscola equals ace.Codigo
                                   join col in db.TbColaboradors
                                   on ace.CodigoColaborador equals col.Codigo
                                   join e in db.TbEscolas
                                   on ace.CodigoEscola equals e.Codigo
                                   join cc in db.TbComponenteCurriculars
                                   on accea.CodigoComponenteCurricular equals cc.Codigo
                                   join an in db.TbAnos 
                                   on accea.CodigoAno equals an.Codigo
                                   join m in db.TbModalidades
                                   on an.CodigoModalidade equals m.Codigo

                                   where e.Codigo == CodigoEscola && av.Finalizada == 1 && av.CodigoColaboradorAvaliador != ace.CodigoColaborador
                                   && cc.Codigo == ccc && av.Datarealizacao.Year == ano

                                   orderby an.Ano, col.Nome

                                   select new linha_plan_relatorio_xls
                                   {
                                       CodigoAvaliacao = av.Codigo,
                                       dataavaliacao = av.Datarealizacao,
                                       Avaliado = col.Nome,
                                       codigoAvaliador = av.CodigoColaboradorAvaliador,
                                       ccurricular = cc.ComponenteCurricular,
                                       anoturma = an.Ano,
                                       modalidade = m.Modalidade,
                                       CodigoCriterio = cr.Codigo,
                                       Criterio = cr.Criterio,
                                       Conceito = cav.Conceito
                                   }).ToListAsync();

            var admin = await (from e in db.TbEscolas
                               join ac in db.TbAtribuicaoColaboradorEscolas
                               on e.Codigo equals ac.CodigoEscola
                               join c in db.TbColaboradors
                               on ac.CodigoColaborador equals c.Codigo
                               join cg in db.TbCargos
                               on c.CodigoCargo equals cg.Codigo
                               where cg.NiveldeAcesso > 0 && e.Codigo == CodigoEscola
                               select new TbColaborador()).ToListAsync();

            var relatorio = new List<Planilhas_Relatorio_Av>();

            foreach (var item in consulta)
            {
                
            }

            


            return relatorio;
        }
    }
}
