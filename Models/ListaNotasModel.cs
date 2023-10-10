using System.Reflection;

namespace AgendaOnline.Models
{
    public class ListaNotasModel
    {
        public int IdNotes { get; set; }
        public required string Nota { get; set; }
        public required string Indice { get; set; }
    }
}
