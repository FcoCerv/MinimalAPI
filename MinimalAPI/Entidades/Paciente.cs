namespace MinimalAPI.Entidades
{
    public class Paciente
    {
        public int Id { get; set; }
        public string CodigoSN { get; set; }
        public string Prospecto { get; set; } = null!;
        public string Especialidades {  get; set; } = null!;
        public string EstadoDeProspecto { get; set; } = null!;
        public string ProductosDeInteres { get; set; } = null!;
        public string Correo { get; set; } = null!;
                
    }
}
