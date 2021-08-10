﻿using AcompanhamentoDocente.Interface;
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
    public class AnoService : IAno
    {

        private dbContext db = new dbContext();


        public async void Create(TbAno tbAno)
        {
            await db.TbAnos.AddAsync(tbAno);
            await db.SaveChangesAsync();
        }

        public async void Delete(TbAno ano)
        {
            db.TbAnos.Remove(ano);
           await db.SaveChangesAsync();
        }

        public async Task<TbAno> Details(int? id)
        {
            var tbAno = await db.TbAnos
                .FirstOrDefaultAsync(m => m.Codigo == id);


            return tbAno;
        }

        public async void Edit(int id, TbAno tbAno)
        {
            db.TbAnos.Update(tbAno);
            await db.SaveChangesAsync();
        }
        
        public async Task<List<TbAno>> Index()
        {
            return await db.TbAnos.OrderBy(c=>c.Ano).ToListAsync();
        }

        public bool TbAnoExists(int id)
        {
            return db.TbAnos.Any(e => e.Codigo == id);
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

    }
}
