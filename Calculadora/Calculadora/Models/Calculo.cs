using SQLite;

namespace Calculadora.Models
{
    public class Calculo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Expressao { get; set; }
        public string Resultado { get; set; }
    }
}
