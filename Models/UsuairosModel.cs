namespace AgendaOnline.Models
{
    public class UsuairosModel
    {
        public int IdUsuario { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Pass { get; set; }

    }
}
