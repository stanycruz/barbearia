using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Barbearia.API.Data;
using Barbearia.API.DTO;
using Barbearia.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Barbearia.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public AgendamentoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var agendamentos = await _dbContext
                .Agendamentos
                .Include(c => c.Cliente)
                .Include(s => s.Servicos)
                .ToListAsync();

            if (agendamentos.Count == 0)
            {
                return NotFound();
            }

            return Ok(agendamentos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Agendamento>> GetById(int id)
        {
            var agendamento = await _dbContext.Agendamentos.FindAsync(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return Ok(agendamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AgendamentoDTO agendamentoDTO)
        {
            if (ModelState.IsValid)
            {
                var agendamento = new Agendamento
                {
                    ClienteID = agendamentoDTO.ClienteID,
                    Observacoes = agendamentoDTO.Observacoes,
                    DataHora = agendamentoDTO.DataHora,
                    Status = agendamentoDTO.Status,
                    Servicos = new List<Servico>()
                };
                agendamento.Cliente = _dbContext.Clientes.Find(agendamento.ClienteID);

                foreach (var item in agendamentoDTO.Servicos)
                {
                    var servico = _dbContext.Servicos.Find(item.Id);

                    if (servico == null)
                    {
                        return BadRequest();
                    }

                    agendamento.Servicos.Add(servico);
                }

                _dbContext.Agendamentos.Add(agendamento);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = agendamento.ClienteID }, agendamento);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, AgendamentoDTO agendamentoDTO)
        {
            if (id != agendamentoDTO.AgendamentoID)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                var agendamento = _dbContext
                    .Agendamentos
                    .Include(s => s.Servicos)
                    .Include(c => c.Cliente)
                    .FirstOrDefault(x => x.AgendamentoID == agendamentoDTO.AgendamentoID);

                if (agendamento == null)
                {
                    return NotFound();
                }

                agendamento.ClienteID = agendamentoDTO.ClienteID;
                agendamento.Observacoes = agendamentoDTO.Observacoes;
                agendamento.DataHora = agendamentoDTO.DataHora;
                agendamento.Status = agendamentoDTO.Status;
                agendamento.Servicos = new List<Servico>();
                agendamento.Cliente = _dbContext.Clientes.Find(agendamento.ClienteID);

                foreach (var item in agendamentoDTO.Servicos)
                {
                    var servico = _dbContext.Servicos.Find(item.Id);

                    if (servico == null)
                    {
                        return BadRequest();
                    }

                    agendamento.Servicos.Add(servico);
                }

                _dbContext.Agendamentos.Update(agendamento);
                await _dbContext.SaveChangesAsync();
                return Ok(agendamento);
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var agendamento = await _dbContext
                .Agendamentos
                .Include(s => s.Servicos)
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(x => x.AgendamentoID == id);

            if (agendamento == null)
            {
                return NotFound();
            }

            _dbContext.Agendamentos.Remove(agendamento);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}