using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MultasProj.Models
{
    public class Multas
    {
        [Key]
        public int ID { get; set; }

        public string Infracao { get; set; }

        public string LocalDaMulta { get; set; }

        public double ValorMulta { get; set; }

        public DateTime DataDaMulta { get; set; }

        //**********************************************************************************
        //construçao das chaves forasteiras
        //**********************************************************************************

        //FK Agentes
        //SQL:ForeginKey NomeAtributoQueÉFK references Tabela(pkDaTabela)
        [ForeignKey("Agente")]
        public int AgenteFK { get; set; }
        public virtual Agentes Agente { get; set; }

        //FK Viaturas
        [ForeignKey("Viaturas")]
        public int ViaturaFK { get; set; }
        public virtual Viaturas Viaturas { get; set; }

        //FK Condutores
        [ForeignKey("Condutor")]
        public int CondutorFK { get; set; }
        public virtual Condutores Condutor { get; set; }


    }
}
 