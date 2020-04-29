using System;
using System.Text;
using System.Windows.Forms;
using SimpleTCP;

namespace Client
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        private SimpleTcpClient _client;

        private void Client_Load(object sender, EventArgs e)
        {
            _client = new SimpleTcpClient { StringEncoder = Encoding.UTF8 };
            _client.DataReceived += Client_DataReceived;
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            txtStatus.Invoke((MethodInvoker)delegate ()
            {
                txtStatus.Text += e.MessageString;
            });
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            _client.Connect(txtIP.Text, int.Parse(txtPort.Text));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _client.WriteLineAndGetReply(txtMessage.Text, TimeSpan.FromSeconds(3));
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}