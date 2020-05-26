using CoisasAFazer.Core.Models;
using CoisasAFazer.Infrastructure;
using CoisasAFazer.Services.Handlers;
using CoisasAFazer.WebApp.Controllers;
using CoisasAFazer.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoisasAFazer.Testes
{
    public class TarefasControllerEndPointCadastraTarefa
    {
        [Fact]
        public void DadaTarefaComInformacoesValidasDeveRetornar200()
        {
            //arranjo
            var mockLog = new Mock<ILogger<CadastraTarefaHandler>>();

            var options = new DbContextOptionsBuilder<DbTarefasContext>()
                            .UseInMemoryDatabase("DbTarefasContext")
                            .Options;

            var contexto = new DbTarefasContext(options);
            contexto.Categorias.Add(new Categoria(20, "Estudo"));
            contexto.SaveChanges();

            var repository = new RepositorioTarefa(contexto);
            
            var controlador = new TarefasController(repository, mockLog.Object);
            var model = new CadastraTarefaVM();

            model.IdCategoria = 20;
            model.Titulo = "Estudar Xunit";
            model.Prazo = new DateTime(2019,12,31);

            //ato
            var retorno = controlador.EndpointCadastraTarefa(model);

            //assert
            Assert.IsType<OkResult>(retorno);
        }

        [Fact]
        public void QuandoExcecaoForlancadaStatusCodeRetorna500()
        {
            //arranjo
            var mockLog = new Mock<ILogger<CadastraTarefaHandler>>();

            var mock = new Mock<IRepositorioTarefas>();
            mock.Setup(r => r.ObtemCategoriaPorId(20)).Returns(new Categoria(20, "Estudo"));
            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>())).Throws(new Exception("erro"));

            var repository = mock.Object;
            
            var controlador = new TarefasController(repository, mockLog.Object);
            var model = new CadastraTarefaVM();

            model.IdCategoria = 20;
            model.Titulo = "Estudar Xunit";
            model.Prazo = new DateTime(2019, 12, 31);

            //ato
            var retorno = controlador.EndpointCadastraTarefa(model);

            var status = (retorno as StatusCodeResult).StatusCode;

            //assert
            Assert.IsType<StatusCodeResult>(retorno);
            Assert.Equal(500, status);
        }
    }
}
