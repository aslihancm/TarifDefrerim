﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarifDefrerim.BusinessLayer
{
    public class BusinessLayerResult<T> where T:class
    {

        public List<string> Errors { get; set; }
        public T result { get; set; }

        public BusinessLayerResult()
        {
            Errors = new List<string>();
        }
    }
}
