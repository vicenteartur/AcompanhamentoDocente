using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Services
{
    public class CriterioViewService : ICriterioViewModel
    {

        private dbContext db = new dbContext();

        

        public SelectList Classificacao()
        {
            var lista = new SelectList(db.TbClassificacaoCriterios, "Codigo", "Classificacao");

            return lista;
        }

        public SelectList ClassificacaoUp(int classificacao)
        {
            var lista = new SelectList(db.TbClassificacaoCriterios, "Codigo", "Classificacao", classificacao);

            return lista;
        }
    

        public MultiSelectList CompCurri()
        {
            var cc = db.TbComponenteCurriculars.Include(c => c.CodigoModalidadeNavigation).OrderBy(a => a.CodigoModalidadeNavigation.Codigo).ToList();
            var ccaux = new List<TbComponenteCurricular>();

            foreach (var item in cc)
            {
                var linha = new TbComponenteCurricular
                {
                    Codigo = item.Codigo,
                    ComponenteCurricular = $"{item.ComponenteCurricular + " - " + item.CodigoModalidadeNavigation.Modalidade}"
                };

                ccaux.Add(linha);

            }


                var m = new MultiSelectList(ccaux, "Codigo", "ComponenteCurricular");
            

            return m;    
        }

        public MultiSelectList UpCompCurri(int crit)
        {
            var cc = db.TbComponenteCurriculars.Include(c => c.CodigoModalidadeNavigation)
                .OrderBy(a => a.CodigoModalidadeNavigation.Codigo).ToList();
            
            var compselecionado = (from ccc in db.TbCriterioComponenteCurriculars
                            where ccc.CodigoCriterioAvaliacao == crit
                            select ccc.CodigoComponenteCurricular).ToArray<int>();
            
            var ccaux = new List<TbComponenteCurricular>();

            foreach (var item in cc)
            {
                var linha = new TbComponenteCurricular
                {
                    Codigo = item.Codigo,
                    ComponenteCurricular = $"{item.ComponenteCurricular + " - " + item.CodigoModalidadeNavigation.Modalidade}"
                };

                ccaux.Add(linha);

            }
            var m = new MultiSelectList(ccaux, "Codigo", "ComponenteCurricular",compselecionado);


            return m;
        }

        public async Task Atualizar(CriterioViewModel criterio)
        {
            var crit = new TbCriterioAvaliacao()
            {
                Codigo = criterio.Codigo,
                Criterio = criterio.Criterio,
                CodigoClassificacaoCriterio = criterio.CodigoClassificacaoCriterio,
                Ativa = criterio.Ativa
            };

            db.TbCriterioAvaliacaos.Update(crit);
            await db.SaveChangesAsync();

            //concluir atualizaçao da atribuicao de componente curricular

        }

        public async Task Deletar(CriterioViewModel criterio)
        {
            var criteriodeletar = await db.TbCriterioComponenteCurriculars.Where(c => c.CodigoCriterioAvaliacao == criterio.Codigo).ToListAsync();
            await db.BulkDeleteAsync(criteriodeletar);

            var crit = new TbCriterioAvaliacao()
            {
                Codigo = criterio.Codigo,
                Criterio = criterio.Criterio,
                CodigoClassificacaoCriterio = criterio.CodigoClassificacaoCriterio,
                Ativa = criterio.Ativa
            };

            db.TbCriterioAvaliacaos.Remove(crit);
            await db.SaveChangesAsync();

        }

        public async Task<CriterioViewModel> Detalhes(int id)
        {
            var ccc = await (from tccc in db.TbCriterioComponenteCurriculars where tccc.CodigoCriterioAvaliacao == id select tccc.CodigoComponenteCurricular).ToArrayAsync();

            var criterio = await (from c in db.TbCriterioAvaliacaos
                                  join cc in db.TbCriterioComponenteCurriculars on c.Codigo equals cc.CodigoCriterioAvaliacao
                                  join ccur in db.TbComponenteCurriculars on cc.CodigoComponenteCurricular equals ccur.Codigo
                                  join m in db.TbModalidades on ccur.CodigoModalidade equals m.Codigo
                                  join cl in db.TbClassificacaoCriterios on c.CodigoClassificacaoCriterio equals cl.Codigo
                                  where c.Codigo == id
                                  select new CriterioViewModel 
                                  {
                                      Codigo = c.Codigo,
                                      Criterio = c.Criterio,
                                      CodigoClassificacaoCriterio = cl.Codigo,
                                      Ativa = c.Ativa,
                                      clcriterio = cl.Classificacao,

                                      

                                  }).FirstAsync();
            return criterio;
        }

        public async Task Inserir(List<CriterioViewModel> criterio)
        {
            var criterioparcial = criterio.First();
            var criterioaux = new TbCriterioAvaliacao()
            {
                Criterio = criterioparcial.Criterio,
                CodigoClassificacaoCriterio = criterioparcial.CodigoClassificacaoCriterio,
                Ativa = criterioparcial.Ativa
            };
            await db.TbCriterioAvaliacaos.AddAsync(criterioaux);
            await db.SaveChangesAsync();
            
            var codcriterio = await db.TbCriterioAvaliacaos.Where(c => c.Criterio == criterioaux.Criterio).FirstAsync<TbCriterioAvaliacao>();
            var listcriteriocc = new List<TbCriterioComponenteCurricular>();
            foreach (var item in criterio)
            {
                
                var criteriocc = new TbCriterioComponenteCurricular()
                {
                    CodigoCriterioAvaliacao = codcriterio.Codigo,
                    CodigoComponenteCurricular = item.CodigoCCUrricular,
                    Ativa = 1
                };
                listcriteriocc.Add(criteriocc);
            }
            await db.BulkInsertAsync(listcriteriocc);
        }

        public async Task<List<CriterioViewModel>> ListaCriterios(int ccurr)
        {
            var criterio = await(from c in db.TbCriterioAvaliacaos
                                 join cc in db.TbCriterioComponenteCurriculars on c.Codigo equals cc.CodigoCriterioAvaliacao
                                 join ccur in db.TbComponenteCurriculars on cc.CodigoComponenteCurricular equals ccur.Codigo
                                 join m in db.TbModalidades on ccur.CodigoModalidade equals m.Codigo
                                 join cl in db.TbClassificacaoCriterios on c.CodigoClassificacaoCriterio equals cl.Codigo
                                 where c.Ativa == 1 && ccur.Codigo == ccurr
                                 select new CriterioViewModel
                                 {
                                     Codigo = c.Codigo,
                                     Criterio = c.Criterio,
                                     CodigoClassificacaoCriterio = cl.Codigo,
                                     Ativa = c.Ativa,
                                     clcriterio = cl.Classificacao,
                                     CodigoCCUrricular = ccur.Codigo    

                                 }).ToListAsync();
            return criterio;
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
               .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public async Task<List<TbComponenteCurricular>> ListaCompCur()
        {
            var tbcomponentecurricular = await db.TbComponenteCurriculars.Include(cc=>cc.CodigoModalidadeNavigation)
               .ToListAsync();

            return tbcomponentecurricular;
        }

        public bool TbCriterioExists(int id)
        {
            return db.TbCriterioAvaliacaos.Any(e => e.Codigo == id);
        }
    }
}
