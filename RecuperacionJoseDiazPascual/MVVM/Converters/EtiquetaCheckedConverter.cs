using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacionJoseDiazPascual.MVVM.Converters
{
    public class EtiquetaCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var etiquetasSeleccionadas = value as ObservableCollection<string>;
            var etiqueta = parameter as string;
            return etiquetasSeleccionadas?.Contains(etiqueta) ?? false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
