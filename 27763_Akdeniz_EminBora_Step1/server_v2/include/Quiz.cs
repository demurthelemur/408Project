using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server_v2.include
{
    internal class Quiz
    {
        public bool quizStarted;
        public bool questionFinished;
        Dictionary<string, int> questionAndAnswers;
        private int questionNo = 0;
        private string questionText = " ";
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.RichTextBox scoreboard;
        List<Player> playerList;
        private static readonly object QuizLock = new object();

        private bool terminating;

        public Quiz(Dictionary<string, int> questionsAndAnswers, System.Windows.Forms.RichTextBox logs, System.Windows.Forms.RichTextBox scoreboard, bool quizStarted, bool questionFinished, bool terminating)
        {
            this.quizStarted = quizStarted;
            this.questionAndAnswers = questionsAndAnswers;
            this.logs = logs;
            this.scoreboard = scoreboard;
            playerList = new List<Player>();
            this.terminating = terminating;
        }

        public void startQuiz(List<Player> playerListExternal)
        {
            playerList = playerListExternal;
            if (questionFinished)
            {
                sendQuestion();
                questionFinished = false;
            }
        }

        public void sendQuestion()
        {
            questionText = questionAndAnswers.Keys.ElementAt(questionNo);
            logs.AppendText(questionText + '\n');
            Byte[] buffer = Encoding.Default.GetBytes(questionText);
            foreach (Player player in playerList)
            {
                try
                {
                    player.socket.Send(buffer);
                } catch
                {
                    logs.AppendText("An error has occured \n");
                }
            }
        }

        public void recieveAnswer(Player currentPlayer)
        {
            Byte[] buffer = new Byte[256];
            bool answerRecieved = false;

            while(!terminating && !answerRecieved)
            {
                if(quizStarted)
                {
                    buffer = new Byte[256];
                    currentPlayer.socket.Receive(buffer);
                    string answer = Encoding.Default.GetString(buffer);
                    if (answer != null)
                    {
                        lock(QuizLock)
                        {
                            // TODO: 
                        }
                    }

                }
            }
        }
    }
}
