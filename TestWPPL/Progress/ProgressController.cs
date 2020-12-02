using System;
using System.Net.Http;
using Velacro.Api;
using Velacro.Basic;
using RestSharp;
using System.Windows; 

namespace TestWPPL.Progress
{
    public class ProgressController : MyController
    {
        public ProgressController(IMyView _myView) : base(_myView) { }

        public async void editProgress(string _startTime, string _endTime, string token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();
            var req = request
                .buildHttpRequest()
                .addHeaders("Authorization", "Bearer"+token)
                .addHeaders("Content - Type", "application/x-www-form-urlencoded")
                .addHeaders("Connection","keep-alive")
                .addHeaders("Accept", "*/*")
                .addHeaders("Accept-Encoding", "gzip,deflate,br")
                .addParameters("start_time", _startTime)
                .addParameters("end_time", _endTime)
                .setEndpoint("api/booking/{id}")
                .setRequestMethod(HttpMethod.Put);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setViewProgressStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            Console.WriteLine(response.getHttpResponseMessage().ToString());
        }


        private void setViewProgressStatus(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setProgressStatus", _response.getJObject()["message"].ToString());
            }
        }


    }
}
