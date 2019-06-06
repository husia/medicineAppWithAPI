using MedicalRecordWpfApp.Data;
using MedicalRecordWpfApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using MedicalRecordWpfApp.Data;

namespace MedicalRecordWpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PacientPageAll.xaml
    /// </summary>
    public partial class PacientPageAll : Window
    {
        RtfTemplateService rtfTemplate = new RtfTemplateService();
        private string change;
        DataContext db = new DataContext();

        DocTemplateService dC = new DocTemplateService();
        DbPacientModel pacient = new DbPacientModel();
        ObservableCollection<string> myList = new ObservableCollection<string>();
        public delegate void mythodConteiner(ObservableCollection<string> list);
        public event mythodConteiner OnChanged;
        public string Change
        { get { return change; }
            set { change = value;
                OnChanged(myList);
            }
        }
        public PacientPageAll(DbPacientModel dbPacient)
        {
            InitializeComponent();
          
            pacient = dbPacient;
            CheckList(myList);
            OnChanged += PacientPageAll_OnChanged;
            

        }
       
        
        private void CheckList(ObservableCollection<string> myList)
        {
            if (pacient.FirstViewFile != null)
            {
                myList.Add("Первичный осмотр");
            }
            if (pacient.EpicrisisFile != null)
            {
                myList.Add("Выписной эпикриз");
                epicrisButton.IsEnabled = false;
            }
            if (myList.Count != 0)
            {
                myListView.ItemsSource = myList;
            }
        }

        private void Clicked(object sender, MouseButtonEventArgs e)
        {
            if (myListView.SelectedItem.ToString() == "Первичный осмотр")
            {
                MedicalModel model = rtfTemplate.AddFromFileDbFileRtfFirstView(pacient.FirstViewFile);
                model.Age = pacient.Age;
                model.Name = pacient.Name;
                PacientPage pp = new PacientPage(model, pacient.Id);
                pp.Show();
                
            }
            if (myListView.SelectedItem.ToString() == "Выписной эпикриз")
            {
                EpicrisisModel model = rtfTemplate.AddFromFileDbFileRtfEpicris(pacient.EpicrisisFile);
                model.Age = pacient.Age;
                model.Name = pacient.Name;
                EpicrisisPage ep = new EpicrisisPage(model, pacient.Id);
                ep.Show();
               
                ep.OnChanged += PacientPageAll_OnChanged2;
                


            }

        }

        private void PacientPageAll_OnChanged2()
        {
            MessageBox.Show("Loshara");
        }
        private void PacientPageAll_OnChanged(ObservableCollection<string> list)
        {
           var thisPacient = db.PacientsDb.Where(p => p.Id == pacient.Id).FirstOrDefault();
            if (thisPacient.FirstViewFile != null)
            {
                if (!myList.Contains("Первичный осмотр"))
                {
                    myList.Add("Первичный осмотр");
                }
            }
            if (thisPacient.EpicrisisFile != null)
            {
                if (!myList.Contains("Выписной эпикриз"))
                {
                    myList.Add("Выписной эпикриз");
                }
                
                
            }
            if (myList.Count != 0)
            {
                myListView.ItemsSource = myList;
            }
        }

        private void clickedEpicris(object sender, RoutedEventArgs e)
        {
            MedicalModel model = rtfTemplate.AddFromFileDbFileRtfFirstView(pacient.FirstViewFile);
            EpicrisisModel epModel = new EpicrisisModel
            {
                Anamnes = model.Anamnes, Age =pacient.Age, Name = pacient.Name, 
                Diagnos = model.Diagnos, StatusPraesens = model.StatusPraesens
            };

            EpicrisisPage epPage = new EpicrisisPage(epModel, pacient.Id);
            epPage.Show();
        }

        
    }
}
