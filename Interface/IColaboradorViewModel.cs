using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface IColaboradorViewModel
    {
        Task<List<SelectListItem>> ListaEscolas(int CodigoAdministrador, ColaboradorViewModel colaborador);
        Task<List<SelectListItem>> ListaCargos(int CodigoAdministrador, ColaboradorViewModel Colaborador);
        Task<TbColaborador> MontarColaborador(int CodigoColaborador, int CodigoEscola, int CodigoAdministrador);
        Task <List<TbColaborador>> ColaboradorAtivo(int CodigoColaborador, int CodigoAdministrador);
        Task<List<TbColaborador>> ColaboradorInativo(int CodigoColaborador, int CodigoAdministrador);
        Task InserirColaborador(ColaboradorViewModel colaborador);
        Task AtualizarColaborador(ColaboradorViewModel colaborador);
        Task AtivarColaborador(ColaboradorViewModel colaborador);
        Task InativarColaborador(ColaboradorViewModel colaborador);
        Task RemoverColaborador(ColaboradorViewModel colaborador);

    }
}
