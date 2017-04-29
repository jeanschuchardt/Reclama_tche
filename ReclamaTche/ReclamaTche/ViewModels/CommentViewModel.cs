using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReclamaTche.ViewModels
{
    public class ComentarioViewModel
    {
        //Represents a post id
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Author { get; set; }
        [Required]
        [AllowHtml]
        public string Body { get; set; }
        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
    }
}