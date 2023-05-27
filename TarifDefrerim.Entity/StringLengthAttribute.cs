using System;

namespace TarifDefrerim.Entity
{
    internal class StringLengthAttribute : Attribute
    {
        private int v;

        public StringLengthAttribute(int v)
        {
            this.v = v;
        }
    }
}