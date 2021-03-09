using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids
{
    public class MenuScene:BaseForm
    {
        public override void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawString("Меню игры", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Graphics.DrawString("<Enter> - игра", new Font(FontFamily.GenericSansSerif, 40, FontStyle.Underline), Brushes.White, 200, 200);
            Buffer.Graphics.DrawString("<Esc> - выход", new Font(FontFamily.GenericSansSerif, 40, FontStyle.Underline), Brushes.White, 200, 300);
            Buffer.Render();
        }

        public override void SceneKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _form.Close(); // Закрываем форму, завершаем работу приложения
            }
            if (e.KeyCode == Keys.Enter)
            {
                SceneManager                // Обратимся к менеджеру сцен
                    .Get();
                    GameForm.Draw();
                                        // Проинициализируем сцену с игрой
                   
            }
        }
    }
}
