using System;
using System.IO;
using System.Net;
using Huddle.Clients.DataPrinters;
using log4net;

namespace Huddle.Clients
{
    public class ApiResponse : IApiResponse
    {
        readonly IPrintData _printer;

        public ILog Log { get; set; }

        public string Location { get; private set; }

        public WebHeaderCollection Headers { get; private set; }

        public ApiCall ApiCall { get; internal set; }

        public virtual ICodec Codec { get; internal set; }

        public bool Error { get; internal set; }

        public Exception Exception { get; internal set; }

        public virtual HttpStatusCode Status { get; private set; }
        
        public string ContentType { get; private set; }

        public string Response { get; private set; }

        internal virtual string ErrorMessage { get { return Exception.Message; } }

        protected ApiResponse()
        {
            _printer = new PlainDataPrinter();
            Log = LogManager.GetLogger(typeof(ApiResponse));
        }

        public ApiResponse(WebResponse response) : this()
        {
            InitialiseResponse(response);
        }

        internal ApiResponse(WebResponse response, IPrintData xmlDataPrinter) : this(response)
        {
            _printer = xmlDataPrinter;
        }

        protected virtual IPrintData GetPrinter()
        {
            return _printer;
        }

        protected void InitialiseResponse(WebResponse response)
        {
            try
            {
                var r = response as HttpWebResponse;

                if (r != null)
                {
                    ContentType = r.ContentType;
                    Status = r.StatusCode;
                    Response = GetResponse(r);
                    Location = r.Headers["Location"];
                    Headers = r.Headers;

                    Log.DebugFormat("Status: {0} ({1})", Status, (int) Status);

                    _printer.Print(Response, Log);
                }
                else
                {
                    Exception = new WebException("No http response");
                    Status = HttpStatusCode.BadGateway;
                }
            }
            catch (Exception ex)
            {
                Exception = ex;

                throw;
            }
        }

        protected virtual string GetResponse(WebResponse response)
        {
            string data;

            using (var stream = response.GetResponseStream())
            {
                data = new StreamReader(stream).ReadToEnd();
            }

            response.Close();

            return data;
        }

        public static implicit operator string(ApiResponse response)
        {
            return response.Response;
        }

        public virtual TData DeserializeAs<TData>() 
        {
            if(Codec == null)
            {
                throw new ArgumentException("Cannot deserialize to "+typeof(TData).Name, "Codec");
            }

            if(Response == null)
            {
                throw new ArgumentException("Cannot deserialize to " + typeof(TData).Name, "Response");
            }

            return Codec.Decode<TData>(Response);
        }
    }

    public class ErroneousResponse : ApiResponse
    {
        
    }
}