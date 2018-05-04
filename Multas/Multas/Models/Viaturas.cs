using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultasProj.Models
{
    public class Viaturas
    {
        public Viaturas()
        {
            //necessario para se realizar o LazyLoading, juntamente com o virtual no objeto
            ListaDeMultas = new HashSet<Multas>();
        }

        [Key]
        public int ID { get; set; }

        //dados especificos da viatura

        public string Matricula { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }

        //dados do dono da viatura

        public string NomeDono { get; set; }

        public string MoradaDono { get; set; }

        public string CodPostalDono { get; set; }

        //complementar a informaçao sobre o relacionamento de uma Viatura com as Multas
        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}