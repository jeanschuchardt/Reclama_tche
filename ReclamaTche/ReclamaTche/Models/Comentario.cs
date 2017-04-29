using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReclamaTche.Models
{
    public class Comentario
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public string Usuario { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:f}", ApplyFormatInEditMode = false)]
        public DateTime? ReleaseDate { get; set; }

        public int ReclamacaoID { get; set; }
        public virtual Reclamacao Reclamacao { get; set; }

        [Display(Name = "Imagem")]
        public byte[] ImageFile { get; set; }
        public string ImageMimeType { get; set; }
    }
}