using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SunrinMPReq.Models;

namespace SunrinMPReq.Controllers
{
    public class RoomController : Controller
    {
        private readonly SunrinMPReqContext _context;
        private Random rng;
        private const string chartable = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        
        public RoomController(SunrinMPReqContext context)
        {
            rng=new Random();
            _context = context;
        }

        // GET: Room
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoomModel.ToListAsync());
        }

        // GET: Room/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomModel = await _context.RoomModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomModel == null)
            {
                return NotFound();
            }

            return View(roomModel);
        }

        // GET: Room/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,RoomKey")] RoomModel roomModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roomModel);
        }
        */

        // GET: Room/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomModel = await _context.RoomModel.FindAsync(id);
            if (roomModel == null)
            {
                return NotFound();
            }
            return View(roomModel);
        }

        // POST: Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,RoomKey")] RoomModel roomModel)
        {
            if (id != roomModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomModelExists(roomModel.Id))
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
            return View(roomModel);
        }

        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomModel = await _context.RoomModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (roomModel == null)
            {
                return NotFound();
            }

            return View(roomModel);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomModel = await _context.RoomModel.FindAsync(id);
            _context.RoomModel.Remove(roomModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // GET: Room/Admin
        public async Task<IActionResult> Admin()
        {
            var rm = new RoomModel() { RoomId = GenRandomstring((int)DateTime.Now.Ticks), RoomKey = Guid.NewGuid().ToString() };
            _context.Add(rm);
            await _context.SaveChangesAsync();
            ViewData["roomid"] = rm.RoomId;
            ViewData["adminkey"] = rm.RoomKey;

            return View(rm);
        }

        public async Task<IActionResult> Guest(string id)
        {
            ViewData["roomId"] = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async void Create()
        {
            var rm=new RoomModel(){RoomId = GenRandomstring((int)DateTime.Now.Ticks),RoomKey = Guid.NewGuid().ToString()};
            _context.Add(rm);
            await _context.SaveChangesAsync();
            Response.Cookies.Append("adminkey",rm.RoomKey);
            Response.Redirect(nameof(Admin));
            //return Response.; //RedirectToAction(nameof(Admin));
        }

        private string GenRandomstring(int baseval)
        {
            baseval += rng.Next();
            baseval = Math.Abs(baseval);
            string res=string.Empty;
            while (baseval > 0)
            {
                res += chartable[baseval % chartable.Length];
                baseval /= chartable.Length;
            }

            return res;
        }
        
        private bool RoomModelExists(int id)
        {
            return _context.RoomModel.Any(e => e.Id == id);
        }
    }
}
