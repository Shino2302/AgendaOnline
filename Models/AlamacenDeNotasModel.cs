using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaOnline.Models
{
    public class AlamacenDeNotasModel
    {
        public int IdNota { get; set; }
        public required string Indice { get; set; }
        public UsuairosModel IdUsuario2 { get; set; }
    }
}
