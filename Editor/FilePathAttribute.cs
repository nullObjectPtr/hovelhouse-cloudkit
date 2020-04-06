using UnityEngine;

public class FilePathAttribute : PropertyAttribute 
{
    public readonly string Ext;

    public FilePathAttribute()
    {

    }

    public FilePathAttribute(string ext)
    {
        Ext = ext;
    }
}