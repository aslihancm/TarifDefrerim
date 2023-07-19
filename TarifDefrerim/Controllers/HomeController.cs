using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TarifDefrerim.BusinessLayer;
using TarifDefrerim.Entity;
using TarifDefrerim.Entity.Messages;
using TarifDefrerim.Entity.ValueObjects;
using TarifDefrerim.ViewModels;

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
                    if(res.Errors.Find(x=>x.Code==ErrorMessageCode.UserIsNotActive)!=null)
                    {
                        ViewBag.SetLink = "http";
                    }
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }
                Session["login"] = res.result;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
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
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                OkViewModel notifymodel = new OkViewModel()
                {

                    Title = "Kayıt Başarılı",
                    RedirectingUrl = "/Home/Login"
                };
                notifymodel.Items.Add("Lütfen eposta adrsinize gönderilen aktivasyon linkine tıklayarak hesabınızı aktifleştiriniz. Hesabınızı aktif etmeden note yazamaz, yorum yapamazsınız.");

                return View("Ok", notifymodel);
            }

           
            return View(model);
        }
      
        public ActionResult UserActivate(Guid id)
        {
            BusinessLayerResult<TarifUser> res = tarifuserManager.ActivateUser(id);
            if(res.Errors.Count>0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Geçersiz işlem",
                    Items = res.Errors
                };
                return View("Error", errorNotifyObj);
            }
            OkViewModel okNotifyObj = new OkViewModel()
            {
                Title = "Hesap Aktifleştirildi",
                RedirectingUrl = "/Home/Login"
            };

            okNotifyObj.Items.Add("Hesabınız aktifleştirildi. Artık note yazabilir ve beğeni yapabilirsiniz.");

            return View("Ok", okNotifyObj);
        }

       
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult ShowProfile()
        {
            TarifUser currentUser = Session["login"] as TarifUser;
            BusinessLayerResult<TarifUser> res = tarifuserManager.GetUserById(currentUser.Id);
            if(res.Errors.Count>0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items = res.Errors
                };
            }
            return View(res.result);
        }
        public ActionResult EditProfile()
        {

            TarifUser currentUser = Session["login"] as TarifUser;
            BusinessLayerResult<TarifUser> res = tarifuserManager.GetUserById(currentUser.Id);
            if(res.Errors.Count>0)
                {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Hata Oluştu",
                    Items =res.Errors
                };
                return View("Error", errorNotifyObj);
            }

            return View(res.result);
        }

        [HttpPost]
        public ActionResult EditProfile(TarifUser model, HttpPostedFileBase ProfileImage)
        {
            if (ProfileImage != null &&
                ProfileImage.ContentType=="image/jpeg" ||
                ProfileImage.ContentType=="image/jpg" ||
                ProfileImage.ContentType=="image/png")
            {
                string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                ProfileImage.SaveAs(Server.MapPath($"~/Content/images/{filename}"));
                model.ProfileImageFilename = filename;
            }

            BusinessLayerResult<TarifUser> res = tarifuserManager.UpdateProfile(model);
            if(res.Errors.Count>0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    Title = "Profil Güncellenemedi",
                    Items = res.Errors,
                    RedirectingUrl = "Home/EditProfile"
                };
                return View("Error", errorNotifyObj);
            }
            return View();
        }
        public ActionResult DeleteProfile()
        {
            TarifUser currentuser = Session["login"] as TarifUser;

            BusinessLayerResult<TarifUser> res = tarifuserManager.RemoveUserById(currentuser.Id);

            if (res.Errors.Count > 0)
            {
                ErrorViewModel errorNotifyObj = new ErrorViewModel()
                {
                    RedirectingUrl = "/Home/ShowProfile",
                    Title = "Profil Silinemedi",
                    Items = res.Errors
                };

                return View("Error", errorNotifyObj);
            }

            Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteProfile(TarifUser model)
        {
            return View();
        }
        }
}