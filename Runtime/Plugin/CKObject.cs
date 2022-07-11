using System;
using System.Runtime.InteropServices;

public class CKObject : object
{
    #if UNITY_IPHONE || UNITY_TVOS
    const string dll = "__Internal";
    #else
    const string dll = "HHCloudKitMacOS";
    #endif
    
    [DllImport(dll)]
    
    private static extern long CKObject_GetHashCode(
        HandleRef ptr
    );
    
    internal HandleRef Handle { get; set; }

    internal CKObject() { }

    public CKObject(IntPtr ptr)
    {
        Handle = new HandleRef(this, ptr);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return Equals(obj as CKObject);
    }

    public bool Equals(CKObject rhs)
    {
        if (rhs == null)
            return false;

        return Handle.Handle == rhs.Handle.Handle;
    }

    public static bool operator ==(CKObject lhs, CKObject rhs)
    {
        // Check for null on left side.
        if (ReferenceEquals(lhs, null))
        {
            if (ReferenceEquals(rhs, null))
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

    public static bool operator !=(CKObject lhs, CKObject rhs)
    {
        return !(lhs == rhs);
    }

    public override int GetHashCode()
    {
        return (int) CKObject_GetHashCode(Handle);
    }

    public override string ToString()
    {
        return String.Format("{0} 0x{1}", this.GetType().Name, Handle.Handle.ToInt64());
    }
}
