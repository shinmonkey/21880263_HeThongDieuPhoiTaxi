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
    public class XesController : Controller
    {
        private readonly CarHubContext _context;

        public XesController(CarHubContext context)
        {
            _context = context;
        }

        // GET: Xes
        public async Task<IActionResult> Index()
        {
            var carHubContext = _context.Xes.Include(x => x.Lx).Include(x => x.Tx);
            return View(await carHubContext.ToListAsync());
        }

        // GET: Xes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes
                .Include(x => x.Lx)
                .Include(x => x.Tx)
                .FirstOrDefaultAsync(m => m.XeId == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // GET: Xes/Create
        public IActionResult Create()
        {
            ViewData["LxId"] = new SelectList(_context.LoaiXes, "LxId", "LxId");
            ViewData["TxId"] = new SelectList(_context.TaiXes, "TxId", "TxId");
            return View();
        }

        // POST: Xes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("XeId,TxId,LxId,XeBienso,XeGiayDangky")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LxId"] = new SelectList(_context.LoaiXes, "LxId", "LxId", xe.LxId);
            ViewData["TxId"] = new SelectList(_context.TaiXes, "TxId", "TxId", xe.TxId);
            return View(xe);
        }

        // GET: Xes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            ViewData["LxId"] = new SelectList(_context.LoaiXes, "LxId", "LxId", xe.LxId);
            ViewData["TxId"] = new SelectList(_context.TaiXes, "TxId", "TxId", xe.TxId);
            return View(xe);
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("XeId,TxId,LxId,XeBienso,XeGiayDangky")] Xe xe)
        {
            if (id != xe.XeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(xe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.XeId))
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
            ViewData["LxId"] = new SelectList(_context.LoaiXes, "LxId", "LxId", xe.LxId);
            ViewData["TxId"] = new SelectList(_context.TaiXes, "TxId", "TxId", xe.TxId);
            return View(xe);
        }

        // GET: Xes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes
                .Include(x => x.Lx)
                .Include(x => x.Tx)
                .FirstOrDefaultAsync(m => m.XeId == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Xes == null)
            {
                return Problem("Entity set 'CarHubContext.Xes'  is null.");
            }
            var xe = await _context.Xes.FindAsync(id);
            if (xe != null)
            {
                _context.Xes.Remove(xe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(int id)
        {
          return (_context.Xes?.Any(e => e.XeId == id)).GetValueOrDefault();
        }
    }
}
