using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLibrary.Models
{
    public class PaymentModel
    {

        [Required]
        public string CardNo { get; set; }
        [Required]
        public string CardName { get; set; }
        [Required]
        public string CardExp { get; set; }
        [Required]
        public string CardCvv { get; set; }
        public string Message { get; set; }
        public string ApiLoginID { get; set; }
        public string ApiTransactionKey { get; set; }
        public decimal? TransactionAmount { get; set; }
    }

}
