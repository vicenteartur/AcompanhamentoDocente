using AcompanhamentoDocente.Interface;
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

        public async Task<TbAvaliacao>AvaliacaoExisteAberta(int atrib)
        {
            var avaliacao = await db.TbAvaliacaos.Where(a => a.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola == atrib && a.Finalizada != 1).FirstOrDefaultAsync();
            
            return avaliacao;
        }
        public async Task<TbCriterioAvaliado> MontarCritAv(int avaliacao)
        {
            var critav = await db.TbCriterioAvaliados.Include(ct => ct.CodigoCriterioAvaliacaoNavigation).Where(c => c.Codigo == avaliacao).FirstAsync();
            await db.SaveChangesAsync();
            return critav;
        }

        public async Task AtualizaCritAv(TbCriterioAvaliado avaliacao)
        {
            if (avaliacao.Comentario == "-")
            {
                avaliacao.Comentario = "ok";
            }
            db.TbCriterioAvaliados.Update(avaliacao);
            await db.SaveChangesAsync();
        }

        public async Task Atualizar(TbAvaliacao avaliacao)
        {
            db.TbAvaliacaos.Update(avaliacao);
            await db.SaveChangesAsync();
        }

        public async Task Deletar(int avaliacao)
        {
            var avaliado = await db.TbCriterioAvaliados.Where(ca => ca.CodigoAvaliacao == avaliacao).ToListAsync();
            await db.BulkDeleteAsync(avaliado);

            var aval = await db.TbAvaliacaos.Where(av => av.Codigo == avaliacao).FirstAsync();
            db.TbAvaliacaos.Remove(aval);
            await db.SaveChangesAsync();
        }

        public async Task<AvaliacaoViewModel> Detalhes(int esc, int atrib, int aval)
        {
            
            
            var avaliacao = await (from c in db.TbColaboradors
                                   join at in db.TbAtribuicaoColaboradorEscolas on c.Codigo equals at.CodigoColaborador
                                   join atribcc in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas on at.Codigo equals atribcc.CodigoAtribuicaoColaboradorEscola
                                   join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                                   join av in db.TbAvaliacaos on atribcc.Codigo equals av.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola
                                   join cc in db.TbComponenteCurriculars on atribcc.CodigoComponenteCurricular equals cc.Codigo
                                   join acc in db.TbCriterioComponenteCurriculars on cc.Codigo equals acc.CodigoComponenteCurricular
                                   join an in db.TbAnos on atribcc.CodigoAno equals an.Codigo
                                   join m in db.TbModalidades on an.CodigoModalidade equals m.Codigo
                                   where c.Ativo == 1 && at.Ativa == 1 && atribcc.Ativa == 1 && e.Ativa == 1 && cc.Ativa == 1 && acc.Ativa == 1 &&
                                   atribcc.Codigo == atrib && aval == av.Codigo
                                   select new AvaliacaoViewModel
                                   {
                                       Codigo = av.Codigo,
                                       dataavaliacao = av.Datarealizacao,
                                       CodigoColaboradorAvaliador = av.CodigoColaboradorAvaliador,
                                       CodigoACECCA = atribcc.Codigo,
                                       NomeColaborador = c.Nome,
                                       escola = e.Escola,
                                       ano = $"{an.Ano + an.Turma + " - " + m.Modalidade + " - " + an.Periodo}",
                                       ccurric = cc.ComponenteCurricular,
                                       Finalizada = av.Finalizada
                                   }
                             ).FirstAsync();

            var adm = await db.TbColaboradors.Where(c => c.Codigo == avaliacao.CodigoColaboradorAvaliador).FirstAsync();
            avaliacao.NomeAvaliador = adm.Nome;




            avaliacao.CriterioAvaliado = await db.TbCriterioAvaliados
                                        .Include(ca => ca.CodigoCriterioAvaliacaoNavigation.CodigoClassificacaoCriterioNavigation)
                                        .Where(ac => ac.CodigoAvaliacao == aval)
                                        .OrderBy(a => a.CodigoCriterioAvaliacaoNavigation.CodigoClassificacaoCriterioNavigation.Classificacao)
                                        .ToListAsync();
            return avaliacao;
        }

        public async Task<AvaliacaoViewModel> Inserir(AvaliacaoViewModel avaliacao)
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
                                             a.Datarealizacao == DateTime.Today && a.Finalizada < 1).OrderBy(av =>av.Datarealizacao).LastOrDefaultAsync();

            var criterios = await (from cc in db.TbCriterioComponenteCurriculars
                                   join ca in db.TbCriterioAvaliacaos on cc.CodigoCriterioAvaliacao equals ca.Codigo
                                   join at in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas on cc.CodigoComponenteCurricular equals at.CodigoComponenteCurricular
                                   where cc.Ativa == 1 && ca.Ativa == 1 && at.Ativa == 1 && at.Codigo == avaliacao.CodigoACECCA
                                   orderby ca.CodigoClassificacaoCriterio,ca.Criterio ascending
                                   
                                   select new TbCriterioAvaliado
                                   {
                                       CodigoAvaliacao = aval.Codigo,
                                       CodigoCriterioAvaliacao = ca.Codigo,
                                       Conceito = 0,
                                       Comentario = "-"
                                   }).ToListAsync();

            await db.BulkInsertAsync(criterios);

            var avalvm = new AvaliacaoViewModel
            {
                Codigo = aval.Codigo,
                CodigoColaboradorAvaliador = aval.CodigoColaboradorAvaliador,
                CodigoACECCA = aval.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola
            };

            return avalvm;
        }

        public async Task<List<AvaliacaoViewModel>> ListaAvaliacoes(int id, int esc)
        {

            var avaliacao = new List<AvaliacaoViewModel>();

            /*avaliacao = await (from av in db.TbAvaliacaos
                                   join atribcc in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas on av.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola equals atribcc.CodigoAtribuicaoColaboradorEscola
                                   join at in db.TbAtribuicaoColaboradorEscolas on atribcc.CodigoAtribuicaoColaboradorEscola equals at.Codigo
                                   join c in db.TbColaboradors on at.CodigoColaborador equals c.Codigo
                                   join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                                   join cc in db.TbComponenteCurriculars on atribcc.CodigoComponenteCurricular equals cc.Codigo
                                   join acc in db.TbCriterioComponenteCurriculars on cc.Codigo equals acc.CodigoComponenteCurricular
                                   join an in db.TbAnos on atribcc.CodigoAno equals an.Codigo
                                   join m in db.TbModalidades on an.CodigoModalidade equals m.Codigo
                                   where c.Ativo == 1 && at.Ativa == 1 && atribcc.Ativa == 1 && e.Ativa == 1 && cc.Ativa == 1 && acc.Ativa == 1 &&
                                   e.Codigo == esc && av.Finalizada != 1 && av.CodigoColaboradorAvaliador == id
                                   
                                   select new AvaliacaoViewModel
                                   {
                                       Codigo = av.Codigo,
                                       dataavaliacao = av.Datarealizacao,
                                       CodigoColaboradorAvaliador = av.CodigoColaboradorAvaliador,
                                       CodigoACECCA = atribcc.Codigo,
                                       NomeColaborador = c.Nome,
                                       escola = e.Escola,
                                       ano = $"{an.Ano + an.Turma + " - " + m.Modalidade + " - " + an.Periodo}",
                                       ccurric = cc.ComponenteCurricular,
                                       Finalizada = av.Finalizada
                                   }
                             ).ToListAsync();

            */


            avaliacao = await (from av in db.TbAvaliacaos
                               join atribcc in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                               on av.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola equals atribcc.Codigo
                               join at in db.TbAtribuicaoColaboradorEscolas on atribcc.CodigoAtribuicaoColaboradorEscola equals at.Codigo
                               join c in db.TbColaboradors on at.CodigoColaborador equals c.Codigo
                               join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                               join cc in db.TbComponenteCurriculars on atribcc.CodigoComponenteCurricular equals cc.Codigo
                               join ano in db.TbAnos on atribcc.CodigoAno equals ano.Codigo
                               join m in db.TbModalidades on ano.CodigoModalidade equals m.Codigo
                               where av.Finalizada != 1 && av.CodigoColaboradorAvaliador == id && at.CodigoEscola == esc && atribcc.Ativa == 1
                               orderby av.Datarealizacao ascending
                               select new AvaliacaoViewModel
                               {
                                   Codigo = av.Codigo,
                                   dataavaliacao = av.Datarealizacao,
                                   CodigoColaboradorAvaliador = av.CodigoColaboradorAvaliador,
                                   CodigoACECCA = atribcc.Codigo,
                                   NomeColaborador = c.Nome,
                                   escola = e.Escola,
                                   ano = $"{ano.Ano + ano.Turma + " - " + m.Modalidade + " - " + ano.Periodo}",
                                   ccurric = cc.ComponenteCurricular, 
                                   Finalizada = av.Finalizada
                               }

                               ).ToListAsync();

            
            return avaliacao;
        }

        public async Task<List<AvaliacaoViewModel>> ListaAvaliacoesFinalizadas(int esc)
        {
            var adm = await (from cl in db.TbColaboradors
                             join cg in db.TbCargos on cl.CodigoCargo equals cg.Codigo
                             join ae in db.TbAtribuicaoColaboradorEscolas on cl.Codigo equals ae.CodigoColaborador
                             join es in db.TbEscolas on ae.CodigoEscola equals es.Codigo
                             where es.Codigo == esc && cg.NiveldeAcesso > 0
                             select cl).ToListAsync();

            var avaliacao = await (from av in db.TbAvaliacaos
                               join atribcc in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                               on av.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola equals atribcc.Codigo
                               join at in db.TbAtribuicaoColaboradorEscolas on atribcc.CodigoAtribuicaoColaboradorEscola equals at.Codigo
                               join c in db.TbColaboradors on at.CodigoColaborador equals c.Codigo
                               join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                               join cc in db.TbComponenteCurriculars on atribcc.CodigoComponenteCurricular equals cc.Codigo
                               join ano in db.TbAnos on atribcc.CodigoAno equals ano.Codigo
                               join m in db.TbModalidades on ano.CodigoModalidade equals m.Codigo
                               where av.Finalizada != 0 && at.CodigoEscola == esc && atribcc.Ativa == 1
                               orderby av.Datarealizacao ascending
                               select new AvaliacaoViewModel
                               {
                                   Codigo = av.Codigo,
                                   dataavaliacao = av.Datarealizacao,
                                   CodigoColaboradorAvaliador = av.CodigoColaboradorAvaliador,
                                   CodigoACECCA = atribcc.Codigo,
                                   NomeColaborador = c.Nome,
                                   escola = e.Escola,
                                   ano = $"{ano.Ano + ano.Turma + " - " + m.Modalidade + " - " + ano.Periodo}",
                                   ccurric = cc.ComponenteCurricular,
                                   Finalizada = av.Finalizada
                               }

                               ).ToListAsync();


            foreach (var item in avaliacao)
            {
                item.NomeAvaliador = (from cadm in adm where cadm.Codigo == item.CodigoColaboradorAvaliador select cadm.Nome).First();
            }

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

        public async Task<TbEscola> MontarEscola(int id)
        {
            var tbescola = await db.TbEscolas
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbescola;
        }

        public async Task<List<AvaliacaoViewModel>> ListaAvaliacoesAtribuicao(int id, int esc, int atrib)
        {
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
                                  e.Codigo == esc && av.Finalizada != 1 && av.CodigoColaboradorAvaliador == id && av.CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola == atrib
                                  select new AvaliacaoViewModel
                                  {
                                      Codigo = av.Codigo,
                                      dataavaliacao = av.Datarealizacao,
                                      CodigoColaboradorAvaliador = av.CodigoColaboradorAvaliador,
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
    }
}
