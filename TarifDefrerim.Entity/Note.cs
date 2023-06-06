using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarifDefrerim.Entity
{
    public class Note:MyEntityBase
    {
        [Required,StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(2000)]

        public string Text { get; set; }
        public bool IsDraft { get; set; }
        public int LikeCount { get; set; }
        public virtual Category Category { get; set; }
        public virtual TarifUser Owner { get; set; }
        public virtual List< Comment> Comments{ get; set; }
        public virtual List< Liked> Likes{ get; set; }




    }
}
