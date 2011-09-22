using System;
using Huddle.Clients.Authentication;

namespace Huddle.Clients
{
    public interface IApiCall
    {
        ICodec Codec { get; }

        event EventHandler<ApiCall.OnResponseEventArgs> RequestComplete;

        ApiResponse<TResult> Get<TResult>(string uri) where TResult : class;

        ApiResponse<TResult> Post<TResult>(string uri) where TResult : class;

        ApiResponse<TResult> Post<TResult>(string uri, object data) where TResult : class;

        ApiResponse<TResult> Put<TResult>(string resourceUri, object resourceToPut) where TResult : class;

        ApiResponse<TResult> Delete<TResult>(string uri) where TResult : class;

        void SetRequestAuthenticationMechanism(IRequestAuthenticationMechanism mechanism);
    }
}