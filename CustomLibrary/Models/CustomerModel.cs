using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CustomLibrary.Models
{
    public class CustomerModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public Customer objCustomer { get; set; }
        public string  ErrorMessage { get; set; }
        public CustomerModel()
        {
           
            InitilizeModel();

        }

        private void InitilizeModel()
        {
           
            Customers =CodeFirstDBAccess.GetCustomers();

        }

       }

    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public string FullName { get; set; }

        public int ContactNo { get; set; }
        //public int Pincode { get; set; }
       
    }

    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UnitPrice { get; set; }
         [ForeignKey("Product")]
        public int PId { get; set; }
        [ForeignKey("Customer")]
        public int CID { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }

    }
     
     public class Product
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductPrice { get; set; }
        [ForeignKey("Seller")]
        public int SID { get; set; }
        public Seller Seller { get; set; }


    }
     public class Seller
    {
        [Key]
        public int Id { get; set; }
        public string SellerName { get; set; }
        public int SellerContactNo { get; set; }
        public int SellerAddress { get; set; }
    }



   
}
