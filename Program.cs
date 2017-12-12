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
            Conn.Init();
            Console.WriteLine("Enter command.");
            Console.ReadLine();
            Boolean Se = true;
            while (Se)
            {
                string comi = Console.ReadLine();
                if (IsCommand(comi))
                {
                    try
                    {
                        //int usernum = Convert.ToInt32(GetPart(comi, 1, ' '));
                        switch (GetPart(comi, 0, ' ').ToLower())
                        {
                            case "ejx":
                                int usernum = Convert.ToInt32(GetPart(comi, 1, ' '));
                                Declarations.UserList[Convert.ToInt32(GetPart(comi, 1, ' '))].Send("mkdl" + ";" + usernum.ToString());
                                break;
                            case "ejk": //mkda
                                usernum = Convert.ToInt32(GetPart(comi, 1, ' '));
                                Declarations.UserList[Convert.ToInt32(GetPart(comi, 1, ' '))].Send("mkda"+";"+ usernum.ToString() + ";" + comi.Substring("/ejk ".Length + usernum.ToString().Length + 1));
                                break;
                            case "exit":
                                Se = false;
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {

                }
            }
            Conn.Finality_Sockets();

            Console.WriteLine("Close read.");
            Console.WriteLine("Press Enter from exit.");
            Console.ReadLine();
        }
        static String GetPart(string tx, int part, char sep)
        {
            try
            {
                return tx.Substring(1).Split(sep)[part];
            }
            catch
            {
                return tx;
            }
        }
        static Boolean IsCommand(string Text)
        {
            try
            {
                if (Text.Split('/').Length > 0)
                {
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
