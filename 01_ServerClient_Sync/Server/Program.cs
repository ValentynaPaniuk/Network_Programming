using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    class Program
    {
        const string IP_ADDR = "127.0.0.1";
        const int PORT = 2020;
        const int BACKLOG = 5;
        const int SIZE = 1024;

        static void Main(string[] args)
        {
            List<string> responceVariety = new List<string>() { "Hello", "World", "Here", "There", "But", "Yes", "No", "Bye" };
            Random rand = new Random();
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string responce;

            try
            {
                server.Bind(new IPEndPoint(IPAddress.Parse(IP_ADDR), PORT));
                server.Listen(BACKLOG);
                Console.Title = IP_ADDR + ":" + PORT;

                while (true)
                {
                    Socket client = server.Accept();

                    byte[] buffer = new byte[SIZE];
                    int count = client.Receive(buffer);

                    Console.WriteLine("Client: " + Encoding.UTF8.GetString(buffer, 0, count));

                    responce = responceVariety[rand.Next(0, responceVariety.Count)];

                    client.Send(Encoding.UTF8.GetBytes(responce));

                    Console.WriteLine("You: " + responce);

                    client.Shutdown(SocketShutdown.Both);
                    client.Close();

                   

                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                
                    server.Close();
                
            }
           

        }


    }
}
