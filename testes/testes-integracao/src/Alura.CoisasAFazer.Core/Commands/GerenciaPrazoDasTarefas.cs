﻿using System;

namespace CoisasAFazer.Core.Commands
{
    /// <summary>
    /// Representa a necessidade de gerenciar o prazo das tarefas, verificando seu prazo e colocando as que expiraram em atraso. Um objeto dessa classe registra as informações necessárias para executar esse "caso de uso".
    /// </summary>
    public class GerenciaPrazoDasTarefas
    {
        public GerenciaPrazoDasTarefas(DateTime horario)
        {
            DataHoraAtual = horario;
        }

        public DateTime DataHoraAtual { get; }
    }
}
