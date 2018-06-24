using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWithCatel.Entity;

namespace TestWithCatel
{
    interface IQuestionsReader
    {
        void Open(string _cs);
        List<int> GetThemesId();
        List<string> GetThemes();
        int GetIdByTheme(string _theme);
        string GetThemeById(int _id);
        List<Question> GetQuestions(int _themeId);
    }
}
