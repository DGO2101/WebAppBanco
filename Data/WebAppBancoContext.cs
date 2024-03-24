using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppBanco.Models;

namespace WebAppBanco.Data
{
    public class WebAppBancoContext : DbContext
    {
        public WebAppBancoContext (DbContextOptions<WebAppBancoContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppBanco.Models.Tarjetas> Tarjetas { get; set; } = default!;
        public DbSet<WebAppBanco.Models.Compras> Compras { get; set; } = default!;
        public DbSet<WebAppBanco.Models.Pagos> Pagos { get; set; } = default!;
    }
}
