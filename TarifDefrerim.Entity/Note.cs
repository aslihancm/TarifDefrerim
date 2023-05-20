using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarifDefrerim.Entity
{
    public class Note:MyEntityBase
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsDraft { get; set; }
        public int LikeCount { get; set; }
        public virtual Category Category { get; set; }
        public virtual TarifUser Owner { get; set; }
        public virtual List< Comment> Comments{ get; set; }
        public virtual List< Liked> Likes{ get; set; }




    }
}
