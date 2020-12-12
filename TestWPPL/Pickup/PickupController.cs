using System;
using System.Net.Http;
using TestWPPL.Model;
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
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to edit pickup");
        }

        public async void requestPickup(String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/myPickups")
                .setRequestMethod(HttpMethod.Get);

            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setPickup);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to load pickup");
        }

        public async void requestUser(int _bookingId, String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/userInfo/" + _bookingId + "/")
                .setRequestMethod(HttpMethod.Get);

            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setUser);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to load user");
        }

        private void setStatus(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setStatus", _response.getJObject()["message"].ToString());
            }
        }

        private void setUser(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setUser", _response.getParsedObject<ItemUser>().user);
            }
        }

        private void setPickup(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                getView().callMethod("setPickup", _response.getParsedObject<Pickups>().pickups);
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