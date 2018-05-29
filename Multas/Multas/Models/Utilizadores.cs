using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Utilizadores
    {

        /// <summary>
        /// os atributos q aqui vao ser adicionados
        /// serao adicionados à tabela dos utilizadores
        /// </summary>

        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "O {0} é de preenchimento obrigatorio")]
        [Display(Name = "Nome Proprio")]
        public string NomeProprio { get; set; }

        public string Apelido { get; set; }

        public DateTime DataNascimento { get; set; }

        public string NIF { get; set; }

        //*************************************************
        // o atributo seguinte vai criar uma chave forasteira para a tabela da 'autenticaçao'
        //*************************************************


        public string NomeRegistoDoUtilizador { get; set; }


    }
}