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
    public class TrangThaiTaiXesController : Controller
    {
        private readonly CarHubContext _context;

        public TrangThaiTaiXesController(CarHubContext context)
        {
            _context = context;
        }

        // GET: TrangThaiTaiXes
        public async Task<IActionResult> Index()
        {
              return _context.TrangThaiTaiXes != null ? 
                          View(await _context.TrangThaiTaiXes.ToListAsync()) :
                          Problem("Entity set 'CarHubContext.TrangThaiTaiXes'  is null.");
        }

        // GET: TrangThaiTaiXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrangThaiTaiXes == null)
            {
                return NotFound();
            }

            var trangThaiTaiXe = await _context.TrangThaiTaiXes
                .FirstOrDefaultAsync(m => m.TtId == id);
            if (trangThaiTaiXe == null)
            {
                return NotFound();
            }

            return View(trangThaiTaiXe);
        }

        // GET: TrangThaiTaiXes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TrangThaiTaiXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TtId,TtTen")] TrangThaiTaiXe trangThaiTaiXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trangThaiTaiXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trangThaiTaiXe);
        }

        // GET: TrangThaiTaiXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrangThaiTaiXes == null)
            {
                return NotFound();
            }

            var trangThaiTaiXe = await _context.TrangThaiTaiXes.FindAsync(id);
            if (trangThaiTaiXe == null)
            {
                return NotFound();
            }
            return View(trangThaiTaiXe);
        }

        // POST: TrangThaiTaiXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TtId,TtTen")] TrangThaiTaiXe trangThaiTaiXe)
        {
            if (id != trangThaiTaiXe.TtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trangThaiTaiXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrangThaiTaiXeExists(trangThaiTaiXe.TtId))
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
            return View(trangThaiTaiXe);
        }

        // GET: TrangThaiTaiXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrangThaiTaiXes == null)
            {
                return NotFound();
            }

            var trangThaiTaiXe = await _context.TrangThaiTaiXes
                .FirstOrDefaultAsync(m => m.TtId == id);
            if (trangThaiTaiXe == null)
            {
                return NotFound();
            }

            return View(trangThaiTaiXe);
        }

        // POST: TrangThaiTaiXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrangThaiTaiXes == null)
            {
                return Problem("Entity set 'CarHubContext.TrangThaiTaiXes'  is null.");
            }
            var trangThaiTaiXe = await _context.TrangThaiTaiXes.FindAsync(id);
            if (trangThaiTaiXe != null)
            {
                _context.TrangThaiTaiXes.Remove(trangThaiTaiXe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrangThaiTaiXeExists(int id)
        {
          return (_context.TrangThaiTaiXes?.Any(e => e.TtId == id)).GetValueOrDefault();
        }
    }
}
