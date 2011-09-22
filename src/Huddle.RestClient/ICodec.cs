namespace Huddle.Clients
{
    public interface ICodec
    {
        string Encode<T>(T data);
        T Decode<T>(string data);
    }
}