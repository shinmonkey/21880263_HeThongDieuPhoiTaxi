using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServerService.Models;

namespace ServerService.Controllers
{
    public class TrangThaiDatXesController : Controller
    {
        private readonly CarHubContext _context;

        public TrangThaiDatXesController(CarHubContext context)
        {
            _context = context;
        }

        // GET: TrangThaiDatXes
        public async Task<IActionResult> Index()
        {
              return _context.TrangThaiDatXes != null ? 
                          View(await _context.TrangThaiDatXes.ToListAsync()) :
                          Problem("Entity set 'CarHubContext.TrangThaiDatXes'  is null.");
        }

        // GET: TrangThaiDatXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrangThaiDatXes == null)
            {
                return NotFound();
            }

            var trangThaiDatXe = await _context.TrangThaiDatXes
                .FirstOrDefaultAsync(m => m.TtdxId == id);
            if (trangThaiDatXe == null)
            {
                return NotFound();
            }

            return View(trangThaiDatXe);
        }

        // GET: TrangThaiDatXes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrangThaiDatXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TtdxId,TtdxTen")] TrangThaiDatXe trangThaiDatXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trangThaiDatXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trangThaiDatXe);
        }

        // GET: TrangThaiDatXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrangThaiDatXes == null)
            {
                return NotFound();
            }

            var trangThaiDatXe = await _context.TrangThaiDatXes.FindAsync(id);
            if (trangThaiDatXe == null)
            {
                return NotFound();
            }
            return View(trangThaiDatXe);
        }

        // POST: TrangThaiDatXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TtdxId,TtdxTen")] TrangThaiDatXe trangThaiDatXe)
        {
            if (id != trangThaiDatXe.TtdxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trangThaiDatXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrangThaiDatXeExists(trangThaiDatXe.TtdxId))
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
            return View(trangThaiDatXe);
        }

        // GET: TrangThaiDatXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrangThaiDatXes == null)
            {
                return NotFound();
            }

            var trangThaiDatXe = await _context.TrangThaiDatXes
                .FirstOrDefaultAsync(m => m.TtdxId == id);
            if (trangThaiDatXe == null)
            {
                return NotFound();
            }

            return View(trangThaiDatXe);
        }

        // POST: TrangThaiDatXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrangThaiDatXes == null)
            {
                return Problem("Entity set 'CarHubContext.TrangThaiDatXes'  is null.");
            }
            var trangThaiDatXe = await _context.TrangThaiDatXes.FindAsync(id);
            if (trangThaiDatXe != null)
            {
                _context.TrangThaiDatXes.Remove(trangThaiDatXe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrangThaiDatXeExists(int id)
        {
          return (_context.TrangThaiDatXes?.Any(e => e.TtdxId == id)).GetValueOrDefault();
        }
    }
}
