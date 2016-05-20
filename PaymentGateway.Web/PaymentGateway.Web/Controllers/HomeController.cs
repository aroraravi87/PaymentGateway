using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using System.Configuration;
using CustomLibrary.Models;

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
            if (ModelState.IsValid)
            {

                if (objPaymentModel != null)
                {
                    objPaymentModel.ApiLoginID = ConfigurationManager.AppSettings["APIloginID"].ToString();
                    objPaymentModel.ApiTransactionKey = ConfigurationManager.AppSettings["ApiTransactionKey"].ToString();

                    HomeController.Run(objPaymentModel);

                     objPaymentModel.Message = "Transaction done sucessfull !!!";
                    return View(objPaymentModel);

                }
                objPaymentModel.Message = "Transaction Abort !!!";
                return View(objPaymentModel);
            }
            return View(objPaymentModel);
        }



        public static void Run(PaymentModel objPaymentModel)
        {

            try
            {
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

                // define the merchant information (authentication / transaction id)
                ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                {
                    name = objPaymentModel.ApiLoginID,
                    ItemElementName = ItemChoiceType.transactionKey,
                    Item = objPaymentModel.ApiTransactionKey,
                };

                var creditCard = new creditCardType
                {
                    cardNumber = objPaymentModel.CardNo.ToString(),//"4111111111111111",
                    expirationDate = objPaymentModel.CardExp,//"0718"
                };

                //standard api call to retrieve response
                var paymentType = new paymentType { Item = creditCard };

                var transactionRequest = new transactionRequestType
                {
                    transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),   // charge the card
                    amount = objPaymentModel.TransactionAmount.HasValue?objPaymentModel.TransactionAmount.Value:2999,//update that value 
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
            catch (Exception ex)
            {
                
                throw;
            }

        }


    }

    
}