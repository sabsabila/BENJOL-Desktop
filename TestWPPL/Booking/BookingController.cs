using System;
using System.Net.Http;
using Velacro.Api;
using Velacro.Basic;
using TestWPPL.Model;

namespace TestWPPL.Booking
{
    public class BookingController : MyController
    {
        public BookingController(IMyView _myView) : base(_myView)
        {

        }

        public async void booking(String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/bengkelBooking")
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setItem);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to load bookings");
        }

        public async void statusBooking(int _booking_id, String _status, String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("status", _status)
                .setEndpoint("api/bookingStatus/" + _booking_id)
                .setRequestMethod(HttpMethod.Put);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to edit");
        }

        private void setItem(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setBooking", _response.getParsedObject<Bookings>().booking);
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
    }
}
