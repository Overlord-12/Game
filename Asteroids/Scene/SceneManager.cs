using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Asteroids
{
   public class SceneManager
    {

        private static SceneManager _sceneManager;
        private BaseForm _scene;

        public static SceneManager Get()
        {
            if (_sceneManager == null)
                _sceneManager = new SceneManager();
            return _sceneManager;
        }

        public IScene Init<T>(Form form) where T : BaseForm, new()
        {
            if (_scene != null)
                _scene.Dispose(); // Очищаем старую сцену

            _scene = new T(); // Создаем экземпляр новой сцены
            _scene.Init(form);
            return _scene; // Возвращаем сцену
        }



    }
}
