using PropertyChanged;
using RecuperacionJoseDiazPascual.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacionJoseDiazPascual.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class PrinicipalViewModel
    {
        public List<Tarea> Tareas { get; set; }
    }
}
