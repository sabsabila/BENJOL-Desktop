using System;
using System.IO;
using System.Net.Http;
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
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to load profile");
        }

        public async void editProfile(MyList<MyFile> files, String[] password, ObjectBengkel bengkel, String token)
        {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(bengkel.name), "name");
            formContent.Add(new StringContent(bengkel.phone), "phone_number");
            formContent.Add(new StringContent(bengkel.email), "email");
            formContent.Add(new StringContent(bengkel.address), "address");
            formContent.Add(new StringContent("PUT"), "_method");
            if (password[0] != null && password[1] != null)
            {
                formContent.Add(new StringContent(password[0]), "oldPassword");
                formContent.Add(new StringContent(password[1]), "newPassword");
            }
            if (files.Count > 0)
                formContent.Add(new StreamContent(new MemoryStream(files[0].byteArray)), "profile_picture", files[0].fullFileName);

            var multiPartRequest = request
             .buildMultipartRequest(new MultiPartContent(formContent))
             .setEndpoint("api/bengkel")
             .setRequestMethod(HttpMethod.Post);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to edit profile");
            else if(response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Unauthorized"))
                getView().callMethod("setFailStatus", "Old password doesn't match ");
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
                getView().callMethod("setStatus", _response.getJObject()["message"].ToString());
            }
        }

        private void setLogoutStatus(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                getView().callMethod("setLogoutStatus", _response.getJObject()["message"].ToString());
            }
        }
    }
}
