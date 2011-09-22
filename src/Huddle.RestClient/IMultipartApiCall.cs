using System;
using System.IO;

namespace Huddle.Clients
{
    public interface IMultipartApiCall : IApiCall
    {
        event EventHandler<UploadProgressEventArgs> UploadProgressChanged;
        void SetFile(string filePath, string mimeType, string fileName, string fieldName);
        void SetFile(Stream stream, string mimeType, string fileName, string fieldName);
        void SetField(string name, object value);
    }
}