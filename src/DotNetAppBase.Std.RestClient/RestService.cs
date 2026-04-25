using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DotNetAppBase.Std.Library.ComponentModel.Model.Svc;
using DotNetAppBase.Std.RestClient.Contracts;
using Flurl;
using Flurl.Http;

namespace DotNetAppBase.Std.RestClient
{
    public class RestService
    {
        private readonly HttpClient _client;
        private readonly IRestController _controller;

        public RestService(IRestController controller)
        {
            _controller = controller;

            _client = new HttpClient();
        }

        public async Task<TypedResult<T>> Execute<T>(Func<IFlurlRequest, Task<TypedResult<T>>> customizeAction)
        {
            try
            {
                var url = new Url(_controller.BaseUrl());

                return await customizeAction(
                    _controller.Intercept(
                        url.WithTimeout(_controller.DefaultTimeout)));
            }
            catch (FlurlHttpException ex)
            {
                return TypedResult<T>.Exception(ex);
            }
        }

        public async Task<TypedResult<T>> ExecuteWrapped<T>(Func<IFlurlRequest, Task<T>> customizeAction)
        {
            try
            {
                var url = new Url(_controller.BaseUrl());

                var data = await customizeAction(_controller.Intercept(url.WithTimeout(_controller.DefaultTimeout)));

                return TypedResult<T>.Success(data);
            }
            catch (FlurlHttpException ex)
            {
                return TypedResult<T>.Exception(ex);
            }
        }

        public async Task<bool> Get<T>(Action<List<T>> onSuccess, Action<Exception> onError = null)
        {
            try
            {
                var result = await _controller.BaseUrl().GetJsonAsync<List<T>>();

                onSuccess?.Invoke(result);

                return true;
            }
            catch (FlurlHttpException ex)
            {
                onError?.Invoke(ex);
            }

            return false;
        }

        public async Task<bool> GetById<T>(int id, Action<T> onSuccess, Action<Exception> onError = null)
        {
            try
            {
                var result = await _controller
                    .BaseUrl()
                    .AppendPathSegment(new {id})
                    .GetJsonAsync<T>();

                onSuccess?.Invoke(result);

                return true;
            }
            catch (FlurlHttpException ex)
            {
                onError?.Invoke(ex);
            }

            return false;
        }
    }
}