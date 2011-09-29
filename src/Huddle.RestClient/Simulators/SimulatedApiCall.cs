using System.Net;

namespace Huddle.Clients.Simulators
{
    public class SimulatedXmlApiCall : XmlApiCall
    {
        public IApiResponse Response { get; set; }

        protected override IApiResponse Execute(WebRequest request)
        {
            return Response;
        }
    }

    public class SimulatedJsonApiCall : JsonApiCall
    {
        public IApiResponse Response { get; set; }

        protected override IApiResponse Execute(WebRequest request)
        {
            return Response;
        }
    }
}