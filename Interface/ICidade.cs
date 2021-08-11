using AcompanhamentoDocente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcompanhamentoDocente.Interface
{
    interface ICidade
    {
        Task<List<TbCidade>> ListaCidade();
        Task<TbCidade> Detalhes(int id);
        Task Inserir(TbCidade cidade);
        Task Atualizar(TbCidade cidade);
        Task Deletar(TbCidade cidade);

        Task<TbColaborador> MontarAdmin(int id);
        SelectList ListaEstado();
        SelectList ListaEstadoUp(TbCidade cidade);
        bool TbCidadeExists(int id);

    }
}