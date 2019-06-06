using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MedicalRecordWpfApp.Data;

namespace MedicalRecordWpfApp.Models
{
    public class MedicalModel : IPacient, INotifyPropertyChanged
    {
        private string _statusPraesens;
        private string _localStatus;
        private string _anamnes;

        public string Name { get; set; }
        public string Complaints { get; set; }
        public string Anamnes
        {
            get => _anamnes; set
            {
                _anamnes = value;
                OnPropertyChanged("Anamnes");
            }
        }
        public string LocalStatus
        {
            get => _localStatus; set
            {
                _localStatus = value;
                OnPropertyChanged("LocalStatus");
            }
        }
        public string StatusPraesens
        {
            get => _statusPraesens; set
            {
                _statusPraesens = value;
                OnPropertyChanged("StatusPraesens");
            }
        }
        public string Diagnos { get; set; }
        public string Age { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
