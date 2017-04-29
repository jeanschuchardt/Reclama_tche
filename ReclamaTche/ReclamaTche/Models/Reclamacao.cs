using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReclamaTche.Models
{
    public class Reclamacao
    {
        public int ReclamacaoID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Data de inserção")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:D}", ApplyFormatInEditMode = false)]
        public DateTime? ReleaseDate { get; set; }

        [StringLength(6000, MinimumLength = 3)]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaID { get; set; }
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Comentario> Comentarios { get; set; }

        public string Usuario { get; set; }

        [Display(Name = "Imagem")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }

    }

    public enum Status
    {
        ABERTO, ENCERRADO, RESOLVIDO
    }
}