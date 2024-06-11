using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barbearia.API.Models
{
    public class Agendamento
    {
        public int AgendamentoID { get; set; }
        public int ClienteID { get; set; }
        public DateTime DataHora { get; set; }
        public string Observacoes { get; set; }
        public int Status { get; set; }
    }
}