using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SC1
{
    public class HandleData
    {
        public static char Sep = ';';
        public static void Handle_Archive(String Data, Byte[] Bytes)
        {

        }
        public static void Handle_PAK(String Data)
        {
            switch (Data)
            {
                /*case "ULOGGIN":
                    Visible_F();
                    MessageBoxEx.Show("Te has desconectado");
                    break;
                case "NOPAS":
                    Visible_F();
                    MessageBoxEx.Show("Contraseña incorrecta...");
                    break;
                case "NOMSN":
                    Visible_F();
                    MessageBoxEx.Show("Cuenta incorrecta o inexistente...");
                    break;
                case "NOACC":
                    Visible_F();
                    MessageBoxEx.Show("Error al iniciar sesión...");
                    break;*/
            }
        }
        public static void Handle_Texto(String Data)
        {
            String Datos = ReadField(1, Data, Sep);

            switch (Datos)
            {
                case "MSG":
                    Console.WriteLine(Data);
                    break;
            }
        }
        public static void Handle_Data(String data, Byte[] Bytes)
        {
            int Count_D = 0;

            if (data.IndexOf(Sep) == -1)
            {
                Count_D = -1;
            }
            else
                Count_D = ReadField(1, data, Sep).Length;
            //MessageBoxEx.Show(ReadField(1, data, Sep));
            switch (Count_D)
            {
                case -1:
                    Handle_PAK(data);
                    break;
                case 4:
                    Handle_Archive(data, Bytes);
                    break;
                case 5:
                    Handle_Texto(data);
                    break;
                default:
                    Handle_Texto(data);
                    break;
            }
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
                //clsFer.Fer_C.Add_Consola("Error ReadField... [" + data + "," + pos + "]");
                return String.Empty;
            }

        }
    }
}
