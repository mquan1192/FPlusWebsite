using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fpluswebsite2.Models;
using System.Web.Security;
using fpluswebsite2.CustomFilters;

namespace fpluswebsite2.Controllers
{
    [AuthLog(Roles = "Admin,Manager,User")]
    public class PickupsController : Controller
    {
        private fplus1 db = new fplus1();

        // GET: Pickups
        public async Task<ActionResult> Index()
        {
            var pickups = db.Pickups.Include(p => p.Reservation).Include(p => p.Staff);
            return View(await pickups.ToListAsync());
        }

        // GET: Pickups/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = await db.Pickups.FindAsync(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // GET: Pickups/Create
        [AuthLog(Roles = "Admin,Manager")]
        public ActionResult Create()
        {
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "GuestName");
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name");
            return View();
        }

        // POST: Pickups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> Create([Bind(Include = "Id,ReservationId,Time,StaffId,Place")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                db.Pickups.Add(pickup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "GuestName", pickup.ReservationId);
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", pickup.StaffId);
            return View(pickup);
        }

        // GET: Pickups/Edit/5
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = await db.Pickups.FindAsync(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "GuestName", pickup.ReservationId);
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", pickup.StaffId);
            return View(pickup);
        }

        // POST: Pickups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ReservationId,Time,StaffId,Place")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "GuestName", pickup.ReservationId);
            ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", pickup.StaffId);
            return View(pickup);
        }

        // GET: Pickups/Delete/5
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pickup pickup = await db.Pickups.FindAsync(id);
            if (pickup == null)
            {
                return HttpNotFound();
            }
            return View(pickup);
        }

        // POST: Pickups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Pickup pickup = await db.Pickups.FindAsync(id);
            db.Pickups.Remove(pickup);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
