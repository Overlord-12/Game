using Library1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library2
{
    class EmployeeV2 : PersonV2
    {
        void Process()
        {
            Birthday = DateTime.Now;
        }
    }
}
