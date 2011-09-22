using System;
using System.Collections.Generic;
using Huddle.Clients.ApiCallObservers.HttpStatusCodeHandlers;
using log4net;

namespace Huddle.Clients.ApiCallObservers
{
    public class HttpStatusCodeExceptionHandler : IApiCallObserver
    {
        private readonly IEnumerable<IHttpStatusCodeExceptionHandler> _httpStatusCodeExceptionHandlers;

        public ILog Log { get; set; }

        public HttpStatusCodeExceptionHandler()
        {
            _httpStatusCodeExceptionHandlers = new ObserverResolver<IHttpStatusCodeExceptionHandler>();
            Log = LogManager.GetLogger(typeof(HttpStatusCodeExceptionHandler));
            Log.InfoFormat("{0} http status code handlers found", new List<IHttpStatusCodeExceptionHandler>(_httpStatusCodeExceptionHandlers).Count);
        }

        public void RegisterApiCall(IApiCall call)
        {
            call.RequestComplete += RequestComplete;
        }

        public void RequestComplete(object sender, ApiCall.OnResponseEventArgs e)
        {
            var response = e.Response;
            var statusCode = response.Status;

            Log.DebugFormat("Getting http status code handler's for the status {0} ({1})", (int)statusCode, statusCode);

            var handlers = new List<IHttpStatusCodeExceptionHandler>();
            foreach (var httpStatusCodeExceptionHandler in _httpStatusCodeExceptionHandlers)
            {
                if (httpStatusCodeExceptionHandler.StatusCode == statusCode)
                {
                    handlers.Add(httpStatusCodeExceptionHandler);
                }
            }

            switch (handlers.Count)
            {
                case 0:
                    Log.DebugFormat("A handler was not found for the status code {0} ({1})", (int)statusCode, statusCode);
                    break;

                case 1:
                    Log.DebugFormat("The handler {0} was returned for the status code {0} ({1})", (int)statusCode, statusCode);
                    handlers[0].Handle(response);
                    break;

                default:
                    Log.ErrorFormat("{0} handlers were found for the status code {1} {2}", handlers.Count, (int)statusCode, statusCode);
                    throw new ArgumentOutOfRangeException(string.Format("More than one exception handler found for the status code {0}", statusCode));
            }
        }

    }
}