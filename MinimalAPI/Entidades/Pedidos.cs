namespace MinimalAPI.Entidades
{
    public class Pedidos
    {
        public int Id { get; set; }
        public string CardCode { get; set; }
        public string NombreCotizacion { get; set; }
        public string Estado {  get; set; }
        public string FechaCaducidad { get; set; }
        public string NombreCliente { get; set; }
        public string Observaciones { get; set; }
        public string Subtotal { get; set; }
        public string ListaFormulas { get; set; }
        public string DescPorVolumen { get; set; }
        public string TotalVentas { get; set; }
        public string Impuestos { get; set; }
        public string NombreContacto { get; set; }
        public string EmailContacto { get; set; }
        public string PaisFacturacion { get; set; }
        public string CalleFacturacion { get; set; }
        public string EstadoProvinciaFacturacion { get; set; }
        public string CodigoPostalFacturacion { get; set; }
        public string CiudadFacturacion { get; set; }
        public string PaisEnvio { get; set; }
        public string CalleEnvio { get; set; }
        public string EstadoProvinciaEnvio { get; set; }
        public string CodigoPostalEnvio { get; set; }
        public string CiudadEnvio { get; set; }


    }
}
