namespace WebAppBanco.Models
{
    public class Pagos // se crean los atributos de pagos 
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int TarjetaId { get; set; } // Propiedad para almacenar el Id de la tarjeta asociada

        public virtual ICollection<Tarjetas> Tarjetas { get; set; }
    }
}
