using ApiStarPare.Data;
using ApiStarPare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStarPare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstacionamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstacionamentoController(AppDbContext context)
        {
              this._context = context;
        }

        [HttpPost("registrar-entrada/{CarroId}")]
        public async Task<IActionResult> RegistrarEntrada(int CarroId)
        {
            var carro = await _context.Carros.FindAsync(CarroId);
            if (carro == null)
                return NotFound("Veículo não encontrado!");

            var estacionamento = new Estacionamento 
            {
                DataEntrada = DateTime.Now,
                CarroEstacionado = carro,
                CarroId = CarroId
            };

            _context.Estacionamentos.Add(estacionamento);
            await _context.SaveChangesAsync();

            return Ok(new { 
                Message = "Entrada registrada!", 
                DataEntrada = estacionamento.DataEntrada }
            );
        }

        [HttpPut("registrar-saida/{CarroId}")]
        public async Task<IActionResult> RegistrarSaida(int CarroId)
        {
            var estacionamento = await _context.Estacionamentos
          .Where(e => e.CarroId == CarroId  && e.DataSaida == null)
          .FirstOrDefaultAsync();

            if (estacionamento == null)
                return NotFound("Veículo não encontrado ou já saiu.");

            estacionamento.DataSaida = DateTime.Now;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Saída registrada!", DataSaida = estacionamento.DataSaida });

        }

        [HttpGet("estacionados")]
        public async Task<IActionResult> ListarEstacionados()
        {
            var estacionamentos = await _context.Estacionamentos.Where(
                e => e.DataSaida == null).Include(e => e.CarroEstacionado).ToListAsync();

            if (estacionamentos.Count == 0)
                return NotFound("Nenhum veículo estacionado no momento.");

            return Ok(estacionamentos);
        }

        [HttpGet("historico")]
        public async Task<IActionResult> ListarHistorico()
        {
            var estacionamentos = await _context.Estacionamentos.Where(
                e => e.DataSaida != null).Include(e => e.CarroEstacionado).ToListAsync();

            if (estacionamentos.Count == 0)
                return NotFound("Nenhum veículo encontrado.");

            return Ok(estacionamentos);
        }
    }
}
