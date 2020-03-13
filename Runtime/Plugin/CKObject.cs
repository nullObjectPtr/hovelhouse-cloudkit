using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CKObject : object
{
    internal HandleRef Handle { get; set; }

    internal CKObject() { }

    public CKObject(IntPtr ptr)
    {
        Handle = new HandleRef(this, ptr);
    }

    internal static object NewTypeOf(long typeId, IntPtr ptr)
    {
        switch(typeId)
        {
            case 0:
                return null;
            case 1:
                //ptr == IntPtr.Zero ? new NSNumber(ptr);
                throw new NotImplementedException();
            case 2:
                //return ptr == IntPtr.Zero ? null : new NSString(ptr);
                throw new NotImplementedException();
            case 3:
                //return ptr == IntPtr.Zero ? null : new NSDate(ptr);
                throw new NotImplementedException();
            case 4:
                //return ptr == IntPtr.Zero ? null : new NSData(ptr);
                throw new NotImplementedException();
            case 5:
                //return ptr == IntPtr.Zero ? null : new NSArray(ptr);
                throw new NotImplementedException();
            case 6:
                //return ptr == IntPtr.Zero ? null : new NSDictionary(ptr);
                throw new NotImplementedException();

        }

        throw new NotImplementedException();
    }
}
