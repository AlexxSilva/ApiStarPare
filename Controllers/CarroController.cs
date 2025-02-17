using ApiStarPare.Data;
using ApiStarPare.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStarPare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarroController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CriarVeiculo([FromBody] Carro carro)
        {
            if (carro == null)
            {
                return NotFound();
            }
            else
            {
                _context.Carros.Add(carro);
                await _context.SaveChangesAsync();
                return (CreatedAtAction(nameof(Carro), carro));
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperarCarroPorId(int id)
        { 
            var carro =  await _context.Carros.FirstOrDefaultAsync(c => c.Id == id);
            if (carro == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(carro); 
            }
        }

        [HttpGet("{Placa}")]
        public async Task<IActionResult> RecuperarCarroPorPelaPlaca(string placa)
        {
            var carro = await _context.Carros.FirstOrDefaultAsync(c => c.Placa == placa);
            if (carro == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(carro);
            }
        }

    }
}
