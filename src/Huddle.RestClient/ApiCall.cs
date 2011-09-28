using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Huddle.Clients.Authentication;
using Huddle.Clients.DataPrinters;
using Huddle.Clients.Exceptions;
using log4net;

namespace Huddle.Clients
{
    public abstract class ApiCall : IApiCall
    {
        public class OnResponseEventArgs : EventArgs
        {
            public virtual ApiResponse Response { get; internal set; }
        }

        class NoAuthenticationMechanism : IRequestAuthenticationMechanism
        {
            public string GetAuthenticationHeader()
            {
                return "no authentication";
            }
        }

        readonly CookieContainer _cookies;
        readonly Dictionary<string, string> _headers;

        IRequestAuthenticationMechanism _requestAuthenticationMechanism;
        string _accept;
        string _contentType;

        public ILog Log { get; set; }

        public ICodec Codec { get; protected set; }
        
        public int Timeout { get; set; }

        protected IPrintData DataPrinter { get; private set; }

        protected ApiCall() : this(new ObserverResolver<IApiCallObserver>(), new PlainDataPrinter()) { }

        protected ApiCall(IEnumerable<IApiCallObserver> observers, IPrintData dataPrinter)
        {
            Log = LogManager.GetLogger(typeof(ApiCall));

            _headers = new Dictionary<string, string>();
            ServicePointManagerHelper.TrustAllCertificates();
            RegisterObservers(new List<IApiCallObserver>(observers).ToArray());
            Timeout = 100000;
            _cookies = new CookieContainer();
            _requestAuthenticationMechanism = new NoAuthenticationMechanism();
            DataPrinter = dataPrinter;
        }

        protected abstract string Serialize<TPostData>(TPostData data);

        protected ApiResponse<TResult> GetApiResponse<TResult>(WebResponse response)
            where TResult : class
        {
            var data = GetResponse(response);
            var deserialisedObject = Deserialize<TResult>(data);
            return new ApiResponse<TResult>(deserialisedObject, response, DataPrinter);
        }

        protected ApiResponse GetApiResponse(WebResponse response)        
        {
            return new ApiResponse(response, DataPrinter);
        }

        public virtual event EventHandler<OnResponseEventArgs> RequestComplete = delegate { };

        public void SetAccept(string value)
        {
            _accept = value;
        }

        public IApiResponse<TResult> Delete<TResult>(string uri) where TResult : class
        {
            return Execute<TResult>(GetRequest(uri, "DELETE"));
        }

        public void SetRequestAuthenticationMechanism(IRequestAuthenticationMechanism mechanism)
        {
            _requestAuthenticationMechanism = mechanism;
        }

        public virtual IApiResponse<TResult> Get<TResult>(string uri)
            where TResult : class
        {
            return Execute<TResult>(GetRequest(uri, "GET"));
        }

        public IApiResponse Get(string uri)
        {
            return Execute(GetRequest(uri, "GET"));
        }

        public virtual IApiResponse Post(string uri)
        {
            var request = GetRequest(uri, "POST");
            request.ContentLength = 0;
            return Execute(request);
        }

        public virtual IApiResponse Post(string uri, object postData)
        {
            IApiResponse response;
            var serializedObject = Serialize(postData);
            
            try
            {
                var request = PostRequest(uri, serializedObject);

                response = Execute(request);
            }
            catch (Exception ex)
            {
                response = GetApiResponse(null);

                response.Error = true;
                response.Exception = ex;
            }

            return response;
        }

        public virtual IApiResponse<TResult> Post<TResult>(string uri)
            where TResult : class
        {
            var request = GetRequest(uri, "POST");
            request.ContentLength = 0;
            return Execute<TResult>(request);
        }

        public IApiResponse<TResult> Post<TResult>(string uri, object postData)
            where TResult : class
        {
            ApiResponse<TResult> response;
            try
            {
                var request = PostRequest(uri, Serialize(postData));

                response = Execute<TResult>(request);
            }
            catch(HuddleApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                response = GetApiResponse<TResult>(null);
                response.Error = true;
                response.Exception = ex;
            }

            return response;
        }

        public IApiResponse Delete(string uri)
        {
            IApiResponse response;
            try
            {
                var request = GetRequest(uri, "DELETE");

                response = Execute(request);
            }
            catch (HuddleApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                response = GetApiResponse(null);
                response.Error = true;
                response.Exception = ex;
            }

            return response;
        }

