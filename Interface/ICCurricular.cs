using AcompanhamentoDocente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface ICCurricular
    {

        Task<List<TbComponenteCurricular>> ListaComponente();
        Task<TbComponenteCurricular> Detalhes(int id);
        Task Inserir(TbComponenteCurricular componente);
        Task Atualizar(TbComponenteCurricular componente);
        Task Deletar(TbComponenteCurricular componente);

        Task<TbColaborador> MontarAdmin(int id);
        bool TbComponenteExists(int id);

    }
}
