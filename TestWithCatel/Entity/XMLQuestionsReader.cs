using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWithCatel.Models;

namespace TestWithCatel.Entity
{
    class XMLQuestionsReader : IQuestionsReader
    {
        private string mFileName;
        private XDocument mXMLFile;

        public XMLQuestionsReader()
        {

        }

        public XMLQuestionsReader(string _fileName)
        {

        }

        public int GetIdByTheme(string _theme)
        {
            return (int)mXMLFile.Descendants("Theme").Where(e => ((string)e.Attribute("Text") == _theme)).Attributes("id").FirstOrDefault();
        }

        public List<Question> GetQuestions(int _themeId)
        {
            List<Question> questions = new List<Question>();
            var elements = mXMLFile.Descendants("Theme").Where(e => ((int)e.Attribute("id") == _themeId)).Descendants("Question");
            foreach (var item in elements)
            {
                Question question = new Question();
                //Console.WriteLine((int)item.Attribute("id"));
                //Console.WriteLine((string)item.Attribute("Text"));
                question.Id = (int)item.Attribute("id");
                question.Id_Theme = _themeId;
                question.Text = (string)item.Attribute("Text");
                int answerId = 1;
                foreach (var answers in item.Descendants("Item"))
                {
                    //Console.WriteLine((string)answers);
                    //Console.WriteLine((bool)answers.Attribute("isRight"));
                    OptionModel answer = new OptionModel();
                    answer.Id = answerId;
                    answer.Name = (string)answers;
                    answer.IsCorrect = (bool)answers.Attribute("isRight");
                    question.Options.Add(answer);
                    answerId++;
                }
                questions.Add(question);
            }
            return questions;
        }

        public string GetThemeById(int _id)
        {
            return (string)mXMLFile.Descendants("Theme").Where(e => ((int)e.Attribute("id") == _id)).Attributes("Text").FirstOrDefault();
        }

        public List<string> GetThemes()
        {
            var elements = mXMLFile.Descendants("Theme");
            List<string> themes = new List<string>();
            foreach (var item in elements)
            {
                themes.Add((string)item.Attribute("Text"));
            }
            return themes;
        }

        public List<int> GetThemesId()
        {
            List<int> themsId = new List<int>();
            var themes = mXMLFile.Descendants("Theme");
            foreach (var item in themes)
            {
                themsId.Add((int)item.Attribute("id"));
            }
            return themsId;
        }

        public void Open(string _cs)
        {
            mXMLFile = XDocument.Load(_cs);
        }
    }
}
