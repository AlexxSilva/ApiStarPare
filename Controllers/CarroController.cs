using ApiStarPare.Data;
using ApiStarPare.Dto;
using ApiStarPare.Models;
using ApiStarPare.Repositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiStarPare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly IRepository<Carro> _repository;

        public CarroController(IRepository<Carro> repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Adiciona um carro que irá estacionar
        /// </summary>
        /// <param name="carro">Objeto com os campos necessários para criar o carro</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso inserção seja feita com sucesso</response>

        [HttpPost]
        public async Task<IActionResult> CriarCarro([FromBody] CarroDTO carroDto)
        {
            if (carroDto == null)
            {
                return NotFound();
            }
            else
            {
                var carro = new Carro
                {
                    Marca = carroDto.Marca,
                    Modelo = carroDto.Modelo,
                    Placa = carroDto.Placa,
                    TotalPassageiros = carroDto.TotalPassageiros
                };


                await _repository.AddAsync(carro);
                return CreatedAtAction(nameof(RecuperarCarroPorId),new { Id = carro.Id }, carro);
            }

        }


        /// <summary>
        /// Retorna o carro informando o Id
        /// </summary>
        /// <param name="id">Parametro para retornar o carro</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o retorno seja feito com sucesso</response>

        [HttpGet("{id:int}")]
        public async Task<IActionResult> RecuperarCarroPorId(int id)
        { 
            var carro =  await _repository.GetByIdAsync(id);
            if (carro == null)
            {
                return NotFound();
            }

            var carroDto = new CarroDTO
            {
                Marca = carro.Marca,
                Modelo = carro.Modelo,
                Placa = carro.Placa,
                TotalPassageiros = carro.TotalPassageiros
            };

            return Ok(carroDto);
        }


        /// <summary>
        /// Retorna o carro informando pela placa do veiculo
        /// </summary>
        /// <param name="placa">Parametro para retornar o carro</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o retorno seja feito com sucesso</response>

        [HttpGet("placa/{Placa}")]
        public async Task<IActionResult> RecuperarCarroPorPelaPlaca(string placa)
        {
            var carro = await _repository.FirstOrDefaultAsync(c => c.Placa == placa);
            if (carro == null)
            {
                return NotFound();
            }
            var carroDto = new CarroDTO
            {
                Marca = carro.Marca,
                Modelo = carro.Modelo,
                Placa = carro.Placa,
                TotalPassageiros = carro.TotalPassageiros
            };

            return Ok(carroDto);
        }

    }
}
