using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using server_v2.include;

namespace server_v2
{
    public partial class Form1 : Form
    {
        Socket serverSocket;
        List<Player> playerList = new List<Player>();
        List<string> playerNames = new List<string>();
        private int numOfQuestions = 0;

        bool terminating = false;
        bool listening = false;
        bool quizStarted = false;
        bool questionFinished = false;

        private static readonly object Lock = new object();
        public Button listenButton { get { return listen_button; } }
        public RichTextBox log { get { return logs; } }
        Quiz quiz;
        QuestionsandAnswers QandA = new QuestionsandAnswers();
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            QandA.addQandAtoDictionary();
            InitializeComponent();
            quiz = new Quiz(QandA.QandADictionary, log, scoreboard, quizStarted, questionFinished, terminating, listenButton);
        }



        private void listen_button_Click(object sender, EventArgs e)
        {
            
            int serverPort;
            terminating = false;
            start_game_button.Enabled = true;
            if (Int32.TryParse(port_tb.Text, out serverPort))
            {
                if (Int32.TryParse(numberofQs_tb.Text, out numOfQuestions))
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    serverSocket.Bind(endPoint);
                    serverSocket.Listen(2);

                    listening = true;
                    listen_button.Enabled = false;
                    port_tb.Enabled = false;
                    numberofQs_tb.Enabled = false;
                    Thread acceptThread = new Thread(Accept);
                    acceptThread.Start();
                    logs.AppendText("Started listening on port: " + serverPort + ".\n");
                }
                else
                {
                    logs.AppendText("Please enter a valid number of questions.\n");
                }
            }
            else
            {
                logs.AppendText("Please enter a valid port.\n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newPlayer = serverSocket.Accept();
                    lock (Lock)
                    {
                        
                        Player tempPlayer = Player.createNewPlayer(newPlayer);
                        
                        if (playerNames.Contains(tempPlayer.playerName)) { return; }
                        else
                        {
                            logs.AppendText("A new player with the name: " + tempPlayer.playerName + " has connected to the server.\n");
                            playerList.Add(tempPlayer);
                            playerNames.Add(tempPlayer.playerName);
                        }

                    }
                    numOfQuestions = Int32.Parse(numberofQs_tb.Text);
                    if (numOfQuestions > 0 && playerList.Count >= 2)
                    {
                        foreach (Player player in playerList)
                        {
                            scoreboard.AppendText(player.playerName + ": " + player.playerScore + "\n");
                        }
                        questionFinished = true;
                        quiz.questionFinished = true;
                        while (true)
                        {

                            foreach(Player player in playerList)
                            {
                                Thread receiveAnswerThread = new Thread(() => quiz.recieveAnswer(player));
                                receiveAnswerThread.Start();
                            }

                            while (Quiz.AnswersList.Count != playerList.Count);
                            quiz.questionFinished = true;
                            quiz.checkAnswers();
                            quiz.questionNo += 1;
                            if (quiz.questionNo == numOfQuestions)
                            {
                                quizStarted = false;
                                quiz.EndGame();
                                playerList.Clear();
                                break;
                            }
                            Quiz.AnswersList = new Dictionary<Player, int>();
                            quiz.startQuiz(playerList);
                        }
                        
                    }

                    

                } 
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }
                }
            }

        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void port_label_Click(object sender, EventArgs e)
        {

        }

        private void port_tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void start_game_button_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (playerList.Count >= 2)
                {
                    
                    start_game_button.Enabled = false;
                    this.quizStarted = true;
                    quiz.quizStarted = true;
                    quiz.startQuiz(playerList);
                }
            }
            catch {
                logs.AppendText("Quiz thread faced a problem.\n");
            }
        }
    }
    
}
