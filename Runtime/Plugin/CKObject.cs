using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CKObject : object
{
    internal HandleRef Handle { get; }

    public CKObject(IntPtr ptr)
    {
        Handle = new HandleRef(this, ptr);
    }
}
