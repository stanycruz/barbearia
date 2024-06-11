using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbearia.API.Models
{
    public class Servico
    {
        public int Id { get; set; }
        public string NomeServico { get; set; }
        public string Descricao { get; set; }
        public int DuracaoMin { get; set; }
        public double Preco { get; set; }
    }
}