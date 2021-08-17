using AcompanhamentoDocente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface IClassificacaoCriterio
    {
        Task<List<TbClassificacaoCriterio>> ListaClassificacao();
        Task<TbClassificacaoCriterio> Detalhes(int id);
        Task Inserir(TbClassificacaoCriterio classificacao);
        Task Atualizar(TbClassificacaoCriterio classificacao);
        Task Deletar(TbClassificacaoCriterio classificacao);

        Task<TbColaborador> MontarAdmin(int id);
        bool TbClassificacaoExists(int id);
    }
}
