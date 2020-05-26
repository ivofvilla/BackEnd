﻿using Microsoft.AspNetCore.Mvc;
using CoisasAFazer.WebApp.Models;
using CoisasAFazer.Core.Commands;
using CoisasAFazer.Services.Handlers;
using CoisasAFazer.Infrastructure;
using Microsoft.Extensions.Logging;

namespace CoisasAFazer.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        IRepositorioTarefas repo;
        ILogger<CadastraTarefaHandler> log;

        public TarefasController(IRepositorioTarefas repo, ILogger<CadastraTarefaHandler> log)
        {
            this.repo = repo;
            this.log = log;
        }

        [HttpPost]
        public IActionResult EndpointCadastraTarefa(CadastraTarefaVM model)
        {
            var cmdObtemCateg = new ObtemCategoriaPorId(model.IdCategoria);
            var categoria = new ObtemCategoriaPorIdHandler(repo).Execute(cmdObtemCateg);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }


            var comando = new CadastraTarefa(model.Titulo, categoria, model.Prazo);
            var handler = new CadastraTarefaHandler(repo, log);
            var resultado = handler.Execute(comando);

            if(resultado.IsSuccess) return Ok();

            return StatusCode(500);
        }
    }
}