using AcompanhamentoDocente.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface IModalidade
    {

        Task<List<TbModalidade>> ListaModalidade();
        Task<TbModalidade> Detalhes(int id);
        Task Inserir(TbModalidade modalidade);
        Task Atualizar(TbModalidade modalidade);
        Task Deletar(TbModalidade modalidade);

        Task<TbColaborador> MontarAdmin(int id);
        bool TbModalidadeExists(int id);

    }
}
