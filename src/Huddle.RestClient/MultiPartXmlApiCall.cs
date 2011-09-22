using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace Huddle.Clients
{
    public class MultiPartXmlApiCall : XmlApiCall, IMultipartApiCall
    {
        private readonly string boundary = Guid.NewGuid().ToString().Replace("-", string.Empty);
        private readonly NameValueCollection formValues = new NameValueCollection();

        private bool cancel;
        private MultipartFile file;

        public MultiPartXmlApiCall() { }

        public MultiPartXmlApiCall(IApiCallObserver[] observers) : base(observers) { }

        private string _footer { get { return "--" + boundary + "--"; } }

        private string _valueHeader { get { return "--" + boundary + "\r\n"; } }

        public virtual event EventHandler<UploadProgressEventArgs> UploadProgressChanged = delegate { };

        public virtual void SetFile(string filePath, string mimeType, string fileName, string fieldName)
        {
            file = new FileInfoMultipartFile
            {
                File = new FileInfo(filePath),
                FileName = fileName,
                FileFieldName = fieldName,
                FileMimeType = mimeType
            };
        }

        public virtual void SetFile(Stream stream, string mimeType, string fileName, string fieldName)
        {
            file = new StreamMultiPartFile
            {
                Stream = stream,
                FileName = fileName,
                FileFieldName = fieldName,
                FileMimeType = mimeType
            };
        }

        public virtual void SetField(string name, object value)
        {
            formValues[name] = value.ToString();
        }

        protected override void InitializeRequest(HttpWebRequest request, string method)
        {
            base.InitializeRequest(request, method);

            using (var fileStream = file.GetStream())
            {
                var formHeader = GetFormHeader();
                var formFooter = GetFormFooter();
                var fileHeader = GetFileFormHeader();
                var fileFooter = GetFileFormFooter();

                var contentLength = formHeader.Length
                    + formFooter.Length
                        + fileHeader.Length
                            + fileFooter.Length
                                + fileStream.Length;
            
                request.ContentLength = contentLength;
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.AllowWriteStreamBuffering = false;
                
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(formHeader, 0, formHeader.Length);
                    requestStream.Write(fileHeader, 0, fileHeader.Length);
                    requestStream.Flush();
                
                    WriteFiles(requestStream, fileStream);

                    requestStream.Write(fileFooter, 0, fileFooter.Length);
                    requestStream.Write(formFooter, 0, formFooter.Length);
                
                    requestStream.Close();
                }
            }
        }

        public ApiResponse Post(string uri)
        {
            var request = GetRequest(uri, "POST");
            return Execute(request);
        }

        public ApiResponse Put(string uri)
        {
            var request = GetRequest(uri, "PUT");
            return Execute(request);
        }

        public ApiResponse<TResult> Put<TResult>(string uri) where TResult : class
        {
            var request = GetRequest(uri, "PUT");
            return Execute<TResult>(request);
        }

        public override ApiResponse<TResult> Post<TResult>(string uri)
        {
            var request = GetRequest(uri, "POST");
            return Execute<TResult>(request);
        }

        private byte[] GetFormHeader()
        {
            var sb = new StringBuilder();
            foreach (string key in formValues.Keys)
            {
                sb.Append(_valueHeader);
                sb.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", key, formValues[key]);
            }

            Log.DebugFormat("Posting form:");
            Log.DebugFormat(sb.ToString());

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private byte[] GetFormFooter()
        {
            return Encoding.UTF8.GetBytes(_footer);
        }

        private void WriteFiles(Stream stream, Stream fileStream)
        {
            const int chunkLength = 1024;
            var fileBuffer = new byte[chunkLength];
            var length = fileStream.Length;
            var amountWritten = 0L;

            int count;
            do
            {
                if (cancel)
                {
                    Log.DebugFormat("Command has been cancelled");
                    break;
                }

                count = fileStream.Read(fileBuffer, 0, fileBuffer.Length);
                stream.Write(fileBuffer, 0, count);
                stream.Flush();
                amountWritten += count;

                UpdateProgress(length, amountWritten);
            }
            while (count > 0);
        }

        private byte[] GetFileFormFooter()
        {
            return Encoding.UTF8.GetBytes("\r\n");
        }

        private byte[] GetFileFormHeader()
        {
            if(!string.IsNullOrEmpty(file.FileMimeType))
            {
                return Encoding.ASCII.GetBytes(
                string.Format(_valueHeader +
                    "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                        "Content-Type: {2};\r\n\r\n",
                              file.FileFieldName,
                              file.FileName,
                              file.FileMimeType));
            }

            return Encoding.ASCII.GetBytes(
                string.Format(_valueHeader +
                    "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n\r\n",
                              file.FileFieldName,
                              file.FileName));
        }

        private void UpdateProgress(long contentLength, long amountWritten)
        {
            UploadProgressChanged(this, new UploadProgressEventArgs((amountWritten/(double) contentLength)*100));
        }

        public virtual void Cancel()
        {
            cancel = true;
        }

        private class FileInfoMultipartFile : MultipartFile
        {
            public FileInfo File { get; set; }

            public override Stream GetStream()
            {
                if (false == File.Exists)
                {
                    throw new FileNotFoundException();
                }

                return File.Open(FileMode.Open, FileAccess.Read);
            }
        }

        private abstract class MultipartFile
        {
            public string FileName { get; set; }

            public string FileFieldName { get; set; }

            public string FileMimeType { get; set; }

            public abstract Stream GetStream();
        }

        private class StreamMultiPartFile : MultipartFile
        {
            public Stream Stream { get; set; }

            public override Stream GetStream()
            {
                return Stream;
            }
        }
    }
}