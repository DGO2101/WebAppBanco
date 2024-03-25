using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml; // Librería para trabajar con Excel
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
        // Muestra la lista de todas las tarjetas de crédito
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tarjetas.ToListAsync());
        }

        // GET: Tarjetas/Details/5
        // Muestra los detalles de una tarjeta específica
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
        // Muestra el formulario para crear una nueva tarjeta de crédito
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tarjetas/Create
        // Crea una nueva tarjeta de crédito en la base de datos
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

        // GET: Tarjetas/AgregarPago/5
        public IActionResult AgregarPago(int id)
        {
            ViewBag.TarjetaId = id;
            return View();
        }

        // POST: Tarjetas/AgregarPago/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarPago(int id, [Bind("Fecha,Descripcion,Monto")] Pagos pago)
        {
            pago.TarjetaId = id;
            _context.Add(pago);
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
        public async Task<IActionResult> Transacciones(int? id)
        {
            // Obtener el primer día del mes actual
            var primerDiaMesActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            // Obtener las compras y pagos realizados en el mes actual para la tarjeta especificada, ordenados por fecha
            var compras = await _context.Compras
                .Where(c => c.TarjetaId == id && c.Fecha >= primerDiaMesActual)
                .OrderBy(c => c.Fecha)
                .ToListAsync();

            var pagos = await _context.Pagos
                .Where(p => p.TarjetaId == id && p.Fecha >= primerDiaMesActual)
                .OrderBy(p => p.Fecha)
                .ToListAsync();

            // Crear la tupla con las compras y pagos del mes actual
            var model = new Tuple<IEnumerable<Compras>, IEnumerable<Pagos>>(compras, pagos);

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
        //edita los objetos Tarjeta creados
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

        //borra los objetos Tarjeta creados
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

        //exporta a Excel la lista de compras
        public ActionResult ExportarExcel()
        {
            var compras = _context.Compras.ToList();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Compras");

                worksheet.Cells.LoadFromCollection(compras, true);

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Compras.xlsx");
            }
        }

        private bool TarjetasExists(int id)
            {
                return _context.Tarjetas.Any(e => e.Id == id);
            }
        }
    }
