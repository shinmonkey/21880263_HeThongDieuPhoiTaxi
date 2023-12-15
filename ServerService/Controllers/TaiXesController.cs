using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServerService.Models;

namespace ServerService.Controllers
{
    public class TaiXesController : Controller
    {
        private readonly CarHubContext _context;

        public TaiXesController(CarHubContext context)
        {
            _context = context;
        }

        // GET: TaiXes
        public async Task<IActionResult> Index()
        {
            var carHubContext = _context.TaiXes.Include(t => t.Tt).Include(t => t.Tx);
            return View(await carHubContext.ToListAsync());
        }

        // GET: TaiXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaiXes == null)
            {
                return NotFound();
            }

            var taiXe = await _context.TaiXes
                .Include(t => t.Tt)
                .Include(t => t.Tx)
                .FirstOrDefaultAsync(m => m.TxId == id);
            if (taiXe == null)
            {
                return NotFound();
            }

            return View(taiXe);
        }

        // GET: TaiXes/Create
        public IActionResult Create()
        {
            ViewData["TtId"] = new SelectList(_context.TrangThaiTaiXes, "TtId", "TtId");
            ViewData["TxId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: TaiXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TxId,TxGiayPhepLaiXe,TxKhamSucKhoe,TxCccd,TtId,TxGpsLon,TxGpsLat")] TaiXe taiXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taiXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TtId"] = new SelectList(_context.TrangThaiTaiXes, "TtId", "TtId", taiXe.TtId);
            ViewData["TxId"] = new SelectList(_context.Users, "Id", "Id", taiXe.TxId);
            return View(taiXe);
        }

        // GET: TaiXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaiXes == null)
            {
                return NotFound();
            }

            var taiXe = await _context.TaiXes.FindAsync(id);
            if (taiXe == null)
            {
                return NotFound();
            }
            ViewData["TtId"] = new SelectList(_context.TrangThaiTaiXes, "TtId", "TtId", taiXe.TtId);
            ViewData["TxId"] = new SelectList(_context.Users, "Id", "Id", taiXe.TxId);
            return View(taiXe);
        }

        // POST: TaiXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TxId,TxGiayPhepLaiXe,TxKhamSucKhoe,TxCccd,TtId,TxGpsLon,TxGpsLat")] TaiXe taiXe)
        {
            if (id != taiXe.TxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiXeExists(taiXe.TxId))
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
            ViewData["TtId"] = new SelectList(_context.TrangThaiTaiXes, "TtId", "TtId", taiXe.TtId);
            ViewData["TxId"] = new SelectList(_context.Users, "Id", "Id", taiXe.TxId);
            return View(taiXe);
        }

        // GET: TaiXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaiXes == null)
            {
                return NotFound();
            }

            var taiXe = await _context.TaiXes
                .Include(t => t.Tt)
                .Include(t => t.Tx)
                .FirstOrDefaultAsync(m => m.TxId == id);
            if (taiXe == null)
            {
                return NotFound();
            }

            return View(taiXe);
        }

        // POST: TaiXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaiXes == null)
            {
                return Problem("Entity set 'CarHubContext.TaiXes'  is null.");
            }
            var taiXe = await _context.TaiXes.FindAsync(id);
            if (taiXe != null)
            {
                _context.TaiXes.Remove(taiXe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiXeExists(int id)
        {
          return (_context.TaiXes?.Any(e => e.TxId == id)).GetValueOrDefault();
        }
    }
}
