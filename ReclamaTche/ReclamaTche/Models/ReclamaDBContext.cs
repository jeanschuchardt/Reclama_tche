using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReclamaTche.Models
{
    public class ReclamaDBContext : DbContext
    {
        public ReclamaDBContext() : base("ReclamaDBContext") { }
        public DbSet<Reclamacao> Reclamacoes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        //public System.Data.Entity.DbSet<ReclamaTche.ViewModels.Bairro> Bairroes { get; set; }
    }
}