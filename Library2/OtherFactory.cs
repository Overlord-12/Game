using Library1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    class OtherFactory
    {
        void Process()
        {
            PersonV2 person = new PersonV2();
            person.Name = "Андрей";
        }
    }
}
