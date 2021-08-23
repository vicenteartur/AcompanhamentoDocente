using AcompanhamentoDocente.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    

        interface ICriterioAvaliacao
        {

            Task<List<TbCriterioAvaliacao>> ListaCriterios();
            Task<TbCriterioAvaliacao> Detalhes(int id);
            Task Inserir(TbCriterioAvaliacao criterio);
            Task Atualizar(TbCriterioAvaliacao criterio);
            Task Deletar(TbCriterioAvaliacao criterio);
            SelectList Classificacao();
            SelectList ClassificacaoUp(int classificacao);
            Task<TbColaborador> MontarAdmin(int id);
            bool TbCriterioExists(int id);

        }

   
}
