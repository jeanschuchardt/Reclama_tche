using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReclamaTche.Models
{
    public class Categoria
    {
        [Display(Name = "Categoria")]
        public int CategoriaID { get; set; }

        [Required]
        [Display(Name = "Categoria")]
        public string Nome { get; set; }

        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        public virtual ICollection<Reclamacao> Reclamacoes { get; set; }
    }
}