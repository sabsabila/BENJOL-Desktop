using System;
using Velacro.Api;
using Velacro.Basic;
using TestWPPL.Model;
using System.Net.Http;

namespace TestWPPL.Payment
{
    class PaymentController : MyController
    {
        String _paymentStatus;
        public PaymentController(IMyView _myView) : base(_myView)
        {

        }

        public async void showPayments(String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/bengkelPayment")
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setItem);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to load payments");
        }

        public async void updatePaymentStatus(int _payment_id, String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("status", _paymentStatus)
                .setEndpoint("api/finishPayment/" + _payment_id)
                .setRequestMethod(HttpMethod.Put);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to edit payment");
        }

        private void setItem(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                Console.WriteLine("BAWAH");
                getView().callMethod("setPayment", _response.getParsedObject<Payments>().payments);


            }
        }

        private void setStatus(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setStatus", _response.getJObject()["message"].ToString());
            }
        }

        public void onRadioButtonPayment1Checked()
        {
            _paymentStatus = "paid";
        }

        public void onRadioButtonPayment2Checked()
        {
            _paymentStatus = "unpaid";
        }


    }
}
