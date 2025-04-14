using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacionJoseDiazPascual.MVVM.Models
{
    public class EtiquetaSeleccionada : INotifyPropertyChanged
    {
        public string Nombre { get; set; }

        private bool _seleccionada;
        public bool Seleccionada
        {
            get => _seleccionada;
            set
            {
                if (_seleccionada != value)
                {
                    _seleccionada = value;
                    OnPropertyChanged(nameof(Seleccionada));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
