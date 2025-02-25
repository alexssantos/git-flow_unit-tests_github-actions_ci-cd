﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Taskfy.Api.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Taskfy.Api.Controllers
{
    [Route("api/tarefa")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private static List<Tarefa> _minhasTarefas = new List<Tarefa>();


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_minhasTarefas);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var tarefa = _minhasTarefas.FirstOrDefault(item => item.Id == id);

            if (tarefa == default)
                return NotFound(tarefa);

            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TarefaDto tarefa)
        {
            if (ModelState.IsValid)
            {
                var novaTarefa = new Tarefa(tarefa.Titulo, tarefa.Descricao);
                _minhasTarefas.Add(novaTarefa);

                return StatusCode(StatusCodes.Status201Created, novaTarefa);
            }
            return Conflict();
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TarefaDto tarefa)
        {
            if (ModelState.IsValid)
            {
                var index = _minhasTarefas.FindIndex(item => item.Id == id);
                if (index >= 0)
                {
                    var itemAntigo = _minhasTarefas[index];
                    itemAntigo.AtualizaTarefa(tarefa.Titulo, tarefa.Descricao);

                    return Ok(itemAntigo);
                }
                return NotFound();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = _minhasTarefas.FindIndex(item => item.Id == id);
            if (index >= 0)
            {
                _minhasTarefas.RemoveAt(index);

                return Ok();
            }
            return NotFound();
        }
    }
}
