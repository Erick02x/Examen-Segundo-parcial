namespace Entidades
{
    public class servicio
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string DescripcionRespuesta { get; set; }
        public int Existencia { get; set; }
        public decimal Precio { get; set; }
        public byte[] Foto { get; set; }
        public bool EstaDisponible { get; set; }


        public servicio()
        {
        }

        public servicio(string codigo, string descripcion, string descripcionRespuesta, int existencia, decimal precio, byte[] foto, bool estaDisponible)
        {
            Codigo = codigo;
            Descripcion = descripcion;
            DescripcionRespuesta = descripcionRespuesta;
            Existencia = existencia;
            Precio = precio;
            Foto = foto;
            EstaDisponible = estaDisponible;
        }
    }
}
