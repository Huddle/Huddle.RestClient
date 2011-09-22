using System;

namespace Huddle.Clients
{
    public interface IApiResponse<T> : IApiResponse where T : class
    {
        T Data { get; }
        Exception Exception { get; }
    }
}
