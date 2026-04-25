#region License

// Copyright(c) 2020 GrappTec
// 
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

#endregion

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