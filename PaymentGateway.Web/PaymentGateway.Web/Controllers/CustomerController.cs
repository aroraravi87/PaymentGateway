using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomLibrary;
using CustomLibrary.Models;

namespace PaymentGateway.Web.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            CustomerModel objModel=new CustomerModel();
            objModel.Customers = CodeFirstDBAccess.GetCustomers();
            return View(objModel);
        }

        [HttpPost]
        public ActionResult Index(CustomerModel objCustModel)
        {
            CodeFirstDBAccess.SaveCustomers(objCustModel);
            return View(objCustModel);
        }

    }
}