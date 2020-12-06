﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to load Spareparts");
        }

        public async void addSparepart(MyList<MyFile> files, ObjectSparepart sparepart, String token)
        {
            Console.WriteLine("ini di add sparepart");
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(sparepart.name), "name");
            formContent.Add(new StringContent(sparepart.price.ToString()), "price");
            formContent.Add(new StringContent(sparepart.stock.ToString()), "stock");
            if (files.Count > 0)
                formContent.Add(new StreamContent(new MemoryStream(files[0].byteArray)), "picture", files[0].fullFileName);

            var multiPartRequest = request
            .buildMultipartRequest(new MultiPartContent(formContent))
            .setEndpoint("api/sparepart")
            .setRequestMethod(HttpMethod.Post);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to add sparepart");
        }

        public async void editSparepart(MyList<MyFile> files, ObjectSparepart sparepart, int sparepartId, String token)
        {
            Console.WriteLine("ini pas request edit sparepart");
            var client = new ApiClient(ApiConstant.BASE_URL);
            var request = new ApiRequestBuilder();

            var formContent = new MultipartFormDataContent();
            formContent.Add(new StringContent(sparepart.name), "name");
            formContent.Add(new StringContent(sparepart.price.ToString()), "price");
            formContent.Add(new StringContent(sparepart.stock.ToString()), "stock");
            formContent.Add(new StringContent("PUT"), "_method");
            if (files.Count > 0)
                formContent.Add(new StreamContent(new MemoryStream(files[0].byteArray)), "picture", files[0].fullFileName);

            Console.WriteLine(sparepart.name);
            var multiPartRequest = request
             .buildMultipartRequest(new MultiPartContent(formContent))
             .setEndpoint("api/sparepart/" + sparepartId)
             .setRequestMethod(HttpMethod.Post);
            client.setAuthorizationToken(token);
            client.setOnSuccessRequest(setStatus);
            var response = await client.sendRequest(request.getApiRequestBundle());
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to edit spareparts");
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
            if (response.getHttpResponseMessage().ReasonPhrase.ToString().Equals("Internal Server Error"))
                getView().callMethod("setFailStatus", "Failed to delete spareparts");
        }

        private void setItem(HttpResponseBundle _response)
        {
            if (_response.getHttpResponseMessage().Content != null)
            {
                string status = _response.getHttpResponseMessage().ReasonPhrase;
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

