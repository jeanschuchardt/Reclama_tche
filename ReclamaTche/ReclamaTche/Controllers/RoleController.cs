using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReclamaTche.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace ReclamaTche.Controllers
{
    public class RoleController : Controller
    {

        ApplicationDbContext context;

        public RoleController()
        {
            context = new ApplicationDbContext();
        }

        #region esta funcionando
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        //public ActionResult ControleDeUsuarios()
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var user = db.Users.ToList();
        //    var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

        //    foreach (ApplicationUser u in user) {
        //        ViewBag.id =  u.Id;
        //        ViewBag.userName = u.UserName;
        //        return PartialView("listRoles");

        //    }

        // //   ViewBag.userOb = user;
        //    return View("ControleDeUsuarios");
        //}

        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            var Role = new IdentityRole();
            return View(Role);
        }

        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion


        [Authorize(Roles = "Administrador")]
        public ActionResult ManageUserRoles()
        {
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
                                    new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            var listUser = context.Users.OrderBy(u => u.UserName).ToList().Select(r => new SelectListItem { Value = r.UserName.ToString(), Text = r.UserName }).ToList();
            ViewBag.listUser = listUser;
            return View();
        }




        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ViewBag.m1 = "entrou na funcao";
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (user != null)
            {
                ViewBag.m2 = "ta no if";
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                UserManager.AddToRole(user.Id, RoleName);

                ViewBag.ResultMessage = "Nivel de acesso criado com sucesso";
            }
            else
            {
                ViewBag.ErrorMessage = "Não foi possível criar esse nivel de acesso";
                ViewBag.WarringMessage = "Verifique se o usuário foi informado corretamente";

            }

            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            var listUser = context.Users.OrderBy(u => u.UserName).ToList().Select(r => new SelectListItem { Value = r.UserName.ToString(), Text = r.UserName }).ToList();
            ViewBag.listUser = listUser;
            ViewBag.m3 = "vai retornar";
            return View("ManageUserRoles");
        }


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {

            /*
             * Funciona  assim como a lina de baixo
                ApplicationDbContext db = new ApplicationDbContext();
                ApplicationUser user = db.Users.FirstOrDefault(x => x.UserName == UserName);
             */
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (user != null)
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id).ToList();
            }
            else // se não foi encontrado usuário na base de dados 
            {
                ViewBag.ErrorMessage = "Opss!!! Esse usuário não existe!";
                ViewBag.WarringMessage = "Verifique se o usuário foi informado corretamente";
            }
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            var listUser = context.Users.OrderBy(u => u.UserName).ToList().Select(r => new SelectListItem { Value = r.UserName.ToString(), Text = r.UserName }).ToList();
            ViewBag.listUser = listUser;
            return View("ManageUserRoles");
        }


        [HttpPost]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            // var account = new AccountController();
            ///ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            if (UserManager.IsInRole(user.Id, RoleName))
            {
                UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Papel de usuário removido com sucesso";
            }
            else
            {
                ViewBag.ErrorMessage = "Esse papel não está atribuido a esse usuário";
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;
            var listUser = context.Users.OrderBy(u => u.UserName).ToList().Select(r => new SelectListItem { Value = r.UserName.ToString(), Text = r.UserName }).ToList();
            ViewBag.listUser = listUser;
            return View("ManageUserRoles");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult listRoles(string UserName)
        //{

        //    /*
        //     * Funciona  assim como a lina de baixo
        //        ApplicationDbContext db = new ApplicationDbContext();
        //        ApplicationUser user = db.Users.FirstOrDefault(x => x.UserName == UserName);
        //     */
        //    ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        //    if (user != null)
        //    {
        //        var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        //        ViewBag.RolesForThisUser = UserManager.GetRoles(user.Id).ToList();
        //    }
        //    else // se não foi encontrado usuário na base de dados 
        //    {
        //        ViewBag.ErrorMessage = "Opss!!! Esse usuário não existe!";
        //        ViewBag.WarringMessage = "Verifique se o usuário foi informado corretamente";
        //    }
        //    var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        //    ViewBag.Roles = list;

        //    return View("ControleDeUsuários");
        //}

    }
}