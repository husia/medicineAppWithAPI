using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MedicalRecordWpfApp.Models;

namespace MedicalRecordWpfApp.Data
{
    class DocTemplateService
    {
        TextService tS;
        public DocTemplateService()
        {
            tS = new TextService();
        }
        public void AddToTemplateFirstDoc(MedicalModel model) //сохранение в docx файле первичного осмотра
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            word.Visible = false;
            var document = word.Documents.Open(tS.MainPath+$"{model.Name}\\{model.Name}первичный.docx");
            tS.ReplaceWords("{name}", model.Name, document);
            tS.ReplaceWords("{Anamnes}", model.Anamnes, document);
            tS.ReplaceWords("{StatusPraesens}", model.StatusPraesens, document);
            tS.ReplaceWords("{LocalStatus}", model.LocalStatus, document);
            tS.ReplaceWords("{Diagnos}", model.Diagnos, document);
            document.Save();
            document.Close();
            word.Quit();


        }
        public void CopyFileFirstViewToDocFirstView(string fileName)
        {
            if (!Directory.Exists(tS.MainPath + $"{fileName}"))
            {
                Directory.CreateDirectory(tS.MainPath + $"{fileName}");
            }
            else { }
            try
            {
                File.Copy(tS.TemplatePath + "TemplateEpicrisis.docx", 
                    tS.MainPath + $"{fileName}\\{fileName}выписка.docx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public MedicalModel AddFromFileDbFile(object path)// doc doctemplateservice - заменить
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            object miss = System.Reflection.Missing.Value;

            object readOnly = true;
            Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
            string allText = docs.Content.Text;
            string[] needTxt = allText.Split(new char[] { ' ', '\n', '\r' });

            MedicalModel md = new MedicalModel
            {
                Complaints = tS.GetDataFromDoc("Жалобы:", "Анамнез:", needTxt),
                Anamnes = tS.GetDataFromDoc("Анамнез:", "ОбщийСтатус:", needTxt),
                StatusPraesens = tS.GetDataFromDoc("ОбщийСтатус:", "МестныйСтатус:", needTxt),
                LocalStatus = tS.GetDataFromDoc("МестныйСтатус:", "ПредварительныйДиагноз:", needTxt),
                Diagnos = tS.GetDataFromDoc("ПредварительныйДиагноз:", "Рекомендации:", needTxt)
            };

            docs.Close();
            word.Quit();
            return md;
        }
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
                Complaints = tS.GetDataFromDoc("Жалобы:", "Anamnes:", needTxt),
                Anamnes = tS.GetDataFromDoc("Anamnes:", "StatusPraesens:", needTxt),
                StatusPraesens = tS.GetDataFromDoc("StatusPraesens:", "LocalStatus:", needTxt),
                LocalStatus = tS.GetDataFromDoc("LocalStatus:", "Diagnos:", needTxt),
                Diagnos = tS.GetDataFromDoc("Diagnos:", "Рекомендации:", needTxt)
            };

            docs.Close();
            word.Quit();
            return md;
        }// doc

        public void AddToTemplateEpicrisDoc(EpicrisisModel model)
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            word.Visible = false;
            var document = word.Documents.Open(tS.MainPath + $"{model.Name}\\{model.Name}выписка.docx");
            tS.ReplaceWords("{name}", model.Name, document);
            tS.ReplaceWords("{Anamnes}", model.Anamnes, document);
            tS.ReplaceWords("{StatusPraesens}", model.StatusPraesens, document);
            tS.ReplaceWords("{DeliveryDate}", model.DeliveryDate, document);
            tS.ReplaceWords("{Diagnos}", model.Diagnos, document);
            tS.ReplaceWords("{treatment}", model.Treatment, document);
            tS.ReplaceWords("{recomendation}", model.Recomendation, document);
            tS.ReplaceWords("{Researches}", model.Researches, document);
            tS.ReplaceWords("{SecondDiagnos}", model.SecondDiagnos, document);
            document.Save();
            document.Close();
            word.Quit();
        }
        public void CopyFileFirstViewToDocEpicris(string fileName)
        {
            if (!Directory.Exists(tS.MainPath + $"{fileName}"))
            {
                Directory.CreateDirectory(tS.MainPath + $"{fileName}");
            }
            else { }
            try
            {
                File.Copy(tS.TemplatePath+$"TemplateFirst.docx",
                   tS.MainPath+$"{fileName}\\{fileName}.docx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
