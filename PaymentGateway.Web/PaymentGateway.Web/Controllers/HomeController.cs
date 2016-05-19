using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;


namespace PaymentGateway.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new PaymentModel());
        }




        [HttpPost]
        public ActionResult Index(PaymentModel objPaymentModel)
        {
            if (objPaymentModel != null)
            {
                HomeController.Run(ApiLoginID: "2vdVcR34GRC", ApiTransactionKey: "5Q23z3c52m69ExWq");

                objPaymentModel.Message = "Transaction done sucessfull !!!";
                return View(objPaymentModel);

            }
            objPaymentModel.Message = "Transaction Abort !!!";
            return View(objPaymentModel);
        }



        public static void Run(String ApiLoginID, String ApiTransactionKey)
        {


            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = "4111111111111111",
                expirationDate = "0718"
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
                amount = 133.45m,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            if (response.messages.resultCode == messageTypeEnum.Ok)
            {
                if (response.transactionResponse != null)
                {
                    Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                }
            }
            else
            {
                Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                if (response.transactionResponse != null)
                {
                    Console.WriteLine("Transaction Error : " + response.transactionResponse.errors[0].errorCode + " " + response.transactionResponse.errors[0].errorText);
                }
            }

        }


    }

    public class PaymentModel
        {
            public long CardNo { get; set; }
            public string CardName { get; set; }
            public string SecretKey { get; set; }
            public string CardCvv { get; set; }
            public string Message { get; set; }
        }
    
}