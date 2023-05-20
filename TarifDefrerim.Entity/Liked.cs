using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarifDefrerim.Entity
{
    public class Liked
    {
        public int Id { get; set; }
        public virtual TarifUser Owner { get; set; }
        public virtual TarifUser LikedUser { get; set; }


    }
}
