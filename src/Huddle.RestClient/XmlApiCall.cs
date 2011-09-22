using Huddle.Clients.Converters;
using Huddle.Clients.DataPrinters;
using Huddle.Clients.Serializers;

namespace Huddle.Clients
{
    public class XmlApiCall : ApiCall
    {
        public XmlApiCall() : this(new ObserverResolver<IApiCallObserver>().ToArray()) { }

        public XmlApiCall(IApiCallObserver[] observers) : base(observers, new XmlDataPrinter())
        {
            Codec = new XmlCodec();
            SetAccept("application/vnd.huddle.data+xml");
            SetContentType("application/vnd.huddle.data+xml");
        }

        protected override string Serialize<TPostData>(TPostData data)
        {
            var xml = Codec.Encode(data);
            Log.DebugFormat("Posting XML:\n{0}", XmlConverter.ToPrettyXml(xml));
            return xml;
        }
    }
}