using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Калмыков

//Добавить космический корабль, как описано в уроке.
//Доработать игру «Астероиды»:
//Добавить ведение журнала в консоль с помощью делегатов;
//* добавить это и в файл.
//Разработать аптечки, которые добавляют энергию.
//Добавить подсчет очков за сбитые астероиды.
//* Добавить в пример Lesson3 обобщенный делегат.



namespace MyGame
{
    class Program
    {
        static void Main(string[] args)
        {

            Form form = new Form()
            {
                Width = Screen.PrimaryScreen.Bounds.Width,
                Height = Screen.PrimaryScreen.Bounds.Height
            };
            init:
            try
            {

                Game.Init(form);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка " + e + ". Устанавливаем значения по умолчанию.");

                form.Height = 999;
                form.Width = 999;
                Game.Init(form);
                goto init;
            }

            form.Show();
            Game.Load();
            Game.Draw();
            Application.Run(form);

        }
    }
}