        public virtual IApiResponse Put(string uri, object postData)
        {
            IApiResponse response;
            try
            {
                var request = PutRequest(uri, postData);

                response = Execute(request);
            }
            catch (Exception ex)
            {
                response = GetApiResponse(null);

                response.Error = true;
                response.Exception = ex;
            }

            return response;
        }
        
        public IApiResponse<TResult> Put<TResult>(string uri, object postData)
            where TResult : class
        {
            ApiResponse<TResult> response;
            try
            {
                var request = PutRequest(uri, postData);

                response = Execute<TResult>(request);
            }
            catch(HuddleApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                response = GetApiResponse<TResult>(null);
                response.Error = true;
                response.Exception = ex;
            }

            return response;
        }

        public virtual ApiCall AddHeader(string header, string value)
        {
            _headers[header] = value;
            return this;
        }

        public virtual ApiCall AddHeader(HttpRequestHeader header, string value)
        {
            _headers[header.ToString()] = value;
            return this;
        }

        protected void SetContentType(string value)
        {
            _contentType = value;
        }

        protected ApiResponse<TResult> Execute<TResult>(WebRequest request)
            where TResult : class
        {
            ApiResponse<TResult> response;

            try
            {
                response = GetApiResponse<TResult>(request.GetResponse());
            }
            catch (WebException ex)
            {
                response = GetApiResponse<TResult>(ex.Response);

                response.Error = true;
                response.Exception = ex;

                Log.DebugFormat("Error:\nStatus: {0}\nMessage:{1}", ex.Status, ex.Message);
            }

            HandleResponse(response);

            return response;
        }

        protected virtual IApiResponse Execute(WebRequest request)
        {
            ApiResponse response;

            try
            {
                response = GetApiResponse(request.GetResponse());
            }
            catch (WebException ex)
            {
                response = GetApiResponse(ex.Response);

                response.Error = true;
                response.Exception = ex;

                Log.DebugFormat("Error: {0}", ex.Message);
            }

            HandleResponse(response);

            return response;
        }

        protected WebRequest GetRequest(string uri, string method)
        {
            Log.InfoFormat("Calling {0}", uri);

            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.Timeout = Timeout;
            InitializeRequest(request, method);
            return request;
        }

        protected virtual void InitializeRequest(HttpWebRequest request, string method)
        {
            Log.DebugFormat("{0}: {1} with {2}.", method, request.RequestUri.AbsoluteUri, _requestAuthenticationMechanism.GetType().Name);

            request.Method = method;
            request.CookieContainer = _cookies;

            AddHeader(HttpRequestHeader.Authorization, _requestAuthenticationMechanism.GetAuthenticationHeader());

            foreach (var header in _headers)
            {
                request.Headers[header.Key] = header.Value;
            }

            request.Accept = _accept;
        }

        void RegisterObservers(IApiCallObserver[] observers)
        {
            Log.DebugFormat("{0} observer(s) registered for the api call", observers.Length);

            foreach (var observer in observers)
            {
                observer.RegisterApiCall(this);
            }
        }

        void HandleResponse(ApiResponse response)
        {
            response.Codec = Codec;
            response.ApiCall = this;
            RequestComplete(this, new OnResponseEventArgs {Response = response});
        }

        WebRequest PutRequest(string uri, object data)
        {
            var request = GetRequest(uri, "PUT");
            return UploadData(Serialize(data), request);
        }

        WebRequest UploadData(string data, WebRequest request)
        {
            var content = Encoding.UTF8.GetBytes(data);

            request.ContentLength = content.Length;
            request.ContentType = _contentType;
            request.GetRequestStream().Write(content, 0, content.Length);
            request.GetRequestStream().Flush();

            return request;
        }

        WebRequest PostRequest(string uri, string serializedObject)
        {
            var request = GetRequest(uri, "POST");
            return UploadData(serializedObject, request);
        }

        protected static string GetResponse(WebResponse response)
        {
            string data;

            using (var stream = response.GetResponseStream())
            {
                data = new StreamReader(stream).ReadToEnd();
            }

            response.Close();

            return data;
        }

        protected T Deserialize<T>(string data)
        {
            return Codec.Decode<T>(data);
        }
    }
}