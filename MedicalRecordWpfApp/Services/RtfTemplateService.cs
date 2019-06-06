using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalRecordWpfApp.Models;
using System.IO;
namespace MedicalRecordWpfApp.Data
{
    class RtfTemplateService
    {
        private DataContext db; //база данных
        TextService tS; 
        public RtfTemplateService()
        {
            tS = new TextService();
            db = new DataContext();
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

                Recomendation = tS.GetDataFromDoc("Рекомендации:", "end", needTxt),
                Treatment = tS.GetDataFromDoc("ПроведенноеЛечение:", "end", needTxt),
                Researches = tS.GetDataFromDoc("Обследования:", "end", needTxt),
                SecondDiagnos = tS.GetDataFromDoc("СопутствующийДиагноз:", "end", needTxt),
                DeliveryDate = tS.GetDataFromDoc("ПриПоступлении:", "end", needTxt),
                Anamnes = tS.GetDataFromDoc("Анамнез:", "end", needTxt),
                StatusPraesens = tS.GetDataFromDoc("ОбщийСтатус:", "end", needTxt),
                Diagnos = tS.GetDataFromDoc("ОсновнойДиагноз:", "end", needTxt)
            };

            return md;
        } // метод для получения текста и системного файла с информацией об первичном осмотре
        public void SavePacient(MedicalModel md, int id)
        {
            string text = "Больной " + md.Name +" end"+ "\n" + "\n" + "Жалобы: " + md.Complaints +" end"+ 
                "\n" + "Анамнез: " + md.Anamnes +" end"+ "\n" + "ОбщийСтатус: " + md.StatusPraesens +" end"+
                "\n" + "МестныйСтатус: " + md.LocalStatus +" end"+ "\n" + "ПредварительныйДиагноз: " + md.Diagnos+" end";
            string name = md.Name;
            var path = tS.TemplatePath + $"{name}первичный.rtf";
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
            if (!File.Exists(tS.TemplatePath + $"{name}первичный.rtf"))
            {
                FileStream fileStream = new FileStream(tS.TemplatePath + $"{name}первичный.rtf", FileMode.CreateNew);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(text);
                writer.Close();
                fileStream.Close();
            }
            else
            {
                File.WriteAllText(tS.TemplatePath + $"{name}первичный.rtf", string.Empty);
                FileStream fileStream = new FileStream(tS.TemplatePath + $"{name}первичный.rtf", FileMode.Open);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(text);
                writer.Close();
                fileStream.Close();
            }


        } //метод для сохранения файла в системном файле первичного осмотра
        public MedicalModel AddFromFileDbFileRtfFirstView(string path)
        {
            string str = string.Empty;
            using (System.IO.StreamReader reader = System.IO.File.OpenText(path))
            {
                str = reader.ReadToEnd();
            }
            string[] needTxt = str.Split(new char[] { ' ', '\n', '\r' });
            MedicalModel md = new MedicalModel
            {
                Name  = tS.GetDataFromDoc("Больной", "end", needTxt),
                Complaints = tS.GetDataFromDoc("Жалобы:", "end", needTxt),
                LocalStatus = tS.GetDataFromDoc("МестныйСтатус:", "end", needTxt),
                Anamnes = tS.GetDataFromDoc("Анамнез:", "end", needTxt),
                StatusPraesens = tS.GetDataFromDoc("ОбщийСтатус:", "end", needTxt),
                Diagnos = tS.GetDataFromDoc("ПредварительныйДиагноз:", "end", needTxt)
            };

            return md;
        } // метод для получения текста и системного файла с информацией об первичном осмотре

        public void SavePacientEpicrisis(EpicrisisModel md, int id)
        {
            string text = "Больной " + md.Name + " end" + "\n" + "\n" + "ПриПоступлении: " + md.DeliveryDate + " end" +
                "\n" + "ОсновнойДиагноз: " + md.Diagnos + " end" + "СопутствующийДиагноз: " +md.SecondDiagnos +" end"+ "\n"
                + "Анамнез: " + md.Anamnes + " end" + "\n" + "ОбщийСтатус: " + md.StatusPraesens + " end" + "Обследования: "+ md.Researches + " end" +
                "\n" +"ПроведенноеЛечение: " + md.Treatment +" end" + "\n" + "Рекомендации: " + md.Recomendation + " end" + "\n";
            string name = md.Name;
            var path = tS.TemplatePath + $"{name}выписка.rtf";
            if (id != 0)
            {
                db.PacientsDb.Where(p => p.Id == id).FirstOrDefault().EpicrisisFile = path;
                //var pacient = db.PacientsDb.Where(p => p.Id == id).FirstOrDefault();
                //pacient.EpicrisisFile = path;
                //db.PacientsDb.Add(pacient);
                db.SaveChanges();
            }
            else
            {

                db.PacientsDb.Add(new DbPacientModel { Name = md.Name, Age = md.Age, Diagnos = md.Diagnos, FirstViewFile = path });
                db.SaveChanges();

            }
            if (!File.Exists(tS.TemplatePath + $"{name}выписка.rtf"))
            {
                FileStream fileStream = new FileStream(tS.TemplatePath + $"{name}выписка.rtf", FileMode.CreateNew);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(text);
                writer.Close();
                fileStream.Close();
            }
            else
            {
                File.WriteAllText(tS.TemplatePath + $"{name}выписка.rtf", string.Empty);
                FileStream fileStream = new FileStream(tS.TemplatePath + $"{name}выписка.rtf", FileMode.Open);
                StreamWriter writer = new StreamWriter(fileStream);
                writer.Write(text);
                writer.Close();
                fileStream.Close();
            }


        }// метод для сохранения системного файла содержащего выписной эпикриз



    }
}
