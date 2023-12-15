using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServerService.Models;
using Microsoft.AspNetCore.SignalR.Client;


namespace ServerService.Controllers
{

    public class DatXesController : Controller
    {
        private readonly CarHubContext _context;
        public double Latitude = 10.762679;
        public double Longitude = 106.682586;

        public DatXesController(CarHubContext context)
        {
            _context = context;
        }

        // GET: DatXes
        public async Task<IActionResult> Index()
        {
            var carHubContext = _context.DatXes.Include(d => d.Kh).Include(d => d.Lx).Include(d => d.Ttdx).Include(d => d.Tx).Include(d => d.Xe);
            return View(await carHubContext.ToListAsync());
        }

        // GET: DatXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DatXes == null)
            {
                return NotFound();
            }

            var datXe = await _context.DatXes
                .Include(d => d.Kh)
                .Include(d => d.Lx)
                .Include(d => d.Ttdx)
                .Include(d => d.Tx)
                .Include(d => d.Xe)
                .FirstOrDefaultAsync(m => m.DxId == id);
            if (datXe == null)
            {
                return NotFound();
            }

            return View(datXe);
        }

        // GET: DatXes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["carHubContext"] = _context.DatXes.Include(d => d.Kh).Include(d => d.Lx).Include(d => d.Ttdx).Include(d => d.Tx).Include(d => d.Xe);
            ViewData["KhId"] = new SelectList(_context.KhachHangs, "KhId", "KhId");
            ViewData["LxId"] = new SelectList(_context.LoaiXes, "LxId", "LxTen");
            ViewData["TtdxId"] = new SelectList(_context.TrangThaiDatXes, "TtdxId", "TtdxId");
            ViewData["TxId"] = new SelectList(_context.TaiXes, "TxId", "TxId");
            ViewData["XeId"] = new SelectList(_context.Xes, "XeId", "XeId");
            return View();
        }

        // POST: DatXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DxId,DxNgayGio,DxDiadiemdon,DxGpsLon,DxGpsLat,KhTen,KhPhone,KhId,TxId,XeId,LxId,TtdxId")] DatXe datXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(datXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KhId"] = new SelectList(_context.KhachHangs, "KhId", "KhId", datXe.KhId);
            ViewData["LxId"] = new SelectList(_context.LoaiXes, "LxId", "LxId", datXe.LxId);
            ViewData["TtdxId"] = new SelectList(_context.TrangThaiDatXes, "TtdxId", "TtdxId", datXe.TtdxId);
            ViewData["TxId"] = new SelectList(_context.TaiXes, "TxId", "TxId", datXe.TxId);
            ViewData["XeId"] = new SelectList(_context.Xes, "XeId", "XeId", datXe.XeId);
            return View(datXe);
        }

        // GET: DatXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DatXes == null)
            {
                return NotFound();
            }

            var datXe = await _context.DatXes.FindAsync(id);
            if (datXe == null)
            {
                return NotFound();
            }
            ViewData["KhId"] = new SelectList(_context.KhachHangs, "KhId", "KhId", datXe.KhId);
            ViewData["LxId"] = new SelectList(_context.LoaiXes, "LxId", "LxId", datXe.LxId);
            ViewData["TtdxId"] = new SelectList(_context.TrangThaiDatXes, "TtdxId", "TtdxId", datXe.TtdxId);
            ViewData["TxId"] = new SelectList(_context.TaiXes, "TxId", "TxId", datXe.TxId);
            ViewData["XeId"] = new SelectList(_context.Xes, "XeId", "XeId", datXe.XeId);
            return View(datXe);
        }

        // POST: DatXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DxId,DxNgayGio,DxDiadiemdon,DxGpsLon,DxGpsLat,KhTen,KhPhone,KhId,TxId,XeId,LxId,TtdxId")] DatXe datXe)
        {
            if (id != datXe.DxId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(datXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatXeExists(datXe.DxId))
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
            ViewData["KhId"] = new SelectList(_context.KhachHangs, "KhId", "KhId", datXe.KhId);
            ViewData["LxId"] = new SelectList(_context.LoaiXes, "LxId", "LxId", datXe.LxId);
            ViewData["TtdxId"] = new SelectList(_context.TrangThaiDatXes, "TtdxId", "TtdxId", datXe.TtdxId);
            ViewData["TxId"] = new SelectList(_context.TaiXes, "TxId", "TxId", datXe.TxId);
            ViewData["XeId"] = new SelectList(_context.Xes, "XeId", "XeId", datXe.XeId);
            return View(datXe);
        }

        // GET: DatXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DatXes == null)
            {
                return NotFound();
            }

            var datXe = await _context.DatXes
                .Include(d => d.Kh)
                .Include(d => d.Lx)
                .Include(d => d.Ttdx)
                .Include(d => d.Tx)
                .Include(d => d.Xe)
                .FirstOrDefaultAsync(m => m.DxId == id);
            if (datXe == null)
            {
                return NotFound();
            }

            return View(datXe);
        }

        // POST: DatXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DatXes == null)
            {
                return Problem("Entity set 'CarHubContext.DatXes'  is null.");
            }
            var datXe = await _context.DatXes.FindAsync(id);
            if (datXe != null)
            {
                _context.DatXes.Remove(datXe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatXeExists(int id)
        {
          return (_context.DatXes?.Any(e => e.DxId == id)).GetValueOrDefault();
        }
    }
}
