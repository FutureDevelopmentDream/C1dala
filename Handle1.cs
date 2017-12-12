using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace SC1
{
    public class Handle1
    {
        public static char Sep = ';';
        public static void Handle_Archive(String dato, UserList_ User_, Byte[] Bytes_)
        {
            if (ReadField_Num(dato, Sep) < 2)
                return;

            switch (ReadField(1, dato, Sep))
            {
                case "IMGG":
                    break;
                case "COMP":
                    break;
            }
        }
        public static void Handle_Texto(String dato, UserList_ User_)
        {
            String Datos = ReadField(1, dato, Sep);

            switch (Datos)
            {
                case "mensa":
                    Console.WriteLine("Mensaje: " + Datos);
                    break;
                case "MSGGL":
                    break;
                case "EDITN":
                    break;

            }
        }
        public static void Handle_Func(String dato, UserList_ User)
        {
            switch (dato)
            {
                case "ULOGGIN":
                    break;
            }
        }
        public static void Handle_Data(String data, UserList_ User, Byte[] Bytes)
        {
            int Count_D = 0;

            if (data.IndexOf(Sep) == -1)
            {
                Count_D = -1;
            }
            else
                Count_D = ReadField(1, data, Sep).Length;

            switch (Count_D)
            {
                case -1:
                    Handle_Func(data, User);
                    break;
                case 4:
                    Handle_Archive(data, User, Bytes);
                    break;
                case 5:
                    Handle_Texto(data, User);
                    break;
            }

            /*switch (Count_D)
            {
                case 0:
                    break;
                case 1:
                    String Tmp_S = Func.Str(User.Name.ToString() + ":" + data.ToString());
                    Consolas.C_Add(Tmp_S);
                    for (int i = 0; i < Declarations.UserList.Count; i++)
                    {
                        Func.Send_Data(Declarations.UserList[i].Cliente, Tmp_S);
                    }
                        break;
                case 2:
                    switch (ReadField(1, data, Sep))
                    {
                        case "LOGIN":
                            String d = ReadField(2, data, Sep);
                            User.Name = d.Replace('\n', ' ');

                            Consolas.C_Add("[" + User.Cliente.Handle.ToString() + "] Changue Name to:" + User.Name);
                            Func.Send_Data(User.Cliente, "LOGGED," + User.Name);
                            break;
                        case "MSG":
                            for (int i = 0; i < Declarations.UserList.Count; i++)
                            {
                                Func.Send_Data(Declarations.UserList[i].Cliente, "MSG" + "," + User.Name + "," + ReadField(2, data, Sep));
                            }
                            break;
                        case "IMG":
                            Consolas.P_Add(Func.byteArrayToImage(Bytes, 4));
                            break;
                    }
                    break;
                case 3:
                    switch (ReadField(1, data, Sep))
                    {
                        case "IMG":
                            break;
                    }
                    break;

            }*/

        }
        public static String ReadEnd(String Paquete, int Inicio)
        {
            try
            {
                return Paquete.Substring(Inicio);
            }
            catch
            {
                return String.Empty;
            }
        }
        public static String ReadField(int Pos, String data, char SepASCII)
        {
            int pos = Pos - 1;
            try
            {
                return data.Split(SepASCII)[pos];
            }
            catch
            {
                Console.WriteLine("Error ReadField... [" + data + "," + pos + "]");
                return String.Empty;
            }
        }
        public static int ReadField_Num(String data, char SepASCII)
        {
            try
            {
                return data.Split(SepASCII).Length;
            }
            catch
            {
                Console.WriteLine("Error ReadField_Num... [" + data + "]");
                return 0;
            }
        }
        public static Socket Search_Name(String name)
        {
            for (int i = 0; i < Declarations.UserList.Count; i++)
            {
                if (Declarations.UserList[i].Name == name)
                {
                    return Declarations.UserList[i].Cliente;
                }
            }
            return null;
        }
    }
}
