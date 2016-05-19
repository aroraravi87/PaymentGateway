using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomLibrary.Models;

namespace CustomLibrary
{
  public static class  CodeFirstDBAccess
    {
        public static IEnumerable<Customer> GetCustomers()
        {
            using (var context = new DbEntityContext())
            {
                return context.Customers.ToList();
            }
        }

        public static CustomerModel SaveCustomers(CustomerModel objCustomerModel)
        {
            using (var context = new DbEntityContext())
            {
                try
                {

                    context.Customers.Add(objCustomerModel.objCustomer);
                    context.SaveChanges();
                    objCustomerModel.ErrorMessage = "Record Inserted";
                    objCustomerModel.Customers=GetCustomers();
                    return objCustomerModel;
                }
                catch (Exception ex)
                {
                    objCustomerModel.ErrorMessage = ex.Message;
                }
                return null;
            }
        }
    }
}
