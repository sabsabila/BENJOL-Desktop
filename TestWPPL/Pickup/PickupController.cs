using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Pickup
{
    class PickupController : MyController
    {
        String _status;

        public PickupController(IMyView _myView) : base(_myView)
        {

        }

        public async void pickup(int _bookingId, String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("status", _status)
                .setEndpoint("api/pickup/" + _bookingId + "/")
                .setRequestMethod(HttpMethod.Put);

            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
        }

        private void setStatus(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                Console.WriteLine(_response.getJObject()["message"]);
                getView().callMethod("setStatus", _response.getJObject()["message"].ToString());
            }
        }

        public void onRadioButtonPickup1Checked()
        {
            _status = "picking up";
        }

        public void onRadioButtonPickup2Checked()
        {
            _status = "processing";
        }

        public void onRadioButtonPickup3Checked()
        {
            _status = "delivering";
        }
    }
}