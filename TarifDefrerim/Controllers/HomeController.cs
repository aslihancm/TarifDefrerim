using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TarifDefrerim.BusinessLayer;
using TarifDefrerim.Entity;

namespace TarifDefrerim.Controllers
{
    public class HomeController : Controller
    {

        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        // GET: Home
        public ActionResult Index()
        {

            //var notes = noteManager.GetAllNote().OrderByDescending(n=>n.ModifiedOn).ToList(); 
            //return View(notes);

            //Tolist dediğimiz yerde SQL çalışıyor
            return View(noteManager.GetAllNoteQueryable().OrderByDescending(n => n.ModifiedOn).ToList());
        }

        public ActionResult ByCategory(int? id) // ? Nullable kişi Id vermezse 0 dönderir
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Category cat = categoryManager.GetCategoryById(id.Value);
            if(cat==null)
            {
                return HttpNotFound();
            }
            return View("Index", cat.Notes.OrderByDescending(n=>n.ModifiedOn).ToList()); 
        }

        public ActionResult MostLiked()
        {
            return View("Index",noteManager.GetAllNoteQueryable().OrderByDescending(n => n.LikeCount).ToList());
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return View();
        }
    }
}