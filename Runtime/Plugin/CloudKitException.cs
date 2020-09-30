using System;

namespace HovelHouse.CloudKit
{
    public class CloudKitException : Exception
    {
        public NSException NSException { get; private set; }
        public CloudKitException(NSException nativeException, string message) : base(message)
        {
            NSException = nativeException;
        }
    }
}