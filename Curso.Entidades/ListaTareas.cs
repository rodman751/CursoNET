using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.Entidades
{
    public class ListaTareas
    {
        [Key]
        public int ListaID { get; set; }
        public string UsuarioID { get; set; }
        public string Nombre { get; set; }

        //relaciones
        public Usuarios? Usuario { get; set; }
        public List<Tareas>? Tareas { get; set; }

    }
}
