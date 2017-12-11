using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SC1
{
    public class Conn
    {
        public static Socket Master_Socket;
        public static IPEndPoint Master_IPEnd;
        public static Thread M_Accept;
        public static Thread M_State;
        public static Boolean Accept_Connections = false;
        public static void Init()
        {
            Master_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress[] IPs = Dns.GetHostAddresses("networkhit.securitytactics.com");
            Master_IPEnd = new IPEndPoint(IPs[0], 7666);
            Console.WriteLine("Socket Starting... {" + Master_IPEnd.Address.ToString() + ":" + Master_IPEnd.Port.ToString() + "}");
            Master_Socket.Bind(Master_IPEnd);
            Console.WriteLine("Socket Bind...");
            Master_Socket.Listen(10);
            Console.WriteLine("Socket Listen...");
            Accept_Connections = true;
            Accept();
        }
        public static void Accept()
        {
            M_Accept = new Thread(new ThreadStart(Socket_Accept));
            M_State = new Thread(new ThreadStart(Socket_State));
            M_Accept.Start();
            M_State.Start();
        }
        public static void Socket_Accept()
        {
            Console.WriteLine("Sockets Accept!");
            UserList_ tmp;
            while (Accept_Connections)
            {
                try
                {
                    Thread.Sleep(1);
                }
                catch
                { }
                if (Declarations.Users < 10)
                {
                    Console.WriteLine("Esperando usuarios...");
                    try
                    {
                        tmp = new UserList_(Master_Socket.Accept(), "");
                        Declarations.UserList.Add(tmp);
                        Declarations.Last_Client += 1;
                        Declarations.UserList[Declarations.Last_Client].Init();

                        Console.WriteLine("Usuario aceptado!");
                        Declarations.Users++;
                    }
                    catch
                    { Console.WriteLine("Error al aceptar usuario"); }
                }
            }
        }
        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        public static void Socket_State()
        {
            Console.WriteLine("Sockets State!");
            while (Declarations.Accept_Connections)
            {
                Thread.Sleep(1);
                Console.WriteLine(Declarations.Users);

                if (Declarations.Users > 0)
                {
                    for (int i = 0; i <= Declarations.UserList.Count - 1; i++)
                    {
                        if (!SocketConnected(Declarations.UserList[i].Cliente))
                        {
                            Console.WriteLine("Cliente " + i.ToString() + " Desconectado...");
                            try
                            {
                                Declarations.UserList[i].Receiv.Join();
                                Declarations.UserList[i].Cliente.Close();
                                Declarations.UserList[i].Cliente.Disconnect(false);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            

                            Declarations.UserList.RemoveAt(i);
                            Declarations.Users--;
                            break;
                        }
                    }
                }

            }
        }
        public static bool SocketConnected(Socket s)
        {
            try
            {
                return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
            }
            catch
            {
                return false;
            }
        }
        public static Boolean Finality_Sockets()
        {
            if (Declarations.Users > 0)
            {
                for (int i = 0; i <= Declarations.Users - 1; i++)
                {
                    try
                    {
                        //Func.Send_Data(Declarations.UserList[i].Cliente, "ULOGGIN");
                        Declarations.UserList[i].Receiv.Interrupt();
                        Declarations.UserList[i].Receiv.Join();
                        Declarations.UserList[i].Cliente.Shutdown(SocketShutdown.Both);
                        Declarations.UserList[i].Cliente.Close();
                        Declarations.UserList[i].Cliente.Disconnect(false);
                    }
                    catch { }
                }
            }
            Console.WriteLine("Finalizando...");
            Accept_Connections = false;
            Thread.Sleep(300);
            Console.WriteLine("Cerrando Threads...");
            M_Accept.Interrupt();
            M_State.Interrupt();
            Console.WriteLine("Eliminando Sockets...");
            Declarations.UserList.Clear();
            Master_Socket.Close();
            Console.WriteLine("Socket Terminado!");
            return true;
        }

    }
}
