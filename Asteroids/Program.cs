﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
     
        static void Main()
        {
            var form = new Form()
            {
                MinimumSize = new System.Drawing.Size(800, 600),
                MaximumSize = new System.Drawing.Size(800, 600),
                MaximizeBox = false,
                MinimizeBox = false,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Asteroids"
            };
            form.Show();

            SceneManager    // Обратимся к менеджеру сцен
                .Get()
                .Init<MenuScene>(form) // Попросим загрузить меню игры
                .Draw();

            Application.Run(form);


        }

      
    }
}
