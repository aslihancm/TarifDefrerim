    using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TarifDefrerim.Common.Helpers;
using TarifDefrerim.DataAccessLayer.EntityFramework;
using TarifDefrerim.Entity;
using TarifDefrerim.Entity.Messages;
using TarifDefrerim.Entity.ValueObjects;


namespace TarifDefrerim.BusinessLayer
{
    public class TarifUserManager
    {

        private Repository<TarifUser> repo = new Repository<TarifUser>();
        public BusinessLayerResult <TarifUser>RegisterUser(RegisterViewModel data)
        {
            TarifUser user = repo.Find(x => x.Username == data.Username || x.Email == data.Email);

            BusinessLayerResult<TarifUser> res = new BusinessLayerResult<TarifUser>();
            if(user != null)
            {
                if(user.Username==data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı");
                }
                if(user.Email==data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists,"Email adresi kayıtlı");
                }
               
            }
            else
            {
                int dbResult = repo.Insert(new TarifUser()
                {
                    Username=data.Username,
                    Email=data.Email,
                    Password=data.Password,
                    ActivateGuid=Guid.NewGuid(),
                    ProfileImageFilename="user.png",
                    IsActive=false,
                    IsAdmin=false
                });
                if(dbResult>0)
                {
                    res.result = repo.Find(x => x.Email == data.Email && x.Username == data.Username);
                    string siteUrl = ConfigHelper.Get<string>("SiteRootUrl");
                    string activateUrl = $"{siteUrl}/Home/UserActivate/{res.result.ActivateGuid}";
                    string body = $"Merhaba{res.result.Username};<br/><br/>Hesabınızı aktifleştirmek için <a href='{activateUrl}' target='_blank'>tıklayınız.</a>";
                    MailHelper.SendMail(body, res.result.Email, "TarifDefterim Hesap Aktifleştirme");
                }
            }
            return res;
        }

        public BusinessLayerResult<TarifUser>LoginUser(LoginViewModel data)
        {
            BusinessLayerResult<TarifUser> res = new BusinessLayerResult<TarifUser>();
            res.result = repo.Find(x => x.Email == data.Email && x.Password == data.Password);

            if(res.result!= null)
            {
                if(!res.result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive,"Kullanıcı aktifleştirilmemiştir.Lütfen email adresinizi kontrol ediniz");

                }
            }
            else
            {
                res.AddError(ErrorMessageCode.EmailOrPassWrong,"Email yada şifre uyuşmuyor");
            }
            return res;
        }
       
        public BusinessLayerResult<TarifUser> ActivateUser(Guid id)
        {
            BusinessLayerResult<TarifUser> res = new BusinessLayerResult<TarifUser>();
            res.result = repo.Find(x => x.ActivateGuid == id);
            if(res.result!=null)
            {
                if(res.result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }
                res.result.IsActive = true;
                repo.Update(res.result);

            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }
            return res;
        }

        public BusinessLayerResult<TarifUser> GetUserById(int id)
        {

            BusinessLayerResult<TarifUser> res = new BusinessLayerResult<TarifUser>();
            res.result = repo.Find(x => x.Id == id);
            if (res.result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }
            return res;
        }

        public static BusinessLayerResult<TarifUser> UpdateProfile(TarifUser model)
        {
            TarifUserManager db_user = repo.Find(x => x.Id == model.Id && (x.Username == model.Username || x.Email == model.Email));
            BusinessLayerResult<TarifUser> res = new BusinessLayerResult<TarifUser>();

            if(db_user!= null && db_user.Id==model.Id)
            {
                if(db_user.UserName==model.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExists, "Kullanıcı adı kayıtlı");
                }
            }

        }
    }
}
