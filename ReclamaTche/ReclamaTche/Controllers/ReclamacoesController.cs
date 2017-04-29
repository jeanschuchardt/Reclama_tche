using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReclamaTche.Models;
using ReclamaTche.ViewModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ReclamaTche.Controllers
{
    public class ReclamacoesController : Controller
    {
        private ReclamaDBContext db = new ReclamaDBContext();
        private ApplicationDbContext context = new ApplicationDbContext();
        // GET: Reclamacoes
        public ViewResult Index(string searchString, string sortOrder, int? SelectedCategory, DateTime? fromDate, DateTime? toDate, bool? aberto, bool? resolvido, bool? encerrado, string Bairro)
        {
            var categorias = db.Categorias.OrderBy(c => c.Nome).ToList();
            ViewBag.SelectedCategory = new SelectList(categorias, "CategoriaID", "Nome", SelectedCategory);
            int categoryID = SelectedCategory.GetValueOrDefault();

            var bai = db.Reclamacoes.OrderBy(s => s.Bairro);
            HashSet<string> bairros = new HashSet<string>();
            foreach (var b in bai)
            {
                bairros.Add(b.Bairro);
            }

            ViewBag.Bairro = bairros.ToList();

            var reclamacoes = db.Reclamacoes
                .Where(c => !SelectedCategory.HasValue || c.CategoriaID == categoryID);

            //var reclamacoes = db.Reclamacoes.Include(r => r.Categoria);

            if (aberto != null && (bool)!aberto)
            {
                reclamacoes = reclamacoes.Where(s => s.Status != Status.ABERTO);
            }

            if (resolvido != null && (bool)!resolvido)
            {
                reclamacoes = reclamacoes.Where(s => s.Status != Status.RESOLVIDO);
            }

            if (encerrado != null && (bool)!encerrado)
            {
                reclamacoes = reclamacoes.Where(s => s.Status != Status.ENCERRADO);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                reclamacoes = reclamacoes.Where(s => s.Nome.Contains(searchString) || s.Descricao.Contains(searchString));
            }

            ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_asc" : "Rating";

            switch (sortOrder)
            {
                case "Rating":
                    reclamacoes = reclamacoes.OrderBy(s => s.ReleaseDate);
                    break;
                case "rating_asc":
                    reclamacoes = reclamacoes.OrderByDescending(s => s.ReleaseDate);
                    break;
            }

            if (fromDate <= toDate)
            {
                if (fromDate != null)
                {
                    reclamacoes = reclamacoes.Where(s => s.ReleaseDate >= fromDate);
                }

                if (toDate != null)
                {
                    reclamacoes = reclamacoes.Where(s => s.ReleaseDate <= toDate);
                }
            }

            if (Bairro != null && !Bairro.Equals(""))
            {
                reclamacoes = reclamacoes.Where(s => s.Bairro == Bairro);
            }

            int countRec = reclamacoes.Count();

            ViewBag.stat1 = countRec;

            int countCommentTotal = 0;
            var primeira = reclamacoes.FirstOrDefault();
            if (primeira == null) {
                return View(reclamacoes.ToList());
            }
          //  if ((fromDate <= reclamacoes.OrderBy(r => r.ReleaseDate).ToList().Last().ReleaseDate || fromDate == null) && (toDate >= reclamacoes.OrderBy(r => r.ReleaseDate).ToList().First().ReleaseDate || toDate == null))
           // {
            //    if (aberto == true || resolvido == true || encerrado == true)
                //{
                    if (reclamacoes != null)
                    {
                        countCommentTotal = reclamacoes.Sum(r => r.Comentarios.Count());
                  }
             //   }


                ViewBag.stat2 = countCommentTotal;

            ViewBag.stat3 = (double)((double)countCommentTotal / (double)countRec);

            var rateAberto = reclamacoes.Count(r => r.Status == Status.ABERTO);
            var rateResolvido = reclamacoes.Count(r => r.Status == Status.RESOLVIDO);
            var rateEncerrado = reclamacoes.Count(r => r.Status == Status.ENCERRADO);

            ViewBag.stat4 = ((double)rateAberto / (double)countRec) * 100.0;
            ViewBag.stat5 = ((double)rateResolvido / (double)countRec) * 100.0;
            ViewBag.stat6 = ((double)rateEncerrado / (double)countRec) * 100.0;


            //}
      

            return View(reclamacoes.ToList());
        }

        public ViewResult UserList(string user, string searchString, string sortOrder, int? SelectedCategory)
        {
            var categorias = db.Categorias.OrderBy(c => c.Nome).ToList();
            ViewBag.SelectedCategory = new SelectList(categorias, "CategoriaID", "Nome", SelectedCategory);
            int categoryID = SelectedCategory.GetValueOrDefault();

            var reclamacoes = db.Reclamacoes
                .Where(c => !SelectedCategory.HasValue || c.CategoriaID == categoryID);

            //var reclamacoes = db.Reclamacoes.Include(r => r.Categoria);

            if (!String.IsNullOrEmpty(searchString))
            {
                reclamacoes = reclamacoes.Where(s => s.Nome.Contains(searchString) || s.Descricao.Contains(searchString));
            }

            ViewBag.RatingSortParm = sortOrder == "Rating" ? "rating_asc" : "Rating";

            switch (sortOrder)
            {
                case "Rating":
                    reclamacoes = reclamacoes.OrderBy(s => s.ReleaseDate);
                    break;
                case "rating_asc":
                    reclamacoes = reclamacoes.OrderByDescending(s => s.ReleaseDate);
                    break;
            }

            reclamacoes = reclamacoes.Where(r => r.Usuario.Equals(User.Identity.Name));

            return View(reclamacoes.ToList());
        }



        // GET: Reclamacoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            return View(reclamacao);
        }

        // GET: Reclamacoes/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome");
            return View();
        }

        // POST: Reclamacoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReclamacaoID,Nome,Endereco,Bairro,ReleaseDate,Descricao,Status,CategoriaID,Usuario,ImageFile,ImageMimeType,ImageUrl")] Reclamacao reclamacao, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    reclamacao.ImageMimeType = image.ContentType;
                    reclamacao.ImageFile = new byte[image.ContentLength];
                    //save the photo file by using image.InputStream.Read method.
                    image.InputStream.Read(reclamacao.ImageFile, 0, image.ContentLength);
                }

                reclamacao.Usuario = User.Identity.Name;
                reclamacao.ReleaseDate = DateTime.Now;
                reclamacao.Status = Status.ABERTO;
                db.Reclamacoes.Add(reclamacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", reclamacao.CategoriaID);
            return View(reclamacao);
        }

        // GET: Reclamacoes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);

            if (reclamacao == null)
            {
                return HttpNotFound();
            }
            
            if (reclamacao.Usuario == User.Identity.Name)
            {
                ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", reclamacao.CategoriaID);
                return View(reclamacao);

            }
            return RedirectToAction("Index");

        }

        // POST: Reclamacoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReclamacaoID,Nome,Endereco,Bairro,ReleaseDate,Descricao,Status,CategoriaID,Usuario,ImageFile,ImageMimeType,ImageUrl")] Reclamacao reclamacao, HttpPostedFileBase image)
        {
          
           
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    reclamacao.ImageMimeType = image.ContentType;
                    reclamacao.ImageFile = new byte[image.ContentLength];
                    //save the photo file by using image.InputStream.Read method.
                    image.InputStream.Read(reclamacao.ImageFile, 0, image.ContentLength);
                }

                reclamacao.ReleaseDate = DateTime.Now;// isso deve ser melhorado
                reclamacao.Usuario = User.Identity.Name;
                db.Entry(reclamacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nome", reclamacao.CategoriaID);
            return View(reclamacao);
        }

        // GET: Reclamacoes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
           
                if (reclamacao == null)
                {
                    return HttpNotFound();
                }
            if (reclamacao.Status == Status.ABERTO)
            {
                
                return View(reclamacao);
            }
            ViewBag.erro ="Você não pode excluir uma reclamação  resolvida ou encerrada";
            return RedirectToAction("Index");
        }

        // POST: Reclamacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            db.Reclamacoes.Remove(reclamacao);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]        
        public ActionResult Comment(int RecID)
        {
            Comentario c = new Comentario();
            c.ReclamacaoID = RecID;
            c.Usuario = User.Identity.Name;
            return this.PartialView(c);
        }

        public ActionResult GetImage(int id)
        {
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            if (reclamacao != null && reclamacao.ImageFile != null)
            {
                //File used to return a binary content and the contenttype of the returned photo
                return File(reclamacao.ImageFile, reclamacao.ImageMimeType);
            }
            else
            {
                return new FilePathResult("~/Images/nao-disponivel.png", "image/png");
            }
        }

        public ActionResult ReclamaFilter(string term)
        {
            term = term.ToLower();
            var reclamacoes = from rec in db.Reclamacoes where (rec.Nome.ToLower().Contains(term)) select rec.Nome;
            return Json(reclamacoes, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult Resolvido(int id)
        {
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            reclamacao.Status = Status.RESOLVIDO;
            db.SaveChanges();
            return RedirectToAction("Details/" + reclamacao.ReclamacaoID.ToString());
        }

        [Authorize(Roles = "Oficial")]
        public ActionResult Encerrar(int id)
        {
            Reclamacao reclamacao = db.Reclamacoes.Find(id);
            reclamacao.Status = Status.ENCERRADO;
            db.SaveChanges();
            return RedirectToAction("Details/" + reclamacao.ReclamacaoID.ToString());
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
