using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Калмыков

//1 Добавить в программу коллекцию астероидов.Как только она заканчивается(все астероиды сбиты), 
//формируется новая коллекция, в которой на один астероид больше.
//2 Дана коллекция List<T>. Требуется подсчитать, сколько раз каждый элемент встречается в данной коллекции:
//для целых чисел;



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