using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server_v2.include
{
    internal class Quiz
    {
        private bool quizStarted;
        private bool questionFinished;
        Dictionary<string, int> questionAndAnswers;
        private int questionNo = 0;
        private string questionText = " ";
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.RichTextBox scoreboard;
        List<Player> playerList;

        public Quiz(Dictionary<string, int> questionsAndAnswers, System.Windows.Forms.RichTextBox logs, System.Windows.Forms.RichTextBox scoreboard, bool quizStarted, bool questionFinished)
        {
            this.quizStarted = quizStarted;
            this.questionAndAnswers = questionsAndAnswers;
            this.logs = logs;
            this.scoreboard = scoreboard;
            playerList = new List<Player>();
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
    }
}
