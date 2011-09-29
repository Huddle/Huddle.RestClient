using System;
using System.Net;

namespace Huddle.Clients.Simulators
{
    public class SimulatedApiResponse : IApiResponse
    {
        public HttpStatusCode Status { get;  set; }
        public virtual ICodec Codec { get; set; }
        public string ContentType { get; set; }
        public string Response { get; set; }
        public bool Error { get; set; }
        public Exception Exception { get; set; }

        public TData DeserializeAs<TData>()
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
}