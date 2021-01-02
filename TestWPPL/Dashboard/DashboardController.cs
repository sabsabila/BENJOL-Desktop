using System;
using System.Net.Http;
using TestWPPL.Model;
using TestWPPL.Model.TestWPPL.Model;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Dashboard
{
    public class DashboardController : MyController
    {
        public DashboardController(IMyView _myView) : base(_myView)
        {

        }

        public async void getProfile(String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/bengkel")
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setItem);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to load profile");
        }

        public async void getBookings(String _status, String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("status", _status)
                .setEndpoint("api/booking/count")
                .setRequestMethod(HttpMethod.Post);
            client.setAuthorizationToken(token);
            if(_status.Equals("finished"))
                client.setOnSuccessRequest(setBookingsDone);
            else if(_status.Equals("upcoming"))
                client.setOnSuccessRequest(setBookingsUpcoming);
            else if(_status.Equals("canceled"))
                client.setOnSuccessRequest(setBookingsCanceled);

            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to load statistics");
        }

        public async void getRevenue(String _status, String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("status", _status)
                .setEndpoint("api/revenue/count")
                .setRequestMethod(HttpMethod.Post);
            client.setAuthorizationToken(token); 
            if (_status.Equals("paid"))
                client.setOnSuccessRequest(setRevenue);
            else if (_status.Equals("unpaid"))
                client.setOnSuccessRequest(setUnpaidRevenue);
            else if (_status.Equals("pending"))
                client.setOnSuccessRequest(setPendingRevenue);

            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to load statistics");
        }

        private void setItem(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setProfile", _response.getParsedObject<Bengkels>().bengkel);
            }
        }

        private void setBookingsDone(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setDone", _response.getParsedObject<BookingCount>().count);
            }
        }

        private void setBookingsCanceled(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setCanceled", _response.getParsedObject<BookingCount>().count);
            }
        }

        private void setBookingsUpcoming(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setUpcoming", _response.getParsedObject<BookingCount>().count);
            }
        }

        private void setRevenue(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setRevenue", _response.getParsedObject<RevenueCount>().count);
            }
        }

        private void setUnpaidRevenue(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setUnpaidRevenue", _response.getParsedObject<RevenueCount>().count);
            }
        }

        private void setPendingRevenue(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setPendingRevenue", _response.getParsedObject<RevenueCount>().count);
            }
        }
    }
}
