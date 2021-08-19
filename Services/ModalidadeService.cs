using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Services
{
    public class ModalidadeService : IModalidade
    {

        private dbContext db = new dbContext();

        public async Task Atualizar(TbModalidade modalidade)
        {
            db.TbModalidades.Update(modalidade);
            await db.SaveChangesAsync();
        }

        public async Task Deletar(TbModalidade modalidade)
        {
            db.TbModalidades.Remove(modalidade);
            await db.SaveChangesAsync();
        }

        public async Task<TbModalidade> Detalhes(int id)
        {
            var tbmodalidade = await db.TbModalidades.Where(e => e.Codigo == id).FirstAsync();
            return tbmodalidade;
        }

        public async Task Inserir(TbModalidade modalidade)
        {
            await db.TbModalidades.AddAsync(modalidade);
            await db.SaveChangesAsync();
        }

        public async Task<List<TbModalidade>> ListaModalidade()
        {
            return await db.TbModalidades.OrderBy(c => c.Codigo).ToListAsync();
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
               .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public bool TbModalidadeExists(int id)
        {
            return db.TbEstados.Any(e => e.Codigo == id);
        }
    }
}
