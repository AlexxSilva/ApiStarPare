using ApiStarPare.Tools;
using System.ComponentModel.DataAnnotations;

namespace ApiStarPare.Dto
{
    public class CarroDTO
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public int TotalPassageiros { get; set; }
    }
}
