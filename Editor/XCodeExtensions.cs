#if UNITY_EDITOR && (UNITY_IOS || UNITY_TVOS || UNITY_STANDALONE_OSX)
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.iOS.Xcode;

namespace HovelHouse.CloudKit
{
    public static class XCodeExtensions
    {
        public static void AddIfMissing(this PlistElementDict dict, string key, string value)
        {
            if (dict.values.ContainsKey(key) == false)
                dict.SetString(key, value);
        }

        public static void AddRangeIfMissing(this PlistElementArray elementArray, IEnumerable<string> values)
        {
            if (elementArray is null)
            {
                throw new ArgumentNullException(nameof(elementArray));
            }

            var here = elementArray.values.OfType<PlistElementString>().Select(e => e.value);
            var set = new HashSet<string>(here);

            foreach (var value in values)
            {
                if (set.Contains(value) == false)
                    elementArray.AddString(value);
            }
        }

        public static void AddIfMissing(this PlistElementArray elementArray, string value)
        {
            if (elementArray is null)
            {
                throw new ArgumentNullException(nameof(elementArray));
            }

            if (elementArray.values.OfType<PlistElementString>().Any(element => element.value == value) == false)
                elementArray.AddString(value);
        }

        public static bool HasStringWithValue(this PlistElementArray elementArray, string value)
        {
            return elementArray.values.OfType<PlistElementString>().Any(element => element.value == value);
        }
    }
}
#endif