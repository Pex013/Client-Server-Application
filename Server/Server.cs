using System;
using System.Text;
using System.Windows.Forms;
using SimpleTCP;

namespace Server
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        //make TCP server
        private SimpleTcpServer server;

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer { Delimiter = 0x13, StringEncoder = Encoding.UTF8 }; // Enter, Make sure you encode in UTF8 format
            server.DataReceived += Server_DataReceived;
        }

        private void Server_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                //Pasting every message back to back
                txtStatus.Text += e.MessageString;
                //Reply back
                e.ReplyLine($"You said {e.MessageString}");
            });
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            txtStatus.Text += "Server starting";
            //Parse the given Ip Address to a long
            //System.Net.IPAddress ip = new System.Net.IPAddress(long.Parse(txtIP.Text));
            var ip = System.Net.IPAddress.Parse(txtIP.Text);
            //Start the server with the ip and parse the port to an int
            server.Start(ip, int.Parse(txtPort.Text));
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.IsStarted)
            {
                server.Stop();
                txtStatus.Clear();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}