using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TarifDefrerim.DataAccessLayer.EntityFramework;
using TarifDefrerim.Entity;
using TarifDefrerim.Entity.ValueObjects;


namespace TarifDefrerim.BusinessLayer
{
    public class TarifUserManager
    {

        private Repository<TarifUser> repo = new Repository<TarifUser>();
        public TarifUser RegisterUser(RegisterViewModel data)
        {
            TarifUser user = repo.Find(x => x.Username == data.Username || x.Email == data.Email);

            if(user != null)
            {
                throw new Exception("Kayıtlı kullanıcı adı yada email adresi");
            }
            return user;
        }

       
        
    }
}
