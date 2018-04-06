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

        [Required(ErrorMessage ="o {0} é de preenchimento obrigatório!")] // o atributo nome é de preenchimento obrigatorio
        [RegularExpression("[A-ZÂÍ][a-záéíóúãõàèìòùâêîôûç.]+(( | de | da | dos | d' |-)[A-ZÂÍ][a-záéíóúãõàèìòùâêîôûç]+){1,3}", 
            ErrorMessage ="o nome apenas aceita letras. Cada palavra começa por uma maiscula, seguida de minusculas...")]
        [StringLength(40)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "o {0} é de preenchimento obrigatório!")]
        public string Fotografia { get; set; }

        [Required(ErrorMessage = "o {0} é de preenchimento obrigatório!")]
        [RegularExpression("[A-z 0-9-]+",ErrorMessage ="Escreva um nome aceitavel...")]
        public string Esquadra { get; set; }

        //complementar a informaçao sobre o relacionamento de um Agente com as Multas por ele 'passadas'
        public virtual ICollection<Multas> ListaDeMultas { get; set; }
    }
}