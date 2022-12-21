using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server_v2
{
    public partial class Form1 : Form
    {
        private int numOfQuestion = 0;

        Socket serverSocket;

        bool terminating = false;
        bool listening = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void listen_button_Click(object sender, EventArgs e)
        {
            
            int serverPort;
            terminating = false;
            if (Int32.TryParse(port_tb.Text, out serverPort))
            {
                if (Int32.TryParse(numberofQs_tb.Text, out numOfQuestion))
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    serverSocket.Bind(endPoint);
                    serverSocket.Listen(2);

                    listening = true;
                    listen_button.Enabled = false;

                    Thread acceptThread = new Thread(Accept);
                    acceptThread.Start();
                    logs.AppendText("Started listening on port: " + serverPort + "\n");
                }
                else
                {
                    logs.AppendText("Please enter a proper number\n");
                }
            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        
    }
}
