namespace Huddle.Clients
{
    public interface IApiCallObserver 
    {
        void RegisterApiCall(IApiCall call);
    }
}