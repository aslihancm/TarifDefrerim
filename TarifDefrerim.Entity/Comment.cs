using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarifDefrerim.Entity
{
    public class Comment:MyEntityBase
    {
        public string Text { get; set; }
        public virtual TarifUser Owner { get; set; }
        public virtual Note Note { get; set; }

    }
}
