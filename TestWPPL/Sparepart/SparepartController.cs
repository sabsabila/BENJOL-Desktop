using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestWPPL.Model;
using Velacro.Api;
using Velacro.Basic;

namespace TestWPPL.Sparepart
{
    class SparepartController : MyController
    {
        public SparepartController(IMyView _myView) : base(_myView)
        {

        }

        public async void requestSpareparts(String token)
        {
            //Console.WriteLine("ini pas request api sparepart");
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/mySpareparts")
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setItem);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject()["token"]);
            //client.setAuthorizationToken(response.getJObject()["access_token"].ToString());
        }

        public async void addSparepart(ObjectSparepart sparepart, String token)
        {
            //Console.WriteLine("ini pas request api sparepart");
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("name", sparepart.name)
                .addParameters("price", sparepart.price.ToString())
                .addParameters("stock", sparepart.stock.ToString())
                .setEndpoint("api/sparepart")
                .setRequestMethod(HttpMethod.Post);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject()["token"]);
            //client.setAuthorizationToken(response.getJObject()["access_token"].ToString());
        }

        public async void editSparepart(ObjectSparepart sparepart, int sparepartId, String token)
        {
            Console.WriteLine("ini pas request api sparepart");
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .addParameters("name", sparepart.name)
                .addParameters("price", sparepart.price.ToString())
                .addParameters("stock", sparepart.stock.ToString())
                .setEndpoint("api/sparepart/" + sparepartId)
                .setRequestMethod(HttpMethod.Put);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject()["token"]);
            //client.setAuthorizationToken(response.getJObject()["access_token"].ToString());
        }

        public async void showSparepart(int sparepartId, String token)
        {
            //Console.WriteLine("ini pas request api sparepart");
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/sparepart/" + sparepartId + "/")
                .setRequestMethod(HttpMethod.Get);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setSparepart);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject()["token"]);
            //client.setAuthorizationToken(response.getJObject()["access_token"].ToString());
        }

        public async void deleteSparepart(int sparepartId, String token)
        {
            //Console.WriteLine("ini pas request api sparepart");
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var req = request
                .buildHttpRequest()
                .setEndpoint("api/sparepart/" + sparepartId + "/")
                .setRequestMethod(HttpMethod.Delete);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            //Console.WriteLine(response.getJObject()["token"]);
            //client.setAuthorizationToken(response.getJObject()["access_token"].ToString());
        }

        private void setItem(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                //Console.WriteLine("ini pas get sparepart");
                //Console.WriteLine(_response.getParsedObject<Spareparts>().spareparts.Count());
                getView().callMethod("setSparepart", _response.getParsedObject<Spareparts>().spareparts);
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

        private void setSparepart(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
                Console.WriteLine("ini masuk edit page");
                Console.WriteLine("sparepart name : " + _response.getParsedObject<ItemSparepart>().spareparts.name);
                getView().callMethod("setItem", _response.getParsedObject<ItemSparepart>().spareparts);
            }
        }
    }
}

