using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestWPPL.Model;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Profile
{
    class ProfileController : MyController
    {
        public ProfileController(IMyView _myView) : base(_myView)
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
            //Console.WriteLine(response.getJObject()["token"]);
            //client.setAuthorizationToken(response.getJObject()["access_token"].ToString());
        }

        public async void editProfile(MyList<MyFile> files, ObjectBengkel bengkel, String token)
        {
            Console.WriteLine("ini pas request edit bengkel");
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(bengkel.name), "name");
            formContent.Add(new StringContent(bengkel.phone), "phone_number");
            formContent.Add(new StringContent(bengkel.email), "email");
            formContent.Add(new StringContent(bengkel.address), "address");
            formContent.Add(new StringContent("PUT"), "_method");
            if (files.Count > 0)
                formContent.Add(new StreamContent(new MemoryStream(files[0].byteArray)), "picture", files[0].fullFileName);

            Console.WriteLine(bengkel.name);
            var multiPartRequest = request
             .buildMultipartRequest(new MultiPartContent(formContent))
             .setEndpoint("api/bengkel")
             .setRequestMethod(HttpMethod.Post);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            Console.WriteLine(response.getHttpResponseMessage().ToString());
        }

        private void setItem(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setProfile", _response.getParsedObject<Bengkels>().bengkel);
            }
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
    }
}
