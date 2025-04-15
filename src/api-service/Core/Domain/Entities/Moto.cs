namespace Domain.Entities
{
    public class Moto
    {
        public Moto(int ano, string? modelo, string? placa)
        {
            Ano = ano;
            Modelo = modelo;
            Placa = placa;
        }

        public Moto(int id, int ano, string? modelo, string? placa)
        {
            Id = id;
            Ano = ano;
            Modelo = modelo;
            Placa = placa;
        }

        public int Id { get; private set; }
        public int Ano { get; private set; }
        public string? Modelo { get; private set; }
        public string? Placa { get; private set; }
        
    }
}