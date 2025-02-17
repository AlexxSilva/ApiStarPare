using ApiStarPare.Tools;
using System.ComponentModel.DataAnnotations;

namespace ApiStarPare.Models
{
    public class Carro 
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "O marca do carro é obrigatória")]
        [MaxLength(30, ErrorMessage = "A marca não pode exceder 30 caracteres")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "O modelo do carro é obrigatória")]
        [MaxLength(10, ErrorMessage = "A modelo não pode exceder 10 caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "A placa do carro é obrigatória")]
        [PlacaVeiculo]
        public string? Placa { get; set; }

        [Required(ErrorMessage = "O total de passageiros é obrigatório")]
        public int TotalPassageiros { get; set; }
    }
}
