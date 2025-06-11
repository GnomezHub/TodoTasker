using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTasker
{
    public static class DecoratedText
    {

       public static void Bullet()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">> ");
            Console.ResetColor();
        }
        public static void BulletLine(string lineText)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">> ");
            Console.ResetColor();
            Console.WriteLine(lineText);
        }

        public static void BulletNumLine(int num, string lineText)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">> ");
            Console.ResetColor();
            Console.Write("(");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"{num}");
            Console.ResetColor();
            Console.WriteLine($") {lineText}");
        }

        public static void BulletLineColored(string lineText, ConsoleColor color)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">> ");
            Console.ForegroundColor = color;
            Console.WriteLine(lineText);
            Console.ResetColor();
        }
    }
}
