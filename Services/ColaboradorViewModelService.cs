using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Services
{
    public class ColaboradorViewModelService : IColaboradorViewModel
    {

        private dbContext db = new dbContext();

        public async Task<List<TbCargo>> ListaCargos(int CodigoAdministrador)
        {
            var colab = await (from col in db.TbColaboradors
                               join c in db.TbCargos on col.CodigoCargo equals c.Codigo
                               select c.NiveldeAcesso).FirstAsync();

            var lista = await db.TbCargos.Where(cg => cg.NiveldeAcesso <= colab).ToListAsync<TbCargo>();

            return lista;
        }

        public Task<List<TbEscola>> ListaEscolas(int CodigoAdministrador)
        {
            throw new NotImplementedException();
        }

        public Task<TbColaborador> MontarColaborador(int CodigoColaborador, int CodigoEscola, int CodigoAdministrador)
        {
            throw new NotImplementedException();
        }

        public Task RemoverColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }

        public Task<List<TbColaborador>> ColaboradorAtivo(int CodigoColaborador, int CodigoAdministrador)
        {
            throw new NotImplementedException();
        }

        public Task<List<TbColaborador>> ColaboradorInativo(int CodigoColaborador, int CodigoAdministrador)
        {
            throw new NotImplementedException();
        }

        public Task InserirColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }

        public Task AtualizarColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }
        public Task AtivarColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }

        public Task InativarColaborador(ColaboradorViewModel colaborador)
        {
            throw new NotImplementedException();
        }

    }
}
