using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    public interface IScene  
    {
        void Init(Form form);
        void Draw();
    }
}
