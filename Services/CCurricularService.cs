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
    public class CCurricularService : ICCurricular
    {

        private dbContext db = new dbContext();

        public async Task Atualizar(TbComponenteCurricular componente)
        {
            db.TbComponenteCurriculars.Update(componente);
            await db.SaveChangesAsync();
        }

        public async Task Deletar(TbComponenteCurricular componente)
        {
            db.TbComponenteCurriculars.Remove(componente);
            await db.SaveChangesAsync();
        }

        public async Task<TbComponenteCurricular> Detalhes(int id)
        {
            var tbccurr = await db.TbComponenteCurriculars.Include(c=>c.CodigoModalidadeNavigation).Where(e => e.Codigo == id).FirstAsync();
            return tbccurr;
        }

        public async Task Inserir(TbComponenteCurricular componente)
        {
            await db.TbComponenteCurriculars.AddAsync(componente);
            await db.SaveChangesAsync();
        }

        public async Task<List<TbComponenteCurricular>> ListaComponente()
        {
            return await db.TbComponenteCurriculars.Include(c=> c.CodigoModalidadeNavigation).OrderBy(c => c.ComponenteCurricular).ToListAsync();
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public bool TbComponenteExists(int id)
        {
            return db.TbComponenteCurriculars.Any(e => e.Codigo == id);
        }
        public SelectList ListaModalidade()
        {
            var lista = new SelectList(db.TbModalidades, "Codigo", "Modalidade");

            return lista;
        }

        public SelectList ListaModalidadeUp(TbComponenteCurricular ccurricular)
        {
            var lista = new SelectList(db.TbModalidades, "Codigo", "Modalidade", ccurricular.CodigoModalidade);
            return lista;
        }
    }
}
