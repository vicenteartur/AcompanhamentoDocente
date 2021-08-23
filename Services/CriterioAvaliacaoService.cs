using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Services
{
    public class CriterioAvaliacaoService :ICriterioAvaliacao
    {
        private dbContext db = new dbContext();

        public async Task Atualizar(TbCriterioAvaliacao criterio)
        {
            db.TbCriterioAvaliacaos.Update(criterio);
            await db.SaveChangesAsync();
        }

        public SelectList Classificacao()
        {
            var lista = new SelectList(db.TbClassificacaoCriterios, "Codigo", "Classificacao");

            return lista;
        }

        public SelectList ClassificacaoUp(int classificacao)
        {
            var lista = new SelectList(db.TbClassificacaoCriterios, "Codigo", "Classificacao",classificacao);

            return lista;
        }

        public async Task Deletar(TbCriterioAvaliacao criterio)
        {
            db.TbCriterioAvaliacaos.Remove(criterio);
            await db.SaveChangesAsync();
        }

        public async Task<TbCriterioAvaliacao> Detalhes(int id)
        {
            var tbcriterio = await db.TbCriterioAvaliacaos.Where(e => e.Codigo == id).FirstAsync();
            return tbcriterio;
        }

        public async Task Inserir(TbCriterioAvaliacao criterio)
        {
            await db.TbCriterioAvaliacaos.AddAsync(criterio);
            await db.SaveChangesAsync();
        }

        public async Task<List<TbCriterioAvaliacao>> ListaCriterios()
        {
            return await db.TbCriterioAvaliacaos.Include(c => c.CodigoClassificacaoCriterioNavigation).OrderBy(c => c.Criterio).ToListAsync();
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public bool TbCriterioExists(int id)
        {
            return db.TbCriterioAvaliacaos.Any(e => e.Codigo == id);
        }
    }
}
