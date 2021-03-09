using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library1
{
    class Employee : Person
    {
        void Process()
        {
            Name = "Андрей";
            Birthday = DateTime.Now;
            Param1 = "P1";
        }
    }
}
