using System;
using System.Collections.Generic;
using System.Linq;
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
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChatClient client;
        int ID;

        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if(!isConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);

                tbUserName.IsEnabled = true;
                bConnDiscon.Content = "Disconnect";
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if(isConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = false;
                bConnDiscon.Content = "Connect";
                isConnected = false;
            }
        }

        private void bConnDiscon_Click(object sender, RoutedEventArgs e)
        {
            if(isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(client != null)
                {
                    client.SendMessage(tbMessage.Text, ID);
                    tbMessage.Text = string.Empty;
                }
            }
        }
    }
}
