using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiStarPare.Models
{
    public class Estacionamento
    {
        [Key]
        public int Id { get; set; }

         [Required(ErrorMessage = "O marca do carro é obrigatória")]
        [Column(TypeName = "datetime")]
        public DateTime DataEntrada { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        public DateTime? DataSaida { get; set; }
        public int? CarroId { get; set; }
        public Carro? CarroEstacionado { get; set; }
    }
}
