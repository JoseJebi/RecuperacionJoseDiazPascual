using RecuperacionJoseDiazPascual.Abstractions;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecuperacionJoseDiazPascual.MVVM.Models
{
    [Table("EtiquetasTarea")]
    public class EtiquetasTarea:TableData
    {
        [ForeignKey(typeof(Tarea))]
        public int TareaId { get; set; }
        [ForeignKey(typeof(Etiqueta))]
        public int EtiquetaId { get; set; }
    }
}
