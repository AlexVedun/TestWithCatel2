using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestWithCatel.Models;

namespace TestWithCatel.Entity
{
    // класс для загрузки вопросов из XML-файла
    class XMLQuestionsReader : IQuestionsReader
    {
        private XDocument mXMLFile;
        // получить Id темы по названию       
        public int GetIdByTheme(string _theme)
        {
            return (int)mXMLFile.Descendants("Theme").Where(e => ((string)e.Attribute("Text") == _theme)).Attributes("id").FirstOrDefault();
        }
        // получить вопросы для темы по ее Id
        public List<Question> GetQuestions(int _themeId)
        {
            List<Question> questions = new List<Question>();
            var elements = mXMLFile.Descendants("Theme").Where(e => ((int)e.Attribute("id") == _themeId)).Descendants("Question");
            foreach (var item in elements)
            {
                Question question = new Question();
                question.Id = (int)item.Attribute("id");
                question.Id_Theme = _themeId;
                question.Text = (string)item.Attribute("Text");
                int answerId = 1;
                foreach (var answers in item.Descendants("Item"))
                {
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
        // получить тему по ее Id
        public string GetThemeById(int _id)
        {
            return (string)mXMLFile.Descendants("Theme").Where(e => ((int)e.Attribute("id") == _id)).Attributes("Text").FirstOrDefault();
        }
        // получить список тем
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
        // получить список Id тем
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
        // открыть файл
        public void Open(string _cs)
        {
            mXMLFile = XDocument.Load(_cs);
        }
    }
}
