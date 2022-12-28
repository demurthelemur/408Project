using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server_v2.include
{
    internal class QuestionsandAnswers
    {
        public Dictionary<String, int> QandADictionary;

        public QuestionsandAnswers() { QandADictionary = new Dictionary<String, int>(); }

        public void addQandAtoDictionary()
        {
            using (StreamReader sr = new StreamReader("questions.txt"))
            {
                string question;
                int answer;
                while ((question = sr.ReadLine()) != null)
                {
                    answer = Int32.Parse(sr.ReadLine());
                    QandADictionary.Add(question, answer);
                }
            }
        }
    }
}
