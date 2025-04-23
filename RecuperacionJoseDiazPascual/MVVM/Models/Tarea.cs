using RecuperacionJoseDiazPascual.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

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
        public List<Etiqueta> Etiquetas { get; set; }

        public String StringEtiquetas { get; set; }

        public void EtiquetasString()
        {
            string etiquetas = "";

            foreach (Etiqueta etiqueta in Etiquetas)
            {
                etiquetas += etiqueta.Titulo + ", ";
            }

            StringEtiquetas = etiquetas.Substring(0, etiquetas.Length - 3);
        }
    }
}
