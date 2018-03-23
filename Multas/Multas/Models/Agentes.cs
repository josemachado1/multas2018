using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Multas.Models
{
    public class Agentes
    {
        //nao é obrigatorio escrever esta linha, é apenas um construtor
        public Agentes() {
            //necessario para se realizar o LazyLoading, juntamente com o virtual no objeto
            ListaDeMultas = new HashSet<Multas>();
        }

        [Key]
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Ftotografia { get; set; }

        public string Esquadra { get; set; }

        //complementar a informaçao sobre o relacionamento de um Agente com as Multas por ele 'passadas'
        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}