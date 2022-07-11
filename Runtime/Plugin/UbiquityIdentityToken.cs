using System;
using System.Runtime.InteropServices;

namespace HovelHouse.CloudKit
{
    public class UbiquityIdentityToken : object, IEquatable<UbiquityIdentityToken>, IDisposable
    {
        private IntPtr Ptr;
        
        #if UNITY_IPHONE || UNITY_TVOS
        const string dll = "__Internal";
        #else
        const string dll = "HHCloudKitMacOS";
        #endif
        
        [DllImport(dll)]
        private static extern bool UbiquityIdentityToken_isEqual(
            IntPtr ptr,
            IntPtr lhs);

        [DllImport(dll)]
        private static extern bool UbiquityIdentityToken_Dispose(
            IntPtr ptr);

        internal UbiquityIdentityToken(IntPtr ptr)
        {
            Ptr = ptr;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as UbiquityIdentityToken);
        }

        public bool Equals(UbiquityIdentityToken other)
        {
            return UbiquityIdentityToken_isEqual(Ptr, other.Ptr);
        }

        public override int GetHashCode()
        {
            return 793797649 + Ptr.GetHashCode();
        }

        public static bool operator ==(UbiquityIdentityToken lhs, UbiquityIdentityToken rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }

            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(UbiquityIdentityToken lhs, UbiquityIdentityToken rhs)
        {
            return !(lhs == rhs);
        }

        [DllImport(dll)]
        private static extern void NSUbiquitousKeyValueStore_Dispose(HandleRef handle);

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                //Debug.Log("NSUbiquitousKeyValueStore Dispose");
                UbiquityIdentityToken_Dispose(Ptr);
                disposedValue = true;
            }
        }

        ~UbiquityIdentityToken()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}