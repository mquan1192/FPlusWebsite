using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using fpluswebsite2.Models;
using System.Web.Security;
using fpluswebsite2.CustomFilters;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace fpluswebsite2.Controllers
{
    [AuthLog(Roles = "Admin")]
    public class RoleController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        //public ActionResult Index()
        //{
        //    try
        //    {
        //       var user = from u in context.Users.`
        //        var reservation = from re in db.Reservations
        //                              //join clr in db.Cleanings on re.HotelId equals clr.HotelId
        //                              //where dateStart <= re.Checkout && re.Checkout <= dateStop && clr.Date < re.Checkout
        //                          where dateStart <= re.Checkout && re.Checkout <= dateStop
        //                          select re;
        //        return View(await reservation.ToListAsync());
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewData["Message"] = ex.Message;
        //        return View("Error");
        //    }
        //}
    
        // GET: Role
        public ActionResult ManageUserRole()
        {
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>  new SelectListItem { Value = rr.Id.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            var listUser = context.Users.OrderBy(u => u.UserName).ToList().Select(uu => new SelectListItem { Value = uu.Id.ToString(), Text = uu.UserName }).ToList();
            ViewBag.Users = listUser;
            return View();
        }

        public ActionResult UpdateRole (string UserID, string RoleID)
        {
            //if (con.State == ConnectionState.Closed)
            //    con.Open();

            //SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(sqlcommand, con);

            //DataTable myDataTable = new DataTable();

            //try
            //{
            //    mySqlDataAdapter.Fill(myDataTable);
            //    gvEmployee.DataSource = myDataTable;
            //    myDataTable.Dispose();
            //    gvEmployee.DataBind();
            //}
            //catch (Exception e)
            //{
            //    ErrorMessage(this, e.Message);
            //}

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["fplus1"].ConnectionString);
            if (con.State == ConnectionState.Closed)
                con.Open();

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format("UPDATE AspNetUserRoles SET RoleId='{0}' WHERE UserId='{1}'", RoleID, UserID);
                //cmd.CommandText = string.Format("INSERT INTO employee (StartDate,FullName,Account,BirthDate,Group_) VALUES ('{0}','{1}','{2}','{3}','{4}')", );
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ViewData["Message"] = ex.Message;
                return View("Error");
            }
            finally
            {
                con.Close();
                con.Dispose();
                
            }
            return View("ManageUserRole");
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var account = new AccountController();

                ViewBag.RolesForThisUser = account.UserManager.GetRoles(user.Id);

                // prepopulat roles for the view dropdown
                var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                ViewBag.Roles = list;
            }

            return View("ManageUserRoles");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            var account = new AccountController();
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (account.UserManager.IsInRole(user.Id, RoleName))
            {
                account.UserManager.RemoveFromRole(user.Id, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageUserRoles");
        }

        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
