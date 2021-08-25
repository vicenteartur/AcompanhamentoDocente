﻿using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Services
{
    public class AvaliacaoViewModelService : IAvaliacaoViewModel
    {

        private dbContext db = new dbContext();

        public async Task Atualizar(TbCriterioAvaliado avaliacao)
        {
            db.TbCriterioAvaliados.Update(avaliacao);
            await db.SaveChangesAsync();
        }

        public async Task Deletar(AvaliacaoViewModel avaliacao)
        {
            var avaliado = await db.TbCriterioAvaliados.Where(ca => ca.CodigoAvaliacao == avaliacao.Codigo).ToListAsync();
            await db.BulkDeleteAsync(avaliado);
            var tbavl = new TbAvaliacao
            {
                Codigo = avaliacao.Codigo,
                CodigoColaboradorAvaliador = avaliacao.CodigoColaboradorAvaliador,
                CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola = avaliacao.CodigoACECCA,
                Finalizada = avaliacao.Finalizada
            };
            db.TbAvaliacaos.Remove(tbavl);
            await db.SaveChangesAsync();
        }

        public async Task<AvaliacaoViewModel> Detalhes(int esc, int atrib, int aval)
        {
            var adm = await (from cl in db.TbColaboradors
                             join cg in db.TbCargos on cl.CodigoCargo equals cg.Codigo
                             join ae in db.TbAtribuicaoColaboradorEscolas on cl.Codigo equals ae.CodigoColaborador
                             join es in db.TbEscolas on ae.CodigoEscola equals es.Codigo
                             where es.Codigo == esc && cg.NiveldeAcesso > 0
                             select cl).ToListAsync();

            var avaliacao =  await (from c in db.TbColaboradors
                             join at in db.TbAtribuicaoColaboradorEscolas on c.Codigo equals at.CodigoColaborador
                             join atribcc in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas on at.Codigo equals atribcc.CodigoAtribuicaoColaboradorEscola
                             join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                             join av in db.TbAvaliacaos on atribcc.Codigo equals av.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola
                             join cc in db.TbComponenteCurriculars on atribcc.CodigoComponenteCurricular equals cc.Codigo
                             join acc in db.TbCriterioComponenteCurriculars on cc.Codigo equals acc.CodigoComponenteCurricular
                             join an in db.TbAnos on atribcc.CodigoAno equals an.Codigo
                             join m in db.TbModalidades on an.CodigoModalidade equals m.Codigo
                             where c.Ativo == 1 && at.Ativa == 1 && atribcc.Ativa == 1 && e.Ativa ==1 && cc.Ativa == 1 && acc.Ativa == 1 &&
                             atribcc.Codigo == atrib && aval == av.Codigo
                             select new AvaliacaoViewModel
                             {
                                 Codigo = av.Codigo,
                                 dataavaliacao = av.Datarealizacao,
                                 CodigoColaboradorAvaliador = av.CodigoColaboradorAvaliador,
                                 NomeAvaliador = (from cadm in adm where cadm.Codigo == av.CodigoColaboradorAvaliador select cadm.Nome).First(),
                                 CodigoACECCA = atribcc.Codigo,
                                 NomeColaborador = c.Nome,
                                 escola = e.Escola,
                                 ano = $"{an.Ano + an.Turma + " - " + m.Modalidade + " - " + an.Periodo}",
                                 ccurric = cc.ComponenteCurricular,
                                 Finalizada = av.Finalizada
                             }
                             ).FirstAsync();
            avaliacao.CriterioAvaliado = await db.TbCriterioAvaliados
                                        .Include(ca => ca.CodigoCriterioAvaliacaoNavigation.CodigoClassificacaoCriterioNavigation)
                                        .Where(ca => ca.Codigo == aval)
                                        .OrderBy(ca => ca.CodigoCriterioAvaliacaoNavigation.CodigoClassificacaoCriterioNavigation.Classificacao)
                                        .ToListAsync();
            return avaliacao;
        }

        public async Task Inserir(AvaliacaoViewModel avaliacao)
        {
            var av = new TbAvaliacao
            {
                Datarealizacao = DateTime.Today,
                CodigoColaboradorAvaliador = avaliacao.CodigoColaboradorAvaliador,
                CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola = avaliacao.CodigoACECCA,
                Finalizada = 0
            };
            await db.TbAvaliacaos.AddAsync(av);
            await db.SaveChangesAsync();

            var aval = await db.TbAvaliacaos.Where(a => a.CodigoColaboradorAvaliador == avaliacao.CodigoColaboradorAvaliador &&
                                             a.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola == avaliacao.CodigoACECCA &&
                                             a.Datarealizacao == DateTime.Today).FirstAsync();

            var criterios =  await (from cc in db.TbCriterioComponenteCurriculars
                             join ca in db.TbCriterioAvaliacaos on cc.CodigoCriterioAvaliacao equals ca.Codigo
                             join at in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas on cc.CodigoComponenteCurricular equals at.CodigoComponenteCurricular
                             where cc.Ativa == 1 && ca.Ativa == 1 && at.Ativa ==1 && at.Codigo ==avaliacao.CodigoACECCA 
                             orderby ca.CodigoClassificacaoCriterio ascending orderby ca.Criterio
                             select new TbCriterioAvaliado 
                             {
                                 CodigoAvaliacao = aval.Codigo,
                                 CodigoCriterioAvaliacao = ca.Codigo,
                                 Conceito = 0,
                                 Comentário="-"
                             }).ToListAsync();

            await db.BulkInsertAsync(criterios);

        }

        public async Task<List<AvaliacaoViewModel>> ListaAvaliacoes(int esc)
        {
            var adm = await (from cl in db.TbColaboradors
                             join cg in db.TbCargos on cl.CodigoCargo equals cg.Codigo
                             join ae in db.TbAtribuicaoColaboradorEscolas on cl.Codigo equals ae.CodigoColaborador
                             join es in db.TbEscolas on ae.CodigoEscola equals es.Codigo
                             where es.Codigo == esc && cg.NiveldeAcesso > 0 select cl).ToListAsync();
            
            var avaliacao = await(from c in db.TbColaboradors
                                  join at in db.TbAtribuicaoColaboradorEscolas on c.Codigo equals at.CodigoColaborador
                                  join atribcc in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas on at.Codigo equals atribcc.CodigoAtribuicaoColaboradorEscola
                                  join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                                  join av in db.TbAvaliacaos on atribcc.Codigo equals av.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola
                                  join cc in db.TbComponenteCurriculars on atribcc.CodigoComponenteCurricular equals cc.Codigo
                                  join acc in db.TbCriterioComponenteCurriculars on cc.Codigo equals acc.CodigoComponenteCurricular
                                  join an in db.TbAnos on atribcc.CodigoAno equals an.Codigo
                                  join m in db.TbModalidades on an.CodigoModalidade equals m.Codigo
                                  where c.Ativo == 1 && at.Ativa == 1 && atribcc.Ativa == 1 && e.Ativa == 1 && cc.Ativa == 1 && acc.Ativa == 1 &&
                                  e.Codigo == esc 
                                  select new AvaliacaoViewModel
                                  {
                                      Codigo = av.Codigo,
                                      dataavaliacao = av.Datarealizacao,
                                      CodigoColaboradorAvaliador = av.CodigoColaboradorAvaliador,
                                      NomeAvaliador = (from cadm in adm where cadm.Codigo == av.CodigoColaboradorAvaliador select cadm.Nome ).First(),
                                      CodigoACECCA = atribcc.Codigo,
                                      NomeColaborador = c.Nome,
                                      escola = e.Escola,
                                      ano = $"{an.Ano + an.Turma + " - " + m.Modalidade + " - " + an.Periodo}",
                                      ccurric = cc.ComponenteCurricular,
                                      Finalizada = av.Finalizada
                                  }
                             ).ToListAsync();
            
            

            return avaliacao;
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
                   .FirstAsync(m => m.Codigo == id);

            return tbcolaborador;
        }
        public bool AvaliacaoExists(int id)
        {
            return db.TbAvaliacaos.Any(e => e.Codigo == id);
        }

        public Task<TbEscola> MontarEscola(int id)
        {
            throw new NotImplementedException();
        }
    }
}
