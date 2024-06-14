using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Barbearia.API.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Favor colocar um nome para o serviço")]
        [DisplayName("Nome Serviço")]
        public string? NomeServico { get; set; }

        [DisplayName("Descrição")]
        public string? Descricao { get; set; }

        [Range(10, 15, ErrorMessage = "A duração deve ser de 10 até 15 minutos")]
        public int DuracaoMin { get; set; }

        [Range(0.01, 100.00, ErrorMessage = "O preço deve ser entre 0.01 até 100.00")]
        public double Preco { get; set; }
    }
}