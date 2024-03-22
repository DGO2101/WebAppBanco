namespace WebAppBanco.Models
{
    public class Pagos // se crean los atributos de pagos 
    {
        public int Id { get; set; }
        public DateTime FechaPago { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }

        public virtual ICollection<Tarjetas> Tarjetas { get; set; }
    }
}
