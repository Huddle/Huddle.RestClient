using System;

namespace Huddle.Clients
{
    public class UploadProgressEventArgs : EventArgs
    {
        public double PercentComplete { get; set; }

        public UploadProgressEventArgs(double percentComplete)
        {
            PercentComplete = percentComplete;
        }
    }
}