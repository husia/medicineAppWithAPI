using MedicalRecordWpfApp.Data;
using MedicalRecordWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecordWpfApp.Services
{
    class TemplateService
    {
        TextService tS = new TextService();
       
        public string AddFromTemplatesFile(string teplateName)
        {
            string text = "loh";
            string pathofTemplate = tS.TemplatePath + "Templates.rtf";
           
            using (System.IO.StreamReader reader = System.IO.File.OpenText(pathofTemplate))
            {
                text = reader.ReadToEnd();
            }
            string[] needTxt = text.Split(new char[] { ' ', '\n', '\r' });

            string str = tS.GetDataFromDoc(teplateName, "end", needTxt);
               
            return str;
        }
    }
}
