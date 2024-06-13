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
    public class ServicoController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ServicoController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var servicos = await _dbContext.Servicos.ToListAsync();
            return Ok(servicos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Servico>> GetById(int id)
        {
            var servico = await _dbContext.Servicos.FindAsync(id);

            if (servico == null)
            {
                return NotFound();
            }
            
            return Ok(servico);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServicoDTO servicoDTO)
        {
            if (ModelState.IsValid)
            {
                var servico = new Servico
                {
                    NomeServico = servicoDTO.NomeServico,
                    Descricao = servicoDTO.Descricao,
                    DuracaoMin = servicoDTO.DuracaoMin,
                    Preco = servicoDTO.Preco
                };

                _dbContext.Servicos.Add(servico);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = servico.Id }, servico);
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, ServicoDTO servicoDTO)
        {
            if (id != servicoDTO.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var servico = _dbContext.Servicos.Find(servicoDTO.Id);

                if (servico == null)
                {
                    return NotFound();
                }

                servico.NomeServico = servicoDTO.NomeServico;
                servico.Descricao = servicoDTO.Descricao;
                servico.DuracaoMin = servicoDTO.DuracaoMin;
                servico.Preco = servicoDTO.Preco;

                _dbContext.Servicos.Update(servico);
                await _dbContext.SaveChangesAsync();
                return Ok(servico);
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var servico = await _dbContext.Servicos.FindAsync(id);

            if (servico == null)
            {
                return NotFound();
            }

            _dbContext.Servicos.Remove(servico);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}