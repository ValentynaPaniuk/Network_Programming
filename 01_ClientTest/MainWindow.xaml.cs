using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _01_ClientTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         Клієнт посилає серверу вказівки для отримання дати або часу (window)
         Сeрвер, в залежності від отриманої команди  - повертає відповідь (console)
         */
        public MainWindow()
        {
            InitializeComponent();
        }

        const int port = 2020;
        const int size = 1024;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), port);

                if (client.Connected)
                {
                    string text = tbMsg.Text;
                    byte[] data = Encoding.UTF8.GetBytes(text);


                    client.Send(data);
                    tbMsg.Clear();

                    byte[] buffer = new byte[size];
                    int count = client.Receive(buffer);

                    lbResponce.Content += Encoding.UTF8.GetString(buffer, 0, count) + Environment.NewLine;
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }

    }
}
