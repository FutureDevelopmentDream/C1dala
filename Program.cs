using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SC1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando...");

            
            Console.WriteLine("Press Enter from initialize server.");
            Console.ReadLine();

            Console.WriteLine("Init...");
            Conn.Init();

            Console.WriteLine("Press Enter from Disconnect.");
            Console.ReadLine();

            Conn.Finality_Sockets();

            Console.WriteLine("Close read.");
            Console.WriteLine("Press Enter from exit.");
            Console.ReadLine();
        }
    }
}
