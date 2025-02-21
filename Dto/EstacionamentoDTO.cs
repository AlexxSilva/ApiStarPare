namespace ApiStarPare.Dto
{
    public class EstacionamentoDTO
    {
        public DateTime DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
    }
}
