using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecuperacionJoseDiazPascual.Abstractions;
using SQLite;

namespace RecuperacionJoseDiazPascual.MVVM.Models
{
    [Table("Etiqueta")]
    public class Etiqueta : TableData
    {
        [Column("titulo"), Indexed, NotNull]
        public string Titulo { get; set; }
    }
}