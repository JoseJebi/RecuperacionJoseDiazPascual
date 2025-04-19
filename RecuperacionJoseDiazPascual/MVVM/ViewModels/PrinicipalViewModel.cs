using PropertyChanged;
using RecuperacionJoseDiazPascual.MVVM.Models;
using RecuperacionJoseDiazPascual.MVVM.Views;
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
        public ICommand RefreshCommand { get; set; }
        public ICommand AgregarCommand { get; set; }

        public PrinicipalViewModel()
        {
            RefreshCommand = new Command(async () => await RefrescarTareas());

            AgregarCommand = new Command(async () =>
            {
                if (Application.Current.MainPage is NavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(new AgregarView());
                }
            });

            Tareas = new ObservableCollection<Tarea>();
            CargarTareas();
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

        private void AgregarTarea()
        {
            new AgregarView();
        }
    }
}
