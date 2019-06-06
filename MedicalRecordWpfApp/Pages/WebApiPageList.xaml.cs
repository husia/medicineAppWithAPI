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
    /// Логика взаимодействия для WebApiPageList.xaml
    /// </summary>
    public partial class WebApiPageList : Window
    {
        ObservableCollection<WebModelPacient> list = new ObservableCollection<WebModelPacient>();
        WebApiService webApiService = new WebApiService();

        public WebApiPageList()
        {
            InitializeComponent();
            
                var list2 = webApiService.GetPacients();
                foreach (var item in list2)
                    list.Add(item);
                datagrid.ItemsSource = list;
           
            
        }
        private void Datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var webModel = (WebModelPacient)datagrid.SelectedItem;
            MedicalModel pacient = new MedicalModel()
            {
                Name = webModel.Name,
                Age = webModel.Age,
                Diagnos = webModel.Diagnos
            };
            var targetWindow = Application.Current.Windows.Cast<Window>().
               FirstOrDefault(w => w is PacientPage) as PacientPage;
            targetWindow.ChangeNameAge = pacient;
            
        }
    }
}
