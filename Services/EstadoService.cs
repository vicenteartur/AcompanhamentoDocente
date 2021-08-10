using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using IdentityModel.Client;
using Microsoft.Data.SqlClient;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;

namespace AcompanhamentoDocente.Services
{
    public class EstadoService : IEstado
    {

        private dbContext db = new dbContext();

        public async Task Atualizar(TbEstado estado)
        {
            db.TbEstados.Update(estado);
            await db.SaveChangesAsync();
        }

        public async Task Deletar(TbEstado estado)
        {
            db.TbEstados.Remove(estado);
            await db.SaveChangesAsync();
        }

        public async Task<TbEstado> Detalhes(int id)
        {
            var tbestado = await db.TbEstados.Where(e => e.Codigo == id).FirstAsync();
            return tbestado;
        }

        public async Task Inserir(TbEstado estado)
        {
            await db.TbEstados.AddAsync(estado);
            await db.SaveChangesAsync();
        }

        public async Task<List<TbEstado>> ListaEstado()
        {
            return await db.TbEstados.OrderBy(c => c.Sigla).ToListAsync();
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public bool TbEstadoExists(int id)
        {
            return db.TbEstados.Any(e => e.Codigo == id);
        }
    }
}
