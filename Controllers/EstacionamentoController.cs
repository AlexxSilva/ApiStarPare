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
    public class EstacionamentoController : ControllerBase
    {
        private readonly IRepository<Carro> _repositoryCarro;
        private readonly IRepository<Estacionamento> _repositoryEstacionamento;

        public EstacionamentoController(IRepository<Carro> repositoryCarro,
            IRepository<Estacionamento> repositoryEstacionamento)
        {
              this._repositoryCarro = repositoryCarro;
              this._repositoryEstacionamento = repositoryEstacionamento;
        }


        /// <summary>
        /// Registra a entrada do veiculo no estacionamento
        /// </summary>
        /// <param name="CarroId">Parametro para registrar a entrada do veiculo</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso o registro de entrada seja feito com sucesso</response>

        [HttpPost("registrar-entrada/{CarroId}")]
        public async Task<IActionResult> RegistrarEntrada(int CarroId)
        {
            var carro = await _repositoryCarro.GetByIdAsync(CarroId);
            if (carro == null)
                return NotFound("Veículo não encontrado!");

            var estacionamento = new Estacionamento 
            {
                DataEntrada = DateTime.Now,
                CarroEstacionado = carro,
                CarroId = CarroId
            };

            await _repositoryEstacionamento.AddAsync(estacionamento);


            var estacionamentoDTO = new EstacionamentoDTO
            {
                DataEntrada = estacionamento.DataEntrada,
                Marca = carro.Marca,
                Modelo = carro.Modelo,
                Placa = carro.Placa
            };

            return Ok(new { 
                Message = "Entrada registrada!", 
                DataEntrada = estacionamento.DataEntrada }
            );

            //return CreatedAtAction(nameof(ListarEstacionados), new
            //{ id = estacionamentoDTO.Id }, estacionamentoDTO);
        }


        /// <summary>
        /// Registra a saida do veiculo no estacionamento
        /// </summary>
        /// <param name="CarroId">Parametro para registrar a saida do veiculo</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso o registro de entrada seja feito com sucesso</response>
        [HttpPut("registrar-saida/{CarroId}")]
        public async Task<IActionResult> RegistrarSaida(int CarroId)
        {
            var estacionamento = await _repositoryEstacionamento.FirstOrDefaultAsync
                (e => e.CarroId == CarroId && e.DataSaida == null);

            if (estacionamento == null)
                return NotFound("Veículo não encontrado ou já saiu.");

            estacionamento.DataSaida = DateTime.Now;
            await _repositoryEstacionamento.UpdateAsync(estacionamento);

            return Ok(new { Message = "Saída registrada!", DataSaida = estacionamento.DataSaida });

            //200 OK: A operação foi bem-sucedida e pode retornar um corpo de resposta com os dados atualizados, se necessário.
            //204 No Content: A operação foi bem-sucedida, mas não há conteúdo para retornar.
        }


        /// <summary>
        /// Consulta os veiculos estacionados
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso retorne veiculos estacionados</response>

        [HttpGet("estacionados")]
        public async Task<IActionResult> ListarEstacionados()
        {
            var estacionamentos = await _repositoryEstacionamento
                .GetAllWithIncludesAsync(e => e.DataSaida == null, e => e.CarroEstacionado);

            if (!estacionamentos.Any())
                return NotFound("Nenhum veículo estacionado no momento.");

            var estacionadosDTO = estacionamentos.Select(e => new EstacionamentoDTO
            {
                DataEntrada = e.DataEntrada,
                Marca = e.CarroEstacionado.Marca,
                Modelo = e.CarroEstacionado.Modelo,
                Placa = e.CarroEstacionado.Placa
            });

            return Ok(estacionadosDTO);
        }


        /// <summary>
        /// Consulta o historio de veiculos - retorno ainda em manutenção
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso retorne veiculos que já foram estacionados.</response>
        [HttpGet("historico")]
        public async Task<IActionResult> ListarHistorico()
        {
            var estacionamentos = await _repositoryEstacionamento
                .GetAllWithIncludesAsync(e => e.DataSaida != null, e => e.CarroEstacionado);

            if (!estacionamentos.Any())
                return NotFound("Nenhum veículo encontrado.");

            var historicoDTO = estacionamentos.Select(e => new EstacionamentoDTO
            {
                DataEntrada = e.DataEntrada,
                DataSaida = e.DataSaida,
                Marca = e.CarroEstacionado.Marca,
                Modelo = e.CarroEstacionado.Modelo,
                Placa = e.CarroEstacionado.Placa
            });

            return Ok(historicoDTO);
        }
    }
}
