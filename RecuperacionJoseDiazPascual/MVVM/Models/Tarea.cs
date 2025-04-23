using RecuperacionJoseDiazPascual.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace RecuperacionJoseDiazPascual.MVVM.Models
{
    [Table("Tarea")]
    public class Tarea : TableData
    {
        [Column("titulo"), Indexed, NotNull]
        public string Titulo { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [Column("prioridad")]
        public string Prioridad { get; set; }

        [ManyToMany(typeof(EtiquetasTarea), CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Etiqueta> Etiquetas { get; set; }

        public String EtiquetasString()
        {
            string etiquetas = "";

            foreach (Etiqueta etiqueta in Etiquetas)
            {
                etiquetas += etiqueta.Titulo + ", ";
            }

            return etiquetas.Substring(0, etiquetas.Length - 3);
        }
    }
}
