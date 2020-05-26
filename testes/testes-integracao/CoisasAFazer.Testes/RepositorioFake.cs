using CoisasAFazer.Core.Models;
using CoisasAFazer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoisasAFazer.Testes
{
    public class RepositorioFake : IRepositorioTarefas
    {
        private static List<Tarefa> ListaTarefas = new List<Tarefa>();

        public void AtualizarTarefas(params Tarefa[] tarefas)
        {
            throw new NotImplementedException();
        }

        public void ExcluirTarefas(params Tarefa[] tarefas)
        {
            throw new NotImplementedException();
        }

        public void IncluirTarefas(params Tarefa[] tarefas)
        {
            throw new Exception("Houve um erro ao incluir as tarefas");
            ListaTarefas.AddRange(tarefas);
        }

        public Categoria ObtemCategoriaPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tarefa> ObtemTarefas(Func<Tarefa, bool> filtro)
        {
            return ListaTarefas.Where(filtro);
        }
    }
}
