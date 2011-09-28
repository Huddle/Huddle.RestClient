using System;
using Huddle.Clients.Authentication;

namespace Huddle.Clients
{
    public interface IApiCall
    {
        ICodec Codec { get; }

        event EventHandler<ApiCall.OnResponseEventArgs> RequestComplete;

        IApiResponse<TResult> Get<TResult>(string uri) where TResult : class;

        IApiResponse<TResult> Post<TResult>(string uri) where TResult : class;

        IApiResponse<TResult> Post<TResult>(string uri, object data) where TResult : class;

        IApiResponse<TResult> Put<TResult>(string resourceUri, object resourceToPut) where TResult : class;

        IApiResponse<TResult> Delete<TResult>(string uri) where TResult : class;

        void SetRequestAuthenticationMechanism(IRequestAuthenticationMechanism mechanism);
    }
}