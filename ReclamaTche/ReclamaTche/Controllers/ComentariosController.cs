using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReclamaTche.Models;

namespace ReclamaTche.Controllers
{
    public class ComentariosController : Controller
    {
        private ReclamaDBContext db = new ReclamaDBContext();

        // GET: Comentarios
        public ActionResult Index()
        {
            var comentarios = db.Comentarios.Include(c => c.Reclamacao);
            return View(comentarios.ToList());
        }

        public ActionResult List(int RecID)
        {
            var comentarios = db.Comentarios.Include(c => c.Reclamacao);
            List<Comentario> cFinal = new List<Comentario>();
            foreach (Comentario c in comentarios)
                if (c.ReclamacaoID == RecID)
                    cFinal.Add(c);

            return View(cFinal.ToList());
        }

        // GET: Comentarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // GET: Comentarios/Create
        public ActionResult Create()
        {
            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Nome");
            return View();
        }

        // POST: Comentarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Body,Usuario, ReleaseDate, ReclamacaoID,ImageFile,ImageMimeType,ImageUrl")] Comentario comentario, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    comentario.ImageMimeType = image.ContentType;
                    comentario.ImageFile = new byte[image.ContentLength];
                    //save the photo file by using image.InputStream.Read method.
                    image.InputStream.Read(comentario.ImageFile, 0, image.ContentLength);
                }
                comentario.ReleaseDate = DateTime.Now;
                db.Comentarios.Add(comentario);
                db.SaveChanges();
                return RedirectToAction("Details/"+comentario.ReclamacaoID.ToString(), "Reclamacoes");
            }

            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Nome", comentario.ReclamacaoID);
            return RedirectToAction("Details/" + comentario.ReclamacaoID.ToString(), "Reclamacoes");
        }

        // GET: Comentarios/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Nome", comentario.ReclamacaoID);
            return View(comentario);
        }

        // POST: Comentarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Body,Usuario,ReleaseDate,ReclamacaoID")] Comentario comentario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comentario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReclamacaoID = new SelectList(db.Reclamacoes, "ReclamacaoID", "Nome", comentario.ReclamacaoID);
            return View(comentario);
        }

        // GET: Comentarios/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario == null)
            {
                return HttpNotFound();
            }
            return View(comentario);
        }

        // POST: Comentarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            db.Comentarios.Remove(comentario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetImage(int id)
        {
            Comentario comentario = db.Comentarios.Find(id);
            if (comentario != null && comentario.ImageFile != null)
            {
                //File used to return a binary content and the contenttype of the returned photo
                return File(comentario.ImageFile, comentario.ImageMimeType);
            }

            return null;
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
