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

namespace MedicalRecordWpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для EpicrisisPage.xaml
    /// </summary>
    public partial class EpicrisisPage : Window
    {
        
        public delegate void MyMethodConteiner();
        public event MyMethodConteiner OnChanged;
        private int Id;
        private bool added;
        public bool Added
        {
            get { return added; }
            set { added = value;
               if (OnChanged !=null)
                    OnChanged();
            }
        }

        public EpicrisisPage(EpicrisisModel model, int pacientId)
        {
            InitializeComponent();
            Id = pacientId;
            MedicalGrid.DataContext = model;
            
            
        }

        private void clickedSaveButton(object sender, RoutedEventArgs e)
        {
            RtfTemplateService template = new RtfTemplateService();
            template.SavePacientEpicrisis((EpicrisisModel)MedicalGrid.DataContext, Id);
            var targetWindow = Application.Current.Windows.Cast<Window>().
              FirstOrDefault(w => w is PacientPageAll) as PacientPageAll;
            targetWindow.Change = "loh";
        }

        private void saveToDoctemplateAction(object sender, RoutedEventArgs e)
        {
            DocTemplateService dC = new DocTemplateService();
            EpicrisisModel md = MedicalGrid.DataContext as EpicrisisModel;
            dC.CopyFileFirstViewToDocFirstView(md.Name);
            dC.AddToTemplateEpicrisDoc(md);
            Added = false;
           var targetWindow = Application.Current.Windows.Cast<Window>().
                FirstOrDefault(w => w is PacientPageAll) as PacientPageAll;
            targetWindow.Change = "loh";
            
            
        }

        
    }
}
