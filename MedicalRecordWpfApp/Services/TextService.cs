using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordWpfApp.Data
{
    class TextService
    {
        public  string MainPath = "C:\\Users\\Husia\\Documents\\Pacients\\"; // основной путь к папке 
        public   string TemplatePath = "C:\\Users\\Husia\\Documents\\Pacients\\Templates\\"; // путь к папке из шаблонами файлов
        public string GetDataFromDoc(string start, string end, string[] arrayText) // метод для разделения и получения текста из текстового файла
        {
            string mytxt = "";
            bool ok = false;
            for (int i = 0; i < arrayText.Length; i++)
            {
                if (arrayText[i] == end)
                {
                    ok = false;
                }
                if (ok)
                {
                    mytxt += " " + arrayText[i];
                }
                if (arrayText[i] == start)
                {
                    ok = true;
                }

            }
            return mytxt;
        }
        public void ReplaceWords(string findText, 
            string replaceText, 
            Microsoft.Office.Interop.Word.Document wordDoc) //метод для постановки текста из модлеи в docx файл
        {
            var range = wordDoc.Content;
            range.Find.ClearFormatting();

            range.Find.Execute(findText);
            range.Text = replaceText;
        }
    }
}
