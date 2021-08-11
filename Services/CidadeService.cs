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
    public class CidadeService : ICidade
    {

        private dbContext db = new dbContext();


        public async Task Atualizar(TbCidade cidade)
        {
            db.TbCidades.Update(cidade);
            await db.SaveChangesAsync();
        }

        public async Task Deletar(TbCidade cidade)
        {
            db.TbCidades.Remove(cidade);
            await db.SaveChangesAsync();
        }

        public async Task<TbCidade> Detalhes(int id)
        {
            var tbcidade = await db.TbCidades.Where(e => e.Codigo == id).FirstAsync();
            return tbcidade;
        }

        public async Task Inserir(TbCidade cidade)
        {
            await db.TbCidades.AddAsync(cidade);
            await db.SaveChangesAsync();
        }

        public async Task<List<TbCidade>> ListaCidade()
        {
            return await db.TbCidades.OrderBy(c => c.Cidade).ToListAsync();
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public bool TbCidadeExists(int id)
        {
            return db.TbEstados.Any(e => e.Codigo == id);
        }

        public SelectList ListaEstado()
        {
            var lista = new SelectList( db.TbEstados, "Codigo", "Estado");

            return lista;
        }

        public SelectList ListaEstadoUp(TbCidade cidade)
        {
            return new SelectList(db.TbEstados, "Codigo", "Estado", cidade.CodigoEstado);
        }
    }
}
