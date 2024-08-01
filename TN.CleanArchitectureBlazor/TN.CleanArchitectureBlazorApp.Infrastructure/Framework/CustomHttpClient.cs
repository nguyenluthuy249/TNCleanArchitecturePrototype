using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TN.CleanArchitectureBlazorApp.Application.Framework;
using TN.CleanArchitectureBlazorApp.Application.Framework.Exceptions;
using TN.CleanArchitectureBlazorApp.Application.Models;

namespace TN.CleanArchitectureBlazorApp.Infrastructure.Framework
{
    public class CustomHttpClient : ICustomHttpClient
    {
        private readonly HttpClient _httpClient;

        public CustomHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResult<T>> Get<T>(string path)
        {
            return await ExecuteRest<T>(path, null, HttpMethod.Get);
        }

        public async Task<ApiResult<T>> Post<T>(string path, object body)
        {
            return await ExecuteRest<T>(path, body, HttpMethod.Post);
        }

        public async Task<ApiResult<T>> Put<T>(string path, object body)
        {
            return await ExecuteRest<T>(path, body, HttpMethod.Put);
        }

        public async Task<ApiResult<T>> Delete<T>(string path)
        {
            return await ExecuteRest<T>(path, null, HttpMethod.Delete);
        }

        private async Task<ApiResult<T>> ExecuteRest<T>(string path, object? body, HttpMethod method)
        {
            HttpContent byteContent = null;
            if (body is not null)
            {
                var stringContent = JsonConvert.SerializeObject(body);
                var buffer = Encoding.UTF8.GetBytes(stringContent);
                byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            HttpResponseMessage response = null;
            try
            {
                if (method == HttpMethod.Get) response = await _httpClient.GetAsync(path);
                if (method == HttpMethod.Delete) response = await _httpClient.DeleteAsync(path);
                if (method == HttpMethod.Post) response = await _httpClient.PostAsync(path, byteContent);
                if (method == HttpMethod.Put) response = await _httpClient.PutAsync(path, byteContent);
                if (response != null)
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (Exception ex)
            {
                string responseError = await GetResponseError<T>(response);
                throw new ApplicationGeneralException($"Error while {method.Method} '{path}': {ex.Message} {ex.InnerException?.Message} {responseError} {responseError}", ex);
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResult<T>>();
            ValidateResult(result);
            return result;
        }

        private void ValidateResult(ApiResult result)
        {
            if (!result.Succeeded)
            {
                switch (result.Error.Code)
                {
                    case (int)HttpStatusCode.NotFound:
                        throw new ApplicationNotFoundException(result.Error.Message);
                    case (int)HttpStatusCode.Forbidden:
                        throw new ApplicationForbiddenException(result.Error.Message);
                    default:
                        throw new ApplicationGeneralException(result.Error.Message);
                }
            }
        }

        private async Task<string> GetResponseError<T>(HttpResponseMessage response)
        {
            try
            {
                var responseJson = await response?.Content?.ReadFromJsonAsync<ApiResult<T>>();
                return responseJson?.Error?.Message;
            }
            catch
            {
                //It throws exception if the response content is not json format.
                return await response?.Content?.ReadAsStringAsync();
            }
        }


    }
}
