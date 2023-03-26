namespace Entidades
{
    public class DetalleTicket
    {
        public int Id { get; set; }
        public int IdTicket { get; set; }
        public string CodigoServicio { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public DetalleTicket()
        {
        }

        public DetalleTicket(int id, int idTicket, string codigoServicio, string descripcion, decimal precio, int cantidad, decimal total)
        {
            Id = id;
            IdTicket = idTicket;
            CodigoServicio = codigoServicio;
            Descripcion = descripcion;
            Precio = precio;
            Cantidad = cantidad;
            Total = total;
        }
    }
}
