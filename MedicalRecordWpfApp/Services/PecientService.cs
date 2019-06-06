using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Word = Microsoft.Office.Interop.Word;
using MedicalRecordWpfApp.Models;
using System.IO;

namespace MedicalRecordWpfApp.Data
{
    class PecientService
    {
        private DataContext db = new DataContext();
        public void AddFromFile(ref TextBox textbox)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;
            object path = @"C:\Users\Husia\Documents\test.docx";
            object readOnly = true;
            Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            string allText = docs.Content.Text;
            string[] needTxt = allText.Split(new char[] { ' ', '\n', '\r' });
            string mytxt = "";
            bool ok = false;
            for (int i = 0; i < needTxt.Length; i++)
            {
                if (ok)
                {
                    mytxt += " " + needTxt[i];
                }
                if (needTxt[i].ToLower() == "анамнез")
                {
                    ok = true;
                }
                if (needTxt[i] == "статус")
                {
                    ok = false;
                }
            }
            textbox.Text = mytxt;
            docs.Close();
            word.Quit();
        } //нет ссылок
        public MedicalModel AddFromFileClass(object path)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;

            object readOnly = true;
            Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            string allText = docs.Content.Text;
            string[] needTxt = allText.Split(new char[] { ' ', '\n', '\r' });

            MedicalModel md = new MedicalModel
            {
                Complaints = GetDataFromDoc("Жалобы:", "Anamnes:", needTxt),
                Anamnes = GetDataFromDoc("Anamnes:", "StatusPraesens:", needTxt),
                StatusPraesens = GetDataFromDoc("StatusPraesens:", "LocalStatus:", needTxt),
                LocalStatus = GetDataFromDoc("LocalStatus:", "Diagnos:", needTxt),
                Diagnos = GetDataFromDoc("Diagnos:", "Рекомендации:", needTxt)
            };

