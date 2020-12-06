using System;
using System.Net.Http;
using Velacro.Api;
using Velacro.Basic;
using RestSharp;
using System.Windows;
using TestWPPL.Model;

namespace TestWPPL.Progress
{
    public class ProgressController : MyController
    {
        public ProgressController(IMyView _myView) : base(_myView) { }

        public async void editProgress(int _bookingId, string _startTime, string _endTime, string token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();
            var req = request
                .buildHttpRequest()
                .addParameters("start_time", _startTime)
                .addParameters("end_time", _endTime)
                .setEndpoint("api/booking/" + _bookingId)
                .setRequestMethod(HttpMethod.Put);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setViewProgressStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            Console.WriteLine(response.getHttpResponseMessage());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to edit progress");
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


        private void setViewProgressStatus(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setProgressStatus", _response.getJObject()["message"].ToString());
            }
        }

        private void setUser(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                Console.WriteLine(_response.getJObject()["message"]);
                getView().callMethod("setUser", _response.getParsedObject<ItemUser>().user);
            }
        }
    }
}
