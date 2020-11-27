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
    class PickupController : MyController {
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
                String status = _response.getHttpResponseMessage().ReasonPhrase;
            }
        }

        private void onRadioButtonPickup1Checked()
        {
            _status = "Picking up your vehicle";
        }

        private void onRadioButtonPickup2Checked()
        {
            _status = "Processing your service";
        }

        private void onRadioButtonPickup3Checked()
        {
            _status = "Delivering back to you";
        }
    }
}
