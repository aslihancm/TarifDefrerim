using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TarifDefrerim.BusinessLayer;
using TarifDefrerim.Entity;
using TarifDefrerim.Entity.ValueObjects;


namespace TarifDefrerim.Controllers
{
    public class HomeController : Controller
    {

        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private TarifUserManager tarifuserManager = new TarifUserManager();
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                BusinessLayerResult<TarifUser> res = tarifuserManager.LoginUser(model);
                if(res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                Session["login"] = res.result;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)

        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<TarifUser> res = tarifuserManager.RegisterUser(model);
                if(res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                return View("RegisterOk");
            }
            return View(model);
        }
        public ActionResult RegisterOk()
        {
            return View();
        }
        public ActionResult UserActivate(Guid activate_id)
        {
            return View();
        }
        public ActionResult Logout()
        {
            return View();
        }
    }
}