using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoTasker
{
    public static class DecoratedText
    {
        //writes a bullet point (>>)
        //used to indicate a new line of text in the console
        public static void Bullet()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(">> ");
            Console.ResetColor();
        }
        //writes bullet point and text with a newline at the end
        public static void WriteLine(string lineText)
        {
            Bullet();
            Console.WriteLine(lineText);
        }
        //writes bullet point and text with a newline at the end, with a specified color
        public static void WriteLine(string lineText, ConsoleColor color)
        {
            Bullet();
            Console.ForegroundColor = color;
            Console.WriteLine(lineText);
            Console.ResetColor();
        }
        //writes bullet point and a number then text with a newline at the end (for menus)
        public static void WriteLine(int num,string lineText)
        {
            Bullet();
            Console.Write("(");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"{num}");
            Console.ResetColor();
            Console.WriteLine($") {lineText}");
        }
        //writes a line of text with a bullet point and no newline at the end
        public static void Write(string lineText)
        {
            Bullet();
            Console.Write(lineText);
        }
    }
}
