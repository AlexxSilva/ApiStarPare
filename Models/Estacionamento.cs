﻿using System.ComponentModel.DataAnnotations;

namespace ApiStarPare.Models
{
    public class Estacionamento
    {
        [Key]
        public int Id { get; set; }

         [Required(ErrorMessage = "O marca do carro é obrigatória")]
        public DateTime DataEntrada { get; set; } = DateTime.Now;

        public DateTime? DataSaida { get; set; }
        public int? CarroId { get; set; }
        public Carro? CarroEstacionado { get; set; }
    }
}
