using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SC1
{
    public class Declarations
    {
        //Const
        public const int Max_Connections = 5;
        public const int Server_Port = 7666;
        public const ProtocolType Protocol_Type = ProtocolType.Tcp;
        public const SocketType Socket_Type = SocketType.Stream;
        public const AddressFamily Address_Family = AddressFamily.InterNetwork;

        //Variants
        public static Boolean Accept_Connections = false;
        public static int Users = 0;
        public static int Last_Client = 0;

        //Enum
        public enum List_User { Add, Del, Clear }

        //Server_Variants
        public static List<UserList_> UserList = new List<UserList_>();

    }
    public class UserList_
    {
        public Socket Cliente { set; get; }
        public String Name { set; get; }
        public Thread Receiv { set; get; }
        public String Correo { set; get; }

        public UserList_(Socket C, String N)
        {
            Cliente = C;
            Name = N;
            Receiv = new Thread(new ThreadStart(Client_Received));
        }
        public void Init()
        {
            Receiv.Start();
        }
        public void Client_Received()
        {
            Byte[] WelB = Encoding.Default.GetBytes("hldac");
            Cliente.Send(WelB);

            int Recv = 0;
            Byte[] data;
            Byte[] Dato;
            string stringData;

            while (Declarations.Accept_Connections && Conn.SocketConnected(Cliente))
            {
                try
                {
                    data = new Byte[1024];
                    //recvBuffers.Add(new ArraySegment<byte>(dati));
                    Recv = Cliente.Receive(data, SocketFlags.None);
                    Dato = new Byte[Recv];
                    Array.Copy(data, 0, Dato, 0, Recv);
                    try
                    {
                        //stringData = Encoding.ASCII.GetString(data, 0, Recv);
                        //String tmp = stringData.Replace('\n', ' ');

                        stringData = Encoding.Default.GetString(Dato, 0, Recv);// Encoding.UTF32.GetString(data, 0, Recv);

                        Handle1.Handle_Data(stringData, this, Dato);
                        Console.WriteLine("[" + Cliente.Handle.ToString() + "] Recivido: " + stringData);
                        /*if (Recv > 1000)
                        {
                            Consolas.C_Add("[" + Cliente.Handle.ToString() + "] Recivido: ARCHIVO!");
                            //Consolas.P_Add(Func.byteArrayToImage(data));
                            Handle1.Handle_Archive(stringData, this, data);
                        }
                        else
                        {
                            Consolas.C_Add("[" + Cliente.Handle.ToString() + "] Recivido: " + stringData);
                            Handle1.Handle_Data(stringData, this, null);
                        }*/
                        stringData = String.Empty;
                        Array.Clear(data, 0, data.Length);
                        Array.Clear(Dato, 0, Dato.Length);
                    }
                    catch (System.ArgumentOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
