using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
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
                    IsActive=false,
                    IsAdmin=false
                });
                if(dbResult>0)
                {
                    res.result = repo.Find(x => x.Email == data.Email && x.Username == data.Username);
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
       
        
    }
}
