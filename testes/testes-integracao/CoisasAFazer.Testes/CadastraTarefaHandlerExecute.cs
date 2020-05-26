using CoisasAFazer.Core.Commands;
using CoisasAFazer.Core.Models;
using CoisasAFazer.Infrastructure;
using CoisasAFazer.Services.Handlers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.Extensions.Logging;

namespace CoisasAFazer.Testes
{
    public class CadastraTarefaHandlerExecute
    {
        delegate void CapturaMensagemLog(LogLevel level, EventId eventId, object state, Exception exception,
            Func<object, Exception, string> function);

        [Fact]
        public void AoExecutarTarefaDeveInserirNoBanco()
        {
            //arrange
            var comando = new CadastraTarefa("Estudar Mock", new Categoria("Estudo"), new DateTime(2019, 12, 31));

            var mockLog = new Mock<ILogger<CadastraTarefaHandler>>();

            var mock = new Mock<ILogger<CadastraTarefaHandler>>();

            var options = new DbContextOptionsBuilder<DbTarefasContext>()
                .UseInMemoryDatabase("DbTarefasContext")
                .Options;
            var contexto = new DbTarefasContext(options);
            var repositorio = new RepositorioTarefa(contexto);

            var handler = new CadastraTarefaHandler(repositorio, mockLog.Object);

            //act
            handler.Execute(comando);

            //assert
            var tarefa = repositorio.ObtemTarefas(t => t.Titulo == "Estudar Mock").FirstOrDefault();
            Assert.NotNull(tarefa);

            //criar comando

            //executar
        }

        [Fact]
        public void QuandoExceptionForLancadaResultadoIsSuccessDeveSerFalso()
        {
            //arrange
            var comando = new CadastraTarefa("Estudar Mock", new Categoria("Estudo"), new DateTime(2019, 12, 31));

            var mock = new Mock<IRepositorioTarefas>();

            var mockLog = new Mock<ILogger<CadastraTarefaHandler>>();

            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>()))
                    .Throws(new Exception("Houve um erro na inclusão de tarefas"));

            var repositorio = mock.Object;

            var handler = new CadastraTarefaHandler(repositorio, mockLog.Object);

            //act
            CommandResult resultado = handler.Execute(comando);

            //assert
            Assert.False(resultado.IsSuccess);
        }

        [Fact]
        public void AoExecutarTarefaDeveInserirDeveLogar()
        {
            //arrange
            var comando = new CadastraTarefa("Estudar Mock", new Categoria("Estudo"), new DateTime(2019, 12, 31));
 
            var mock = new Mock<IRepositorioTarefas>();

            var mockLog = new Mock<ILogger<CadastraTarefaHandler>>();

            LogLevel levelCapturado = LogLevel.Error;
            string msgCapturada = string.Empty;

            CapturaMensagemLog captura = (level, eventId, state, exception, func) =>
            {
                levelCapturado = level;
                msgCapturada = func(state, exception);
            };

            mockLog.Setup(l => l.Log(
                    It.IsAny<LogLevel>(),
                    It.IsAny<EventId>(),
                    It.IsAny<Object>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                )).Callback(captura);

            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>()))
                    .Throws(new Exception("Houve um erro na inclusão de tarefas"));

            var repositorio = mock.Object;

            var handler = new CadastraTarefaHandler(repositorio, mockLog.Object);

            Assert.Equal(LogLevel.Debug, levelCapturado);

            Assert.Contains("Estudar Mock", msgCapturada);
        }

        [Fact]
        public void QuandoExceptionForLancadaDeveLogarAMensagemDaExcessao()
        {
            //arrange
            var comando = new CadastraTarefa("Estudar Mock", new Categoria("Estudo"), new DateTime(2019, 12, 31));
            
            var mock = new Mock<IRepositorioTarefas>();

            var mockLog = new Mock<ILogger<CadastraTarefaHandler>>();

            var msgErro = "Houve um erro na inclusão de tarefas";
            var excecaoExperada = new Exception(msgErro);

            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>())).Throws(excecaoExperada);

            var repositorio = mock.Object;

            var handler = new CadastraTarefaHandler(repositorio, mockLog.Object);

            //act
            CommandResult resultado = handler.Execute(comando);

            //assert
            mockLog.Verify(v => 
                    v.Log(LogLevel.Error, //nivel do log
                    It.IsAny<EventId>(), //indentificar do evento
                    It.IsAny<Object>(), //objeto q sera logado
                    excecaoExperada,  //excecao q sera logada
                    It.IsAny<Func<Object, Exception, string>>()), //funcao q converte objeto+excecao em string
                Times.Once); //quantidade de vezes q eh executado

        }
    }
}
