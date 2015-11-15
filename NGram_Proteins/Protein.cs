using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGram_Proteins
{
    public class Protein : IComparable<Protein>
    {
        public string PClass;
        public string PName;

        int IComparable<Protein>.CompareTo(Protein obj)
        {
            if (obj != null)
            {
                if (this.PClass +this.PName== obj.PClass+obj.PName)
                {
                    return 0;
                }
                else 
                {
                    return 1;
                }
                //else
                //{
                //    return -1;
                //}
            }
            return -1;
        }

        public Protein(string pclass, string name){
            PClass = pclass;
            PName = name;
    }
    }
}
