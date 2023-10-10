namespace AgendaOnline.Models
{
    public class NotasModel
    {
        public int IdNotes { get; set; }
        public required string Nota { get; set; }
        public AlamacenDeNotasModel IdNota2 { get; set; }
    }
}
