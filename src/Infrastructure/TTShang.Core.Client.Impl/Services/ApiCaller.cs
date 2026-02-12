// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using TTShang.Core.Client.Authorization.Events;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TTShang.Core.Client.Impl.Services
{
    /// <summary>
    /// api 调用器
    /// </summary>
    [ScopedService]
    public class ApiCaller : IApiCaller
    {
        private readonly JsonSerializerOptions? jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        private readonly HttpClient httpClient;
        private readonly IClientLogger log;
        private readonly IEventBus eventBus;
        private readonly ILocalizationLocalizer localizer;
        /// <summary>
        /// api 调用器
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="log"></param>
        /// <param name="eventBus"></param>
        /// <param name="localizer"></param>
        public ApiCaller(IClientLogger log, IEventBus eventBus, ILocalizationLocalizer localizer, HttpClient httpClient)
        {
            jsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

            this.log = log;
            this.eventBus = eventBus;
            this.localizer = localizer;
            this.httpClient = httpClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        private string ConvertErrorMessage(HttpResponseMessage response)
        {
            var statusCode = response.StatusCode;
            string msg = nameof(SharedLocalResource.ResuqesFail);
            //特殊状态提示
            switch (statusCode)
            {
                case HttpStatusCode.Unauthorized:
                    msg = nameof(SharedLocalResource.Unauthorized);
                    break;
                case HttpStatusCode.Forbidden:
                    msg = nameof(SharedLocalResource.Forbidden);
                    break;
            }
            return $"{localizer[msg]}：{response.RequestMessage?.Method.ToString()} {response.RequestMessage?.RequestUri?.AbsolutePath}";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="result"></param>
        private void LogError<TResponse>(ApiResult<TResponse>? result)
        {
            bool loged=false;
            //验证失败 没有自定义错误code
            if (result != null && result.StatusCode != null && result.StatusCode.Equals(400) && result.Errors != null && ExceptionCode.ModelValidateFailed.ToString().Equals(result.ErrorCode?.ToString()))
            {
                string? json = result.Errors.ToString();
                if (!string.IsNullOrEmpty(json))
                {
                    //验证失败
                    Dictionary<string, string[]>? resultError = JsonSerializer.Deserialize<Dictionary<string, string[]>>(json);
                    if (resultError != null)
                    {
                        resultError.Values.SelectMany(x => x).ForEach(x =>
                        {
                            log.Error(x);
                            loged=true;
                        });
                    }
                }
            }
            //未记录
            if(!loged)
            {
                log.Error(result?.Errors?.ToString() ?? string.Empty, result?.StatusCode);
            }
            //时间戳过期
            if (result != null && result.StatusCode == 400 && ExceptionCode.Refreshtoken_No_Exist_Or_Expire.ToString().Equals(result.ErrorCode?.ToString()))
            {
                eventBus.Publish(new RefreshTokenErrorEvent());
            }
        }
        /// <summary>
        /// 调用包装
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="func"></param>
        /// <param name="retry">重试次数</param>
        /// <returns></returns>
        async Task<TResponse> ResponseHandle<TResponse>(Func<HttpClient, Task<HttpResponseMessage>> func, int retry = 0)
        {
            try
            {
                HttpResponseMessage httpResponse = await func.Invoke(httpClient);
                if (HttpStatusCode.OK.Equals(httpResponse.StatusCode))
                {
                    var result = await httpResponse.Content.ReadFromJsonAsync<ApiResult<TResponse>>(jsonSerializerOptions);
                    if (result == null || !result.Succeeded)
                    {
                        LogError(result);

                        //TODO:待client全局异常捕获完成时，在这里抛出异常即可
#pragma warning disable CS8603 // 可能返回 null 引用。
                        return default;
#pragma warning restore CS8603 // 可能返回 null 引用。
                    }
                    //TODO:待client全局异常捕获完成时，在这里抛出异常即可
#pragma warning disable CS8603 // 可能返回 null 引用。
                    return result.Data;
#pragma warning restore CS8603 // 可能返回 null 引用。
                }
                //请求失败 
                log.Error(ConvertErrorMessage(httpResponse), (int)httpResponse.StatusCode);
                //TODO:待client全局异常捕获完成时，在这里抛出异常即可
#pragma warning disable CS8603 // 可能返回 null 引用。
                return default;
#pragma warning restore CS8603 // 可能返回 null 引用。
            }
            catch (Exception ex)
            {
                log.Error($"{localizer[nameof(SharedLocalResource.ResuqesException)]}[{ex.Message}]", -999, ex);
                //todo:待client全局异常捕获完成时，在这里抛出异常即可
#pragma warning disable CS8603 // 可能返回 null 引用。
                return default;
#pragma warning restore CS8603 // 可能返回 null 引用。
            }
        }
        /// <summary>
        /// 调用包装
        /// </summary>
        /// <param name="func"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        async Task ResponseHandle(Func<HttpClient, Task<HttpResponseMessage>> func, int retry = 0)
        {
            try
            {
                HttpResponseMessage httpResponse = await func.Invoke(httpClient);
                if (!HttpStatusCode.OK.Equals(httpResponse.StatusCode))
                {
                    //请求失败 
                    log.Error(ConvertErrorMessage(httpResponse), (int)httpResponse.StatusCode);
                }
                else
                {
                    var result = await httpResponse.Content.ReadFromJsonAsync<ApiResult<object>>(jsonSerializerOptions);
                    if (result == null || !result.Succeeded)
                    {
                        LogError(result);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error($"{localizer[nameof(SharedLocalResource.ResuqesException)]}[{ex.Message}]", -999, ex);
            }
        }
        /// <summary>
        /// get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private string GetUrl(string url, IDictionary<string, object?>? queryString = null)
        {
            if (queryString != null && queryString.Count > 0)
            {
                url = QueryHelpers.AddQueryString(url, queryString.ToDictionary(p => p.Key, p => p.Value == null ? "" : p.Value.ToString()));
            }
            return url;
        }
        /// <summary>
        /// get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        private string GetUrl(string url, List<KeyValuePair<string, object?>> queryString)
        {
            if (queryString != null && queryString.Count() > 0)
            {
                foreach (KeyValuePair<string, object?> item in queryString)
                {
                    url = QueryHelpers.AddQueryString(url, item.Key, item.Value?.ToString() ?? "");
                }
            }
            return url;
        }
        #region post
        /// <summary>
        /// post 有参数 无返回
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task PostAsync<TRequest>(string url, TRequest? request = default)
        {
            return ResponseHandle((HttpClient) =>
            {
                if (request == null)
                {
                    //todo:request 等于null时 给空 是否会有问题
                    return HttpClient.PostAsJsonAsync(url, string.Empty);
                }
                return HttpClient.PostAsJsonAsync(url, request);
            });
        }
        /// <summary>
        /// post 有参数 有返回
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest? request = default)
        {
            return ResponseHandle<TResponse>((HttpClient) =>
           {
               return HttpClient.PostAsJsonAsync(url, request);
           });

        }
        /// <summary>
        /// post 无参数 有返回
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task<TResponse> PostWithoutBodyAsync<TResponse>(string url, IDictionary<string, object>? queryString = null)
        {
            if (queryString != null && queryString.Count > 0)
            {
                url = QueryHelpers.AddQueryString(url, queryString.ToDictionary(p => p.Key, p => p.Value == null ? "" : p.Value.ToString()));
            }

            return ResponseHandle<TResponse>((HttpClient) =>
            {
                return HttpClient.PostAsJsonAsync(url, Guid.NewGuid());
            });
        }
        #endregion
        #region get
        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public Task<TResponse> GetAsync<TResponse>(string url)
        {
            return ResponseHandle<TResponse>((HttpClient) =>
            {
                return HttpClient.GetAsync(url); ;
            });

        }
        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task<TResponse> GetAsync<TResponse>(string url, IDictionary<string, object?> queryString)
        {
            return ResponseHandle<TResponse>((HttpClient) =>
             {
                 url = GetUrl(url, queryString);
                 return HttpClient.GetAsync(url); ;
             });

        }
        /// <summary>
        /// get
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task<TResponse> GetAsync<TResponse>(string url, List<KeyValuePair<string, object?>> queryString)
        {
            return ResponseHandle<TResponse>((HttpClient) =>
            {
                url = GetUrl(url, queryString);
                return HttpClient.GetAsync(url); ;
            });

        }
        #endregion
        #region delete
        /// <summary>
        /// delete
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task DeleteAsync(string url, IDictionary<string, object?>? queryString = null)
        {
            return ResponseHandle((HttpClient) =>
             {
                 return HttpClient.DeleteAsync(GetUrl(url, queryString));
             });
        }
        /// <summary>
        /// delete
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public Task<TResponse> DeleteAsync<TResponse>(string url, IDictionary<string, object?>? queryString = null)
        {
            return ResponseHandle<TResponse>((HttpClient) =>
            {
                return HttpClient.DeleteAsync(GetUrl(url, queryString));
            });
        }
        #endregion
        #region put
        /// <summary>
        /// put
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task PutAsync<TRequest>(string url, TRequest? request = default)
        {
            return ResponseHandle((HttpClient) =>
            {
                return HttpClient.PutAsJsonAsync(url, request);
            });
        }
        /// <summary>
        /// put
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="url"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<TResponse> PutAsync<TRequest, TResponse>(string url, TRequest? request = default)
        {
            return ResponseHandle<TResponse>((HttpClient) =>
           {
               return HttpClient.PutAsJsonAsync(url, request);
           });
        }

        #endregion
    }
}
