using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace _01_ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
  
            const int PORT = 2020;
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), PORT);
            try
            {
                server.Bind(ep);
                // 3
                const int backlog = 10;
                const int size = 5;
                server.Listen(backlog); // переходимо в режим прослуховування
                Console.Title = "Server: " + ep;
                while (true)
                {
                    // 4
                    Socket client = server.Accept(); // Wait for client

                    //5
                    byte[] buffer = new byte[size];
                    int count = 0;
                    string data = "";
                    do
                    {
                        int tempCount = client.Receive(buffer);
                        data += Encoding.UTF8.GetString(buffer, 0, tempCount);
                        count += tempCount;
                    }
                    while (client.Available > 0);
                    // Перетворюємо масив байтів у рядок

                    if (data == "DATE" || data == "date")
                    {
                        Console.WriteLine("Got: {0}, count bytes = {1}", data, count);

                        //6
                        string responce = String.Format("DATE => {0}", DateTime.Now.ToShortDateString());
                        client.Send(Encoding.UTF8.GetBytes(responce.ToCharArray(), 0, responce.Length));
                    }

                    else if (data == "TIME" || data == "time")
                    {
                        {
                            Console.WriteLine("Got: {0}, count bytes = {1}", data, count);

                            //6
                            string responce = String.Format("Time => {0}", DateTime.Now.ToShortTimeString());
                            client.Send(Encoding.UTF8.GetBytes(responce.ToCharArray(), 0, responce.Length));
                        }
                    }

                    else
                    {
                        Console.WriteLine("Enter correct text");
                    }

                                    
                    client.Shutdown(SocketShutdown.Both);
                    client.Close(); // звільняє ресурси
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
