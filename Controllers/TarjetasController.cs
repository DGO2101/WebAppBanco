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
        public IActionResult AgregarCompra()
        {
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarCompra([Bind("TarjetaId,FechaCompra,Descripcion,Monto")] Compras compras)
        {
                _context.Add(compras);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
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
