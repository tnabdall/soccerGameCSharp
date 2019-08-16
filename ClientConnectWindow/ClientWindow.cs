using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using SoccerGame;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;



namespace ClientConnectWindow
{
    public partial class ClientConnectionApp : Form
    {
        String serverIP;
        int serverPort;
        bool connected = false;
        String connectedType;

        public ClientConnectionApp()
        {
            InitializeComponent();

            var wpfwindow = new SoccerGame.MainWindow();
            ElementHost.EnableModelessKeyboardInterop(wpfwindow);
            wpfwindow.Show();
        }

        private void HandleErrorConnection(PacketHeader header, Connection connection, string message)
        {
            errorLabel.Text = message;
        }

        private void HandleConnectionInfo(PacketHeader header, Connection connection, string message)
        {
            if (message[1] == '1')
            {
                keeperButton.Enabled = true;
            }
            else
            {
                keeperButton.Enabled = false;
            }

            if (message[3] == '1')
            {
                shooterConnectButton.Enabled = true;
            }
            else
            {
                shooterConnectButton.Enabled = false;
            }

        }

        private void HandleSuccessConnection(PacketHeader header, Connection connection, string message)
        {
            connectedType = message;
            keeperButton.Enabled = false;
            shooterConnectButton.Enabled = false;
            spectateButton.Enabled = false;
            connectButton.Enabled = false;
        }
    

        

        private bool isValidIPAddress()
        {
            String text = ipAddressTextBox.Text;
            Regex rx = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}[:]\d{1,6}$",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rx.Matches(text);
            return (matches.Count != 0);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (isValidIPAddress()) {
                try
                {
                    String serverInfo = ipAddressTextBox.Text;
                    //Parse the necessary information out of the provided string
                    serverIP = serverInfo.Split(':').First();
                    serverPort = int.Parse(serverInfo.Split(':').Last());

                    // Handle error connection request
                    NetworkComms.AppendGlobalIncomingPacketHandler<string>("connectionInfo", HandleConnectionInfo);
                    NetworkComms.AppendGlobalIncomingPacketHandler<string>("success", HandleSuccessConnection);
                    NetworkComms.AppendGlobalIncomingPacketHandler<string>("error", HandleErrorConnection);

                    //Send the message in a single line
                    NetworkComms.SendObject("RequestConnectionInfo", serverIP, serverPort, "");
                    connected = true;
                    connectButton.Enabled = false;
                }
                catch
                {
                    MessageBox.Show("Could not connect to requested IP and port");
                }
                 
            }
            else
            {
                MessageBox.Show("Invalid IP and port entered");
            }

        }

        private void ipAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            if (isValidIPAddress())
            {
                ipAddressTextBox.ForeColor = Color.Green;
            }
            else
            {
                ipAddressTextBox.ForeColor = Color.Red;
            }
        }

        private void keeperButton_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                NetworkComms.SendObject("ConnectionRequest", serverIP, serverPort, "keeper");
            }
        }

        private void shooterConnectButton_Click(object sender, EventArgs e)
        {
            if (connected)
            {
                NetworkComms.SendObject("ConnectionRequest", serverIP, serverPort, "shooter");
            }
        }
    }
}
