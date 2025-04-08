using PropertyChanged;
using RecuperacionJoseDiazPascual.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecuperacionJoseDiazPascual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class PrinicipalViewModel
    {
        public ObservableCollection<Tarea> Tareas { get; set; }
        public bool IsRefreshing { get; set; }
        public ICommand RefreshCommand { get; }

        public PrinicipalViewModel()
        {
            RefreshCommand = new Command(async () => await RefrescarTareas());
        }

        private void CargarTareas()
        {
            var tareasDesdeDb = App.TareaRepositorio.GetItemsCascada();

            Tareas.Clear();

            foreach (var tarea in tareasDesdeDb)
            {
                Tareas.Add(tarea);
            }
        }

        private async Task RefrescarTareas()
        {
            IsRefreshing = true;

            await Task.Delay(1000);

            CargarTareas();

            IsRefreshing = false;
        }
    }
}
