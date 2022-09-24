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

namespace basic_chat_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Socketer Socketer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Connexion au serveur
            Socketer = new Socketer("SIMPLE CHAT APP",txtBox_Ip.Text, Convert.ToInt32(txtBox_Port.Text), Receive);
        }

        private void Receive(Message msg)
        {
            Dispatcher.Invoke(() =>
            {
                if(msg.MessageType == MESSAGE_TYPE.MESSAGE)
                    richTextBox_chat.AppendText("\n"+ msg.Id + " said : " + msg.Content);
                else if(msg.MessageType == MESSAGE_TYPE.CONNECTION)
                {
                    richTextBox_chat.AppendText("\n" + msg.Id + " a rejoins le chat");
                }
                else if(msg.MessageType == MESSAGE_TYPE.DISCONNECTION)
                {
                    richTextBox_chat.AppendText("\n" + msg.Id + " s'est déconnecté du chat");
                }
            });
        }

        private void txtBox_sender_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (Socketer == null)
                {
                    button_connect.Foreground = Brushes.Red;
                }
                else
                {

                    Socketer.Send(txtBox_sender.Text);
                    Receive(new Message(Socketer.ConnetionId, txtBox_sender.Text, Socketer.AppName, MESSAGE_TYPE.MESSAGE));

                    txtBox_sender.Text = String.Empty;
                }
            }
        }
    }
}
