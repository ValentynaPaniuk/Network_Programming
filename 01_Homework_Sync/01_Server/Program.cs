using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _01_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PORT = 2020;
            const int BACKLOG = 5;
            const int SIZE = 1024;

            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT));
            server.Listen(BACKLOG);
            while (true)
            {
                Socket client = server.Accept();

                byte[] buffer = new byte[SIZE];
                int count = client.Receive(buffer);

                if (Encoding.UTF8.GetString(buffer, 0, count).Equals("Date"))
                    client.Send(Encoding.UTF8.GetBytes(DateTime.Now.ToShortDateString()));
                else if (Encoding.UTF8.GetString(buffer, 0, count).Equals("Time"))
                    client.Send(Encoding.UTF8.GetBytes(DateTime.Now.ToShortTimeString()));


                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }
    }
}
