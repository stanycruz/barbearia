using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barbearia.API.Models
{
    public class Agendamento
    {
        [Key]
        public int AgendamentoID { get; set; }

        [Required]
        public int ClienteID { get; set; }

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public string? Observacoes { get; set; }

        [Required]
        public int Status { get; set; }
    }
}