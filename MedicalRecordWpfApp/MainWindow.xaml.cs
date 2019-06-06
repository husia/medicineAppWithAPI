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
using MedicalRecordWpfApp.Data;
using MedicalRecordWpfApp.Pages;
using MedicalRecordWpfApp.Models;
using System.Collections.ObjectModel;

namespace MedicalRecordWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        DataContext db = new DataContext();
        ObservableCollection<DbPacientModel> list = new ObservableCollection<DbPacientModel>();
        public MainWindow()
        {

            InitializeComponent();
            var list2 = db.PacientsDb.ToList();
            foreach (var item in list2)
                list.Add(item);
            datagrid.ItemsSource = list;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PacientPage pp = new PacientPage();
            pp.Show();


        }

        private void Datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var pacient = (DbPacientModel)datagrid.SelectedItem;
            PacientPageAll pp = new PacientPageAll(pacient);
            pp.Show();
        }
        private void clickDelete(object sender, RoutedEventArgs e)
        {
            dynamic er = datagrid.SelectedItem;

            var message = MessageBox.Show("jhl", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            if (message == MessageBoxResult.Yes)
            {
                list.Remove((DbPacientModel)datagrid.SelectedItem);
            }
            datagrid.ItemsSource = list;
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            var list2 = db.PacientsDb.ToList();
            list.Clear();
            foreach (var item in list2)
                list.Add(item);
            datagrid.ItemsSource = list;
        }

        private void ChangingTextBlock(object sender, TextChangedEventArgs e)
        {
            datagrid.ItemsSource = list.Where(x => x.Name.Contains(findTextBlock.Text));
        }
    }
}
