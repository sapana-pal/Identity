using Identity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Identity.Controllers
{
    public class CustomerController : Controller
    {
        // CustDataContext objDataContext = new CustDataContext();
        private readonly AppIdentityDbContext _IdentityDbContext;
        public CustomerController(AppIdentityDbContext IdentityDbContext)
        {
            _IdentityDbContext = IdentityDbContext;

        }
        public ActionResult Index()
        {
            return RedirectToAction("CustDetails");
        }

        public ActionResult CustDetails()
        {
            return View(_IdentityDbContext.customer.ToList());
        }   
        public ActionResult create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult create(Customer objCust)
        {
            _IdentityDbContext.customer.Add(objCust);
            try
            {
                _IdentityDbContext.SaveChanges();
            }
            catch (DbUpdateException exception) when (exception?.InnerException?.Message.Contains("Cannot insert duplicate key row in object") ?? false)
            {
               
            }
            //objDataContext.SaveChanges();
            // return View();
            return RedirectToAction("CustDetails");
        }
        public ActionResult Details(string id)
        {
            int custId = Convert.ToInt32(id);
            var cust = _IdentityDbContext.customer.Find(custId);
            return View(cust);
        }
        public ActionResult Edit(string id)
        {
            int custId = Convert.ToInt32(id);
            var cust = _IdentityDbContext.customer.Find(custId);
            return View(cust);
        }
        [HttpPost]
        public ActionResult Edit(Customer objCust)
        {
            var data = _IdentityDbContext.customer.Find(objCust.CustId);
            if (data != null)
            {
                data.Name = objCust.Name;
                data.Address = objCust.Address;
                data.Email = objCust.Email;
                data.MobileNo = objCust.MobileNo;
                data.DateOfBirth = objCust.DateOfBirth;
                data.Custom = objCust.Custom;
            }
            _IdentityDbContext.SaveChanges();
            return View();
        }
        public ActionResult Delete(string id)
        {
            int custId = Convert.ToInt32(id);
            var cust = _IdentityDbContext.customer.Find(custId);
            return View(cust);
        }
    }
}
