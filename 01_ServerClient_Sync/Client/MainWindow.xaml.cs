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

namespace Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
          Створити два віконних додатки. Перший - сервер, другий - клієнт
        Користувач вводить IP адрес і номер порту. Після натискання на кнопку Connect - клієнт підключається до серверу.
        Якщо з'єднання успішне, клієнт і сервер можуть обмінюватися повідомленнями, поки хтось не набере "Bye"
        Після відправки цього рядка - розривається з'єднання.
        Для реалізації відповідей комп'ютера використовуйте набір заготовлених тез, що обираються випадково.
         */
        const int SIZE = 1024;
        string ip_addr;
        int port;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ConnectBtn_Click(object sender, RoutedEventArgs e)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ip_addr = tbIP.Text;
            port = int.Parse(tbPort.Text);

            try
            {
                client.Connect(new IPEndPoint(IPAddress.Parse(ip_addr), port));
                if (client.Connected)
                {
                    lb1.Content = "IPAddress: " + tbIP.Text;
                    lb2.Content = "Port: " + tbPort.Text;
                    tbIP.Visibility = tbPort.Visibility = btnConnect.Visibility = Visibility.Collapsed;
                    tbMessage.Visibility = SendMessageBtn.Visibility = Visibility.Visible;
                  

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

        private void SendMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(new IPEndPoint(IPAddress.Parse(ip_addr), port));

                if (client.Connected)
                {

                    client.Send(Encoding.UTF8.GetBytes(tbMessage.Text));

                    Label label1 = new Label();
                    label1.Content = "You: " + tbMessage.Text;
                    Label label = new Label();

                    byte[] buffer = new byte[SIZE];

                    int count = client.Receive(buffer);

                    label.Content = "Server: " + Encoding.UTF8.GetString(buffer, 0, count);

                    if (label.Content.ToString() == "Server: Bye" || label1.Content.ToString() == "You: Bye")
                    {
                        MessageBox.Show("The connection is broken");
                        client.Close();
                    }

                    sp.Children.Add(label1);
                    sp.Children.Add(label);

                    
                    
                }
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
            //finally
            //{
            //    client.Close();
            //}
        }
    }
}
