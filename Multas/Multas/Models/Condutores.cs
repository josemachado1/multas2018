using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Condutores
    {
        public Condutores()
        {
            //necessario para se realizar o LazyLoading, juntamente com o virtual no objeto
            ListaDeMultas = new HashSet<Multas>();
        }

        [Key]
        public int ID { get; set; }//Chave primaria

        //dados proprios do condutor

        public string Nome { get; set; }

        public string BI { get; set; }

        public string Telemovel { get; set; }

        public DateTime DataNascimento { get; set; }

        //dados da carta de conduçao do condutor

        public string NumCartaConducao { get; set; }

        public string LocalEmissao { get; set; }

        public DateTime DataValidadeCarta { get; set; }

        //complementar a informaçao sobre o relacionamento de um Condutor com as Multas 
        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}