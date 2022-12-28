using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
            answer_textBox.Enabled = true;
            submit_button.Enabled = false;
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string IP = textBox_ip.Text;
            string name = name_textBox.Text;

            int portNum;
            if(Int32.TryParse(textBox_port.Text, out portNum))
            {
                try
                {
                    clientSocket.Connect(IP, portNum);
                    button_connect.Enabled = false;
                    disconnet_button.Enabled = true;
                    submit_button.Enabled = true;
                    connected = true;
                    logs.AppendText("Connected to the server!\n");
                    string message = name_textBox.Text;
                    if (message != "" && message.Length <= 128)
                    {
                        Byte[] buffer = Encoding.Default.GetBytes(message);
                        clientSocket.Send(buffer);
                    }

                    Thread receiveThread = new Thread(Receive);
                    receiveThread.Start();
                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n");
                }
            }
            else
            {
                logs.AppendText("Check the port\n");
            }

        }

        private void Receive()
        {
            while(connected)
            {
                try
                {
                    Byte[] buffer = new Byte[256];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Trim('\0');

                    if (incomingMessage.Length > 0)
                        logs.AppendText(incomingMessage + "\n");
                    if (incomingMessage == "Game has ended" || incomingMessage == "")
                    {

                        clientSocket.Close();
                        connected = false;
                        logs.AppendText("The server has disconnected\n");
                        disconnet_button.Enabled = false;
                        button_connect.Enabled = true;
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server has disconnected\n");
                        button_connect.Enabled = true;
                    }

                    clientSocket.Close();
                    connected = false;
                    disconnet_button.Enabled = false;
                    button_connect.Enabled = true;
                }

            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string client_name = name_textBox.Text;

            if(client_name != "" && client_name.Length <= 256)
            {
                Byte[] buffer = Encoding.Default.GetBytes(client_name);
                clientSocket.Send(buffer);
            }


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void name_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void disconnet_button_Click(object sender, EventArgs e)
        {
            connected = false;
            terminating = true;

            string answer = name_textBox.Text + ": Has Disconnected\n";
            if (answer != "" && answer.Length <= 256)
            {
                Byte[] buffer = Encoding.Default.GetBytes(answer);
                clientSocket.Send(buffer);
            }
            clientSocket.Close();
            disconnet_button.Enabled = false;
            button_connect.Enabled = true;
            answer_textBox.Enabled = false;
            submit_button.Enabled = false;
            logs.AppendText("Server: Game Ended \n");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void answer_textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            string answer = answer_textBox.Text;
            if (answer != "" && answer.Length <= 256)
            {
                Byte[] buffer = Encoding.Default.GetBytes(answer);
                clientSocket.Send(buffer);
                logs.AppendText("Answer: " + answer + "\n");
            }

            
        }
    }
}
