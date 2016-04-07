using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Empty.Models;

namespace MVC_Empty.Controllers
{
    public class CustomersController : Controller
    {
        ModelMVC_Empty context = new ModelMVC_Empty();

        // GET: Customers
        public ActionResult Index()
        {
            List<Customer> customers = context.Customers.OrderBy(o => o.ContactName).ToList();

            return View(customers);
        }

        //
        // Get Customer
        //
        public ActionResult Detail(string Id)
        {
            Customer customer = context.Customers.FirstOrDefault(c => c.CustomerID == Id);

            if (customer == null)
            {
                // hoppas tillbaka till Index
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        public ActionResult Edit(string Id)
        {
            Customer customer = context.Customers.FirstOrDefault(c => c.CustomerID == Id);

            if (customer == null)
            {
                // hoppas tillbaka till Index
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var cust = context.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);
                if (cust != null)
                {
                    cust.CompanyName = customer.CompanyName;
                    cust.ContactName = customer.ContactName;
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

    }
}