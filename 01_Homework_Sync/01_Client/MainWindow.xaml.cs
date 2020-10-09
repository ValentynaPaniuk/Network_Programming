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

namespace _01_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int PORT = 2020;
        const int SIZE = 1024;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), PORT);
                if (client.Connected)
                {

                    client.Send(Encoding.UTF8.GetBytes(tbCommand.Text));

                    byte[] buffer = new byte[SIZE];
                    client.Receive(buffer);

                    lblDate.Content = Encoding.UTF8.GetString(buffer);
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(IPAddress.Parse("127.0.0.1"), PORT);
                if (client.Connected)
                {

                    client.Send(Encoding.UTF8.GetBytes(tbCommand.Text));

                    byte[] buffer = new byte[SIZE];
                    client.Receive(buffer);

                    lblDate.Content = Encoding.UTF8.GetString(buffer);
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}
