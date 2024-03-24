    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using WebAppBanco.Data;
    using WebAppBanco.Models;

    namespace WebAppBanco4.Controllers
    {
        public class TarjetasController : Controller
        {
            private readonly WebAppBancoContext _context;

            public TarjetasController(WebAppBancoContext context)
            {
                _context = context;
            }

            // GET: Tarjetas
            public async Task<IActionResult> Index()
            {
                return View(await _context.Tarjetas.ToListAsync());
            }

            // GET: Tarjetas/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tarjetas = await _context.Tarjetas
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (tarjetas == null)
                {
                    return NotFound();
                }

                return View(tarjetas);
            }

            //GET: Tarjetas/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Tarjetas/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("Id,Nombres,Apellidos,Numero")] Tarjetas tarjetas)
            {
                    _context.Add(tarjetas);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
            }

        // GET: Tarjetas/AgregarCompra/5
        public IActionResult AgregarCompra(int id)
        {
            ViewBag.TarjetaId = id;
            return View();
        }

        // POST: Tarjetas/AgregarCompra/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarCompra(int id, [Bind("Fecha,Descripcion,Monto")] Compras compra)
        {
                compra.TarjetaId = id;
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
        }

        // GET: Tarjetas/VerCompras/5
        public async Task<IActionResult> ListaCompras(int? id)
        {
            // Obtener el primer día del mes actual
            var primerDiaMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Obtener el primer día del mes anterior
            var primerDiaMesAnterior = primerDiaMesActual.AddMonths(-1);

            // Obtener las compras realizadas en el mes actual para la tarjeta especificada
            var comprasMesActual = await _context.Compras
                .Where(c => c.TarjetaId == id && c.Fecha >= primerDiaMesActual)
                .ToListAsync();

            // Obtener las compras realizadas en el mes anterior para la tarjeta especificada
            var comprasMesAnterior = await _context.Compras
                .Where(c => c.TarjetaId == id && c.Fecha >= primerDiaMesAnterior && c.Fecha < primerDiaMesActual)
                .ToListAsync();

            // Calcular el total de las compras del mes anterior
            decimal totalComprasMesAnterior = comprasMesAnterior.Sum(c => c.Monto);

            // Obtener los datos de la tarjeta
            var tarjeta = await _context.Tarjetas.FirstOrDefaultAsync(t => t.Id == id);

            // Crear la tupla con la tarjeta, las compras del mes actual y el total de compras del mes anterior
            var model = new Tuple<Tarjetas, IEnumerable<Compras>, decimal>(tarjeta, comprasMesActual, totalComprasMesAnterior);

            return View(model);
        }









        // GET: Tarjetas/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tarjetas = await _context.Tarjetas.FindAsync(id);
                if (tarjetas == null)
                {
                    return NotFound();
                }
                return View(tarjetas);
            }

            // POST: Tarjetas/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,Numero")] Tarjetas tarjetas)
            {
                if (id != tarjetas.Id)
                {
                    return NotFound();
                }
                    try
                    {
                        _context.Update(tarjetas);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TarjetasExists(tarjetas.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
            }

            // GET: Tarjetas/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var tarjetas = await _context.Tarjetas
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (tarjetas == null)
                {
                    return NotFound();
                }

                return View(tarjetas);
            }

            // POST: Tarjetas/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var tarjetas = await _context.Tarjetas.FindAsync(id);
                if (tarjetas != null)
                {
                    _context.Tarjetas.Remove(tarjetas);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool TarjetasExists(int id)
            {
                return _context.Tarjetas.Any(e => e.Id == id);
            }
        }
    }
