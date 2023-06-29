using System.Windows;
using ChatClient.ServiceChat;
using System.Windows.Input;
namespace ChatClient
{
    public partial class MainWindow : Window, IServiceChatCallback
    {
        bool IsConnected = false;
        ServiceChatClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!IsConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                btnConnectDisconnect.Content = "Disconnect";
                IsConnected = true;
            }
        }
        void DisconnectUser()
        {
            if (IsConnected)
            {
                client.Disconnect(ID);
                client = null;
                tbUserName.IsEnabled = true;
                btnConnectDisconnect.Content = "Connect";
                IsConnected = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsConnected)
                DisconnectUser();
            else ConnectUser();
        }

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void TbMessege_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                    client.SendMsg(tbMessege.Text, ID);
                tbMessege.Text = string.Empty;
            }
        }
    }
}
