using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebAppBanco.Models
{
    public class Tarjetas //se declaran los atributos de Tarjetas       
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Numero { get; set; }

        public virtual ICollection<Pagos> Pagos { get; set; }
        public virtual ICollection<Compras  > Compras { get; set; }
    }
}
