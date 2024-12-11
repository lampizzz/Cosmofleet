
//-----------------------------------------------------------------------------
// <auto-generated>
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using Unity.Services.Authentication.Server.Shared;

namespace Unity.Services.Authentication.Server.Proxy.Generated
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    internal interface IProxyApi : IApiAccessor
    {
        /// <summary>
        /// Get Token (V4)
        /// </summary>
        /// <remarks>
        /// Retrieve a token through a local proxy
        /// </remarks>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TokenResponse</returns>
        System.Threading.Tasks.Task<ApiResponse<TokenResponse>> GetTokenAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    internal partial class ProxyApi : IProxyApi
    {        
        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public IApiClient Client { get; }
        
        /// <summary>
        /// Gets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public IApiConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="apiClient">The client interface for synchronous API access.</param>
        public ProxyApi(IApiClient apiClient)
        {      
            if (apiClient == null) throw new ArgumentNullException("apiClient");

            this.Client = apiClient;
            this.Configuration = new ApiConfiguration()
            {
                BasePath = "http://localhost:8086"
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="apiClient">The client interface for synchronous API access.</param>
        /// <param name="apiConfiguration">The configuration object.</param>
        public ProxyApi(IApiClient apiClient, IApiConfiguration apiConfiguration)
        {      
            if (apiClient == null) throw new ArgumentNullException("apiClient");
            if (apiConfiguration == null) throw new ArgumentNullException("apiConfiguration");

            this.Client = apiClient;
            this.Configuration = apiConfiguration;
        }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <returns>The base path</returns>
        public string GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Get Token (V4) Retrieve a token through a local proxy
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of TokenResponse</returns>
        public async System.Threading.Tasks.Task<ApiResponse<TokenResponse>> GetTokenAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            ApiRequestOptions localRequestOptions = new ApiRequestOptions();

            string[] _contentTypes = new string[] {
            };

            // to determine the Accept header
            string[] _accepts = new string[] {
                "application/json"
            };

            var localContentType = ApiUtils.SelectHeaderContentType(_contentTypes);
            if (localContentType != null)
            {
                localRequestOptions.HeaderParameters.Add("Content-Type", localContentType);
            }

            var localAccept = ApiUtils.SelectHeaderAccept(_accepts);
            if (localAccept != null)
            {
                localRequestOptions.HeaderParameters.Add("Accept", localAccept);
            }


            localRequestOptions.Operation = "ProxyApi.GetToken";


            // make the HTTP request
            return await this.Client.GetAsync<TokenResponse>("/v4/token", localRequestOptions, this.Configuration, cancellationToken);
        }
    }
}
