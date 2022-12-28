using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server_v2.include
{
    internal class Quiz
    {
        public bool quizStarted;
        public bool questionFinished;
        Dictionary<string, int> questionAndAnswers;
        public int questionNo = 0;
        private string questionText = " ";
        public System.Windows.Forms.RichTextBox logs;
        public System.Windows.Forms.RichTextBox scoreboard;
        List<Player> playerList;
        private static readonly object QuizLock = new object();
        public static Dictionary<Player, int> AnswersList = new Dictionary<Player, int>();
        public Button listenButton;
        private bool terminating;

        public Quiz(Dictionary<string, int> questionsAndAnswers, System.Windows.Forms.RichTextBox logs, System.Windows.Forms.RichTextBox scoreboard, bool quizStarted, bool questionFinished, bool terminating, Button listenButton)
        {
            this.quizStarted = quizStarted;
            this.questionAndAnswers = questionsAndAnswers;
            this.logs = logs;
            this.scoreboard = scoreboard;
            playerList = new List<Player>();
            this.terminating = terminating;
            this.listenButton = listenButton;
        }

        public void startQuiz(List<Player> playerListExternal)
        {
            playerList = playerListExternal;
            if (questionFinished)
            {
                lock(QuizLock)
                {
                    sendQuestion();
                    questionFinished = false;
                }
                
            }
        }

        public void sendQuestion()
        {
            questionText = questionAndAnswers.Keys.ElementAt(questionNo);
            Console.WriteLine(questionText);
            logs.AppendText(questionText + "\n");
            Byte[] buffer = Encoding.Default.GetBytes(questionText);
            foreach (Player player in playerList)
            {
                try
                {
                    player.socket.Send(buffer);
                }
                catch
                {
                    logs.AppendText("An error has occured \n");
                }
            }
        }

        public void recieveAnswer(Player currentPlayer)
        {
            Byte[] buffer = new Byte[256];
            bool answerRecieved = false;

            while (!terminating && !answerRecieved)
            {
                try
                {
                    if (quizStarted)
                    {
                        buffer = new Byte[256];
                        currentPlayer.socket.Receive(buffer);
                        string answer = Encoding.Default.GetString(buffer);
                        if (answer != null)
                        {
                            lock (QuizLock)
                            {
                                answer = answer.Trim('\n');
                                if (answer.Length > 0)
                                {
                                    logs.AppendText(currentPlayer.playerName + ": " + answer + '\n');
                                }
                                answerRecieved = true;
                                questionFinished = true;

                                AnswersList.Add(currentPlayer, Int32.Parse(answer));
                            }
                        }

                    }
                }
                catch
                {
                    if(!terminating)
                    {
                        logs.AppendText(currentPlayer.playerName + "has disconnected.\n");
                        quizStarted = false;
                        currentPlayer.socket.Close();
                        playerList.Remove(currentPlayer);
                    }
                }

            }
        }

        public void checkAnswers()
        {
            int numberOfCorrectAnswers = 0;
            int answer = questionAndAnswers[questionText];
            foreach (var PA in AnswersList)
            {
                if (PA.Value == answer)
                {
                    PA.Key.didAnswerCorrect = true;
                    numberOfCorrectAnswers++;
                }
                else
                {
                    PA.Key.didAnswerCorrect = false;
                }
            }
            if (numberOfCorrectAnswers > 1)
            {
                foreach (Player P in playerList)
                {
                    if (P.didAnswerCorrect)
                    {
                        P.playerScore += 1/numberOfCorrectAnswers;
                    }
                    logs.AppendText("The Round was a tie\n");
                }
            }
            else if (numberOfCorrectAnswers == 1)
            {
                foreach (Player P in playerList)
                {
                    if (P.didAnswerCorrect)
                    {
                        P.playerScore += 1;
                        logs.AppendText(P.playerName + "Won this round\n");
                    }
                }
            }
            else
            {
                logs.AppendText("There is no win of this round.\n");
            }
            scoreboard.AppendText("----Current Scores---\n");
            foreach (Player P in playerList)
            {
                scoreboard.AppendText(P.playerName + ": " + P.playerScore + '\n');
            }
        }

        public void EndGame()
        {
            double max = 0;
            string currentWinner = "";
            bool isTie = false;
            int tieCounter = 0;

            foreach (Player P in playerList)
            {
                if (P.playerScore > max)
                {
                    max = P.playerScore;
                    currentWinner = P.playerName;
                }
            }

            foreach (Player P in playerList)
            {
                if (P.playerScore == max)
                {
                    tieCounter++;
                }
            }

            if (tieCounter == 1)
            {
                logs.AppendText("The Winner is: " + currentWinner + '\n');
                playerList.Clear();
            }
            else if (tieCounter >= 2)
            {
                logs.AppendText("The Game is a Tie Between: \n");
                foreach (Player P in playerList)
                {
                    if (P.playerScore == max)
                    {
                        currentWinner = P.playerName;
                        logs.AppendText(currentWinner);
                    }
                }
                playerList.Clear();
            }

            string informative = "Game has ended, " + currentWinner + " is the winner. \n";
            Byte[] buffer = Encoding.Default.GetBytes(informative);
            lock (QuizLock)
            {
                foreach (Player P in playerList)
                {
                    P.socket.Send(buffer);
                    P.socket.Disconnect(false);
                }
            }
            terminating = false;
            scoreboard.Text = "";
            questionNo = 0;
            listenButton.Enabled = false;
        }
    }
    

}