            docs.Close();
            word.Quit();
            return md;
        }// doc
        public MedicalModel AddFromFileDbFile(object path)// doc doctemplateservice
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;

            object readOnly = true;
            Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            string allText = docs.Content.Text;
            string[] needTxt = allText.Split(new char[] { ' ', '\n', '\r' });

            MedicalModel md = new MedicalModel
            {
                Complaints = GetDataFromDoc("Жалобы:", "Анамнез:", needTxt),
                Anamnes = GetDataFromDoc("Анамнез:", "ОбщийСтатус:", needTxt),
                StatusPraesens = GetDataFromDoc("ОбщийСтатус:", "МестныйСтатус:", needTxt),
                LocalStatus = GetDataFromDoc("МестныйСтатус:", "ПредварительныйДиагноз:", needTxt),
                Diagnos = GetDataFromDoc("ПредварительныйДиагноз:", "Рекомендации:", needTxt)
            };

            docs.Close();
            word.Quit();
            return md;
        }
        public MedicalModel AddFromFileDbFileRtf(object path)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;

            object readOnly = true;
            Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            string allText = docs.Content.Text;
            string[] needTxt = allText.Split(new char[] { ' ', '\n', '\r' });

            MedicalModel md = new MedicalModel
            {
                Complaints = GetDataFromDoc("Жалобы:", "Анамнез:", needTxt),
                Anamnes = GetDataFromDoc("Анамнез:", "ОбщийСтатус:", needTxt),
                StatusPraesens = GetDataFromDoc("ОбщийСтатус:", "МестныйСтатус:", needTxt),
                LocalStatus = GetDataFromDoc("МестныйСтатус:", "ПредварительныйДиагноз:", needTxt),
                Diagnos = GetDataFromDoc("ПредварительныйДиагноз:", "Рекомендации:", needTxt)
            };

            docs.Close();
            word.Quit();
            return md;
        }// doc no ref


        private string GetDataFromDoc(string start, string end, string[] arrayText)
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
        } //textService

        public void SavePacient(MedicalModel md, int id)
        {
            string text = "ФИО:" + md.Name + "\n" + "\n" + "Жалобы:" + md.Complaints + "\n" + "Анамнез:" + md.Anamnes + "\n" + "ОбщийСтатус:" + md.StatusPraesens +
                "\n" + "МестныйСтатус:" + md.LocalStatus + "\n" + "ПредварительныйДиагноз:" + md.Diagnos;
            string name = md.Name;
            var path = $"C:\\Users\\Husia\\Documents\\Pacients\\Templates\\{name}первичный.rtf";
            if (id != 0)
            {
                var pacient = db.PacientsDb.Where(p => p.Id == id).FirstOrDefault();
                pacient.FirstViewFile = path;
                db.PacientsDb.Add(pacient);
                db.SaveChanges();
            }
            else
            {

                db.PacientsDb.Add(new DbPacientModel { Name = md.Name, Age = md.Age, Diagnos = md.Diagnos, FirstViewFile = path });
                db.SaveChanges();

            }
            if (!File.Exists($"C:\\Users\\Husia\\Documents\\Pacients\\Templates\\{name}.rtf"))
            {
                FileStream fileStream = new FileStream($"C:\\Users\\Husia\\Documents\\Pacients\\Templates\\{name}.rtf", FileMode.CreateNew);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(text);
                writer.Close();
                fileStream.Close();
            }


        } //RtfTemplateService

        public void CopyFileFirstViewToDocFirstView(string fileName)
        {
            if (!Directory.Exists($"C:\\Users\\Husia\\Documents\\Pacients\\{fileName}"))
            {
                Directory.CreateDirectory($"C:\\Users\\Husia\\Documents\\Pacients\\{fileName}");
            }
            else { }
            try
            {
                File.Copy($"C:\\Users\\Husia\\Documents\\Pacients\\Templates\\TemplateFirst.docx",
                    $"C:\\Users\\Husia\\Documents\\Pacients\\{fileName}\\{fileName}.docx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } //DocTemplateService

        public void AddToTemplateFirstDoc(MedicalModel model)// DocTemplateService
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            word.Visible = false;
            var document = word.Documents.Open($"C:\\Users\\Husia\\Documents\\Pacients\\{model.Name}\\{model.Name}первичный.docx");
            ReplaceWords("{name}", model.Name, document);
            ReplaceWords("{Anamnes}", model.Anamnes, document);
            ReplaceWords("{StatusPraesens}", model.StatusPraesens, document);
            ReplaceWords("{LocalStatus}", model.LocalStatus, document);
            ReplaceWords("{Diagnos}", model.Diagnos, document);
            document.Save();
            document.Close();
            word.Quit();


        }
        private void ReplaceWords(string findText, string replaceText, Word.Document wordDoc)
        {
            var range = wordDoc.Content;
            range.Find.ClearFormatting();

            range.Find.Execute(findText);
            range.Text = replaceText;
        }//textService

        private void SavePAcientToDb()
        { }//empty

        private void WriteFirstViewInRtfTemplate(List<string> list, string str, string path)
        {
            foreach (var a in list)
            {
                var b = a.Split(' ');

                str = str.Replace(b[0], b[1]);

            }
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(str);
            }
        }


        public EpicrisisModel AddFromFileDbFileRtfEpicris(string path)
        {
            string str = string.Empty;
            using (System.IO.StreamReader reader = System.IO.File.OpenText(path))
            {
                str = reader.ReadToEnd();
            }
            string[] needTxt = str.Split(new char[] { ' ', '\n', '\r' });
            EpicrisisModel md = new EpicrisisModel
            {
                
                Recomendation = GetDataFromDoc("Рекомендации:", "end", needTxt),
                Treatment = GetDataFromDoc("ПроведенноеЛечение:", "end", needTxt),
                Researches = GetDataFromDoc("Обследования:", "end", needTxt ),
                SecondDiagnos = GetDataFromDoc("СопутствующийДиагноз:", "end", needTxt),
                DeliveryDate = GetDataFromDoc("ПриПоступлении:", "end", needTxt ),
                Anamnes = GetDataFromDoc("Анамнез:", "end", needTxt),
                StatusPraesens = GetDataFromDoc("ОбщийСтатус:", "end", needTxt),
                Diagnos = GetDataFromDoc("ОсновнойДиагноз:", "end", needTxt)
            };

            return md;
        } //RtfTemplateService
    }
}
