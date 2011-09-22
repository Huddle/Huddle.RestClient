using Huddle.Clients.Converters;
using Huddle.Clients.DataPrinters;
using Huddle.Clients.Serializers;

namespace Huddle.Clients
{
    public class JsonApiCall : ApiCall
    {
        public JsonApiCall() : this(new ObserverResolver<IApiCallObserver>().ToArray()) { }

        public JsonApiCall(IApiCallObserver[] observers) : base(observers, new JsonDataPrinter())
        {
            SetAccept("application/vnd.huddle.data+json");
            SetContentType("application/vnd.huddle.data+json");
            Codec = new JsonCodec();
        }

        protected override string Serialize<TPostData>(TPostData data)
        {
            var json = Codec.Encode(data);
            Log.DebugFormat("Posting Json:\n{0}", JsonConverter.ToPrettyJson(json));
            return json;
        }
    }
}