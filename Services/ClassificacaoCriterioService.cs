using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Services
{
    public class ClassificacaoCriterioService : IClassificacaoCriterio
    {

        private dbContext db = new dbContext();

        public async Task Atualizar(TbClassificacaoCriterio classificacao)
        {
            db.TbClassificacaoCriterios.Update(classificacao);
            await db.SaveChangesAsync();
        }

        public async Task Deletar(TbClassificacaoCriterio classificacao)
        {
            db.TbClassificacaoCriterios.Remove(classificacao);
            await db.SaveChangesAsync();
        }

        public async Task<TbClassificacaoCriterio> Detalhes(int classificacao)
        {
            var tbclassificacao = await db.TbClassificacaoCriterios.Where(e => e.Codigo == classificacao).FirstAsync();
            return tbclassificacao;
        }

        public async Task Inserir(TbClassificacaoCriterio classificacao)
        {
            await db.TbClassificacaoCriterios.AddAsync(classificacao);
            await db.SaveChangesAsync();
        }

        public async Task<List<TbClassificacaoCriterio>> ListaClassificacao()
        {
            return await db.TbClassificacaoCriterios.OrderBy(c => c.Classificacao).ToListAsync();
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public bool TbClassificacaoExists(int id)
        {
            return db.TbClassificacaoCriterios.Any(e => e.Codigo == id);
        }
    }
}
