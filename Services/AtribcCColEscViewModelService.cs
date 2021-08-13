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
    public class AtribcCColEscViewModelService:IAtribCCColEscViewModel
    {

        private dbContext db = new dbContext();

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public async Task<TbEscola> MontarEsc(int id)
        {
            var tbescola = await db.TbEscolas
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbescola;
        }

        public async Task<List<ColaboradorViewModel>> ListaProfessores(int escola)
        {
            var colaborador = await (from cg in db.TbCargos
                                     join c in db.TbColaboradors on cg.Codigo equals c.CodigoCargo
                                     join at in db.TbAtribuicaoColaboradorEscolas on c.Codigo equals at.CodigoColaborador
                                     join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                                     where cg.NiveldeAcesso == 0 && at.CodigoEscola == escola && c.Ativo == 1
                                     orderby c.Nome
                                     select new ColaboradorViewModel
                                     {
                                         Codigo = c.Codigo,
                                         Nome = c.Nome,
                                         Email = c.Email,
                                         Ativo = c.Ativo,
                                         NiveldeAcesso = cg.NiveldeAcesso,
                                         Cargo = cg.Cargo
                                     }).ToListAsync();
            return colaborador;
        }

        public SelectList ListaAno()
        {
            throw new NotImplementedException();
        }

        public SelectList ListaCCurricular()
        {
            throw new NotImplementedException();
        }

        public Task Atualizar(AtribCCColEscViewModel atribuicao)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(AtribCCColEscViewModel atribuicao)
        {
            throw new NotImplementedException();
        }

        public Task<AtribCCColEscViewModel> Detalhes(int id)
        {
            throw new NotImplementedException();
        }

        public Task Inserir(AtribCCColEscViewModel atribuicao)
        {
            throw new NotImplementedException();
        }

        public Task<List<AtribCCColEscViewModel>> ListaAtribuicao()
        {
            throw new NotImplementedException();
        }
    }
}
