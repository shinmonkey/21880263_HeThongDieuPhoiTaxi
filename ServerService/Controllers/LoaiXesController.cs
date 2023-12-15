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
    public class LoaiXesController : Controller
    {
        private readonly CarHubContext _context;

        public LoaiXesController(CarHubContext context)
        {
            _context = context;
        }

        // GET: LoaiXes
        public async Task<IActionResult> Index()
        {
              return _context.LoaiXes != null ? 
                          View(await _context.LoaiXes.ToListAsync()) :
                          Problem("Entity set 'CarHubContext.LoaiXes'  is null.");
        }

        // GET: LoaiXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiXes == null)
            {
                return NotFound();
            }

            var loaiXe = await _context.LoaiXes
                .FirstOrDefaultAsync(m => m.LxId == id);
            if (loaiXe == null)
            {
                return NotFound();
            }

            return View(loaiXe);
        }

        // GET: LoaiXes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LxId,LxTen")] LoaiXe loaiXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiXe);
        }

        // GET: LoaiXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiXes == null)
            {
                return NotFound();
            }

            var loaiXe = await _context.LoaiXes.FindAsync(id);
            if (loaiXe == null)
            {
                return NotFound();
            }
            return View(loaiXe);
        }

        // POST: LoaiXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LxId,LxTen")] LoaiXe loaiXe)
        {
            if (id != loaiXe.LxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiXeExists(loaiXe.LxId))
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
            return View(loaiXe);
        }

        // GET: LoaiXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiXes == null)
            {
                return NotFound();
            }

            var loaiXe = await _context.LoaiXes
                .FirstOrDefaultAsync(m => m.LxId == id);
            if (loaiXe == null)
            {
                return NotFound();
            }

            return View(loaiXe);
        }

        // POST: LoaiXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiXes == null)
            {
                return Problem("Entity set 'CarHubContext.LoaiXes'  is null.");
            }
            var loaiXe = await _context.LoaiXes.FindAsync(id);
            if (loaiXe != null)
            {
                _context.LoaiXes.Remove(loaiXe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiXeExists(int id)
        {
          return (_context.LoaiXes?.Any(e => e.LxId == id)).GetValueOrDefault();
        }
    }
}
