using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppBanco.Models
{
    public class Compras //se declaran los atributos de compras 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int TarjetaId { get; set; } // Propiedad para almacenar el Id de la tarjeta asociada

        public virtual ICollection<Tarjetas> Tarjetas { get; set; }

    }
}
