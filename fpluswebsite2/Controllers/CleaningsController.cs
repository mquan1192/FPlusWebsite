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
using fpluswebsite2.CustomFilters;

namespace fpluswebsite2.Controllers
{
    [AuthLog(Roles = "Admin,Manager,User")]
    public class CleaningsController : Controller
    {
        private fplus1 db = new fplus1();

        //get list hotel need to clean
        public async Task<ActionResult> ViewReservation(String date1, String date2)
        {
            try
            {
                DateTime dateStart = Convert.ToDateTime(date1);
                DateTime dateStop = Convert.ToDateTime(date2);
                var reservation = from re in db.Reservations
                                      //join clr in db.Cleanings on re.HotelId equals clr.HotelId
                                      //where dateStart <= re.Checkout && re.Checkout <= dateStop && clr.Date < re.Checkout
                                  where dateStart <= re.Checkout && re.Checkout <= dateStop
                                  select re;
                return View(await reservation.ToListAsync());
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Error");
            }   
        }


        // GET: Cleanings
        public async Task<ActionResult> Index()
        {
            try
            {
                var cleanings = db.Cleanings.Include(c => c.Hotel).Include(c => c.Staff);
                return View(await cleanings.ToListAsync());
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Error");
            }
            
        }

        // GET: Cleanings/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cleaning cleaning = await db.Cleanings.FindAsync(id);
                if (cleaning == null)
                {
                    return HttpNotFound();
                }
                return View(cleaning);
            }
            catch (Exception ex)
            {

                ViewData["Message"] = ex.Message;
                return View("Error");
            }
            
        }

        // GET: Cleanings/Create
        [AuthLog(Roles = "Admin,Manager")]
        public ActionResult Create()
        {
            try
            {
                ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name");
                ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name");
                return View();
            }
            catch (Exception ex)
            {

                ViewData["Message"] = ex.Message;
                return View("Error");
            }
            
        }

        // POST: Cleanings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> Create([Bind(Include = "Id,StaffId,HotelId,Date,TimeIn,TimeOut,Fee,Memo")] Cleaning cleaning)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Cleanings.Add(cleaning);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name", cleaning.HotelId);
                ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", cleaning.StaffId);
                return View(cleaning);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Error");
            }
           
        }

        // GET: Cleanings/Edit/5
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> Edit(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cleaning cleaning = await db.Cleanings.FindAsync(id);
                if (cleaning == null)
                {
                    return HttpNotFound();
                }
                ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name", cleaning.HotelId);
                ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", cleaning.StaffId);
                return View(cleaning);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Error");
            }
           
        }

        // POST: Cleanings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,StaffId,HotelId,Date,TimeIn,TimeOut,Fee,Memo")] Cleaning cleaning)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cleaning).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name", cleaning.HotelId);
                ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", cleaning.StaffId);
                return View(cleaning);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Error");
            }
            
        }

        // GET: Cleanings/Delete/5
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> Delete(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cleaning cleaning = await db.Cleanings.FindAsync(id);
                if (cleaning == null)
                {
                    return HttpNotFound();
                }
                return View(cleaning);
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Error");
            }
            
        }

        // POST: Cleanings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthLog(Roles = "Admin,Manager")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            try
            {
                Cleaning cleaning = await db.Cleanings.FindAsync(id);
                db.Cleanings.Remove(cleaning);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Error");
            }
           
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="createAdv"></param>
        /// 
        public ActionResult CreateAdvance(long? id,string name)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //Cleaning cleaning = await db.Cleanings.FindAsync(id);
                //Hotel h = db.Hotels.FindAsync(id);
                //if (h == null)
                //{
                //    return HttpNotFound();
                //}
                ViewBag.HotelName = new SelectList(db.Hotels,"Id","Name",id);
                //ViewBag.HotelId = new SelectList(db.Hotels, "Id", "Name", cleaning.HotelId);
                //ViewBag.StaffId = new SelectList(db.Staffs, "Id", "Name", cleaning.StaffId);
                return View();
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Error");
            }

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
