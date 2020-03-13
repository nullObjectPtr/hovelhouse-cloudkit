using System;
using System.Runtime.InteropServices;

namespace HovelHouse.CloudKit
{
    /// <summary>
    /// A dictionary key used to lookup callback instances
    /// </summary>
    public struct InvocationRecord
    {
        public readonly IntPtr ptr;
        public readonly ulong id;

        static ulong next = 0;

        public InvocationRecord(HandleRef handle)
        {
            this.ptr = HandleRef.ToIntPtr(handle);
            id = next++;
        }

        public InvocationRecord(IntPtr ptr, ulong id)
        {
            this.ptr = ptr;
            this.id = id;
        }
    }
}