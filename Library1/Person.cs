using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library1
{
    class Person // internal
    {
        int Age { get; set; } // private
        public string Name { get; set; }

        protected DateTime Birthday { get; set; } 

        internal string Param1 { get; set; }


    }
}
