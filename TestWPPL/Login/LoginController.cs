using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Login {
    public class LoginController : MyController{
        public LoginController(IMyView _myView) : base(_myView){
            
        }

        public async void login(string _email, string _password) {
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("email", _email)
                .addParameters("password", _password)
                .setEndpoint("api/login/")
                .setRequestMethod(HttpMethod.Post);
            client.setOnSuccessRequest(setUserToken);
           var response = await client.sendRequest(request.getApiRequestBundle());
           if(response.getHttpResponseMessage().StatusCode.ToString().Equals("Unauthorized"))
                getView().callMethod("setStatus", "Wrong e-mail or password");
        }

        private void setUserToken(HttpResponseBundle _response){
            if (_response.getHttpResponseMessage().Content != null) {
                getView().callMethod("saveToken", _response.getJObject()["token"].ToString());
            }
        }
    }
}
