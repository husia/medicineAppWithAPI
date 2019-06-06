using System;
using System.Collections.Generic;
using System.IO;
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
using MedicalRecordWpfApp.Models;
using MedicalRecordWpfApp.Services;
using Microsoft.Win32;

namespace MedicalRecordWpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PacientPage.xaml
    /// </summary>
    public partial class PacientPage : Window
    {
        RtfTemplateService rtfTemplate = new RtfTemplateService();
        DocTemplateService dC = new DocTemplateService();
        private int pacientId = 0;
        private MedicalModel changeNameAge;
        TemplateService templateService = new TemplateService();

        public delegate void MethodForChageName(MedicalModel pacient);
        public event MethodForChageName OnChangedNameAndAge;
        public MedicalModel ChangeNameAge
        {
            get { return changeNameAge; }
            set
            {
                changeNameAge = value;
                OnChangedNameAndAge(changeNameAge);
            }
        }

        public PacientPage()
        {
            InitializeComponent();
            MedicalGrid.DataContext = new MedicalModel();
            OnChangedNameAndAge += ChangeName;

        }
        private void ChangeName(MedicalModel pacient)
        {
           
            MedicalGrid.DataContext = pacient;
        }

        public PacientPage(MedicalModel pacient, int Id)
        {
            InitializeComponent();
            pacientId = Id;
            MedicalGrid.DataContext = pacient;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Title = "open new file",
                Filter = ".docx(Ms Word file)|*.docx"
            };
            if (openFile.ShowDialog() == true)
            {
                string directoryPath = System.IO.Path.GetFullPath(openFile.FileName);
                MedicalGrid.DataContext = dC.AddFromFileClass(directoryPath);
            }


            //OpenFileDialog oponFile = new OpenFileDialog
            //{
            //    Title = "open new file",
            //    Filter = ".doc(MSWord Files)|*.docx"
            //};
            //MedicalGrid.DataContext = ps.AddFromFileClass();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MedicalModel md = MedicalGrid.DataContext as MedicalModel;

            rtfTemplate.SavePacient(md, pacientId);


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MedicalModel md = MedicalGrid.DataContext as MedicalModel;
            dC.CopyFileFirstViewToDocFirstView(md.Name);
            dC.AddToTemplateFirstDoc(md);
        }

        private void ClickToServer(object sender, RoutedEventArgs e)
        {
            WebApiService ws = new WebApiService();
            if (ws.GetClient().BaseAddress != null)
            {
                WebApiPageList wl = new WebApiPageList();
                wl.Show(); }
            else
            {
                MessageBox.Show("нет содинения с сервером!");
            }
            
           
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            string item = selectedItem.Content.ToString().Replace(" ", string.Empty) + ":";

            if (item == "Общийстатус:")
            {
                ((MedicalModel)MedicalGrid.DataContext).StatusPraesens = templateService.AddFromTemplatesFile(item);
            }
            else if (item == "Локальныйстатус:")
            {
                ((MedicalModel)MedicalGrid.DataContext).LocalStatus = templateService.AddFromTemplatesFile(item);
            }
            else if (item == "Анамнез:")
            {
                ((MedicalModel)MedicalGrid.DataContext).Anamnes = templateService.AddFromTemplatesFile(item);
            }
            //switch (switch_on)
            //{
            //    case "Общий статус":

            //}
        }
    }
}
