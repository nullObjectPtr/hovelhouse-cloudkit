using UnityEngine;
using HovelHouse.CloudKit;
using System.Collections;
using System;

public class Example8_KeyValueStore : MonoBehaviour
{
    private NSUbiquitousKeyValueStore store;
    private readonly string[] strings = new string[] { "Aries", "Leo", "Cancer", "Pisces", "Scorpio" };

    // Start is called before the first frame update
    void Start()
    {
        Run();
    }

    private void Run()
    {
        // For this example to work, you'll need to run this from two clients
        // Both will make somewhat random changes to the key-value store, which
        // will trigger the notification
        // Notifications only trigger when the key-value store is changed externally

        Debug.Log("Example 8 - Key Value Store");
        Debug.Log("Open this example on another client to start receiving KVS changed Notifications");

        store = NSUbiquitousKeyValueStore.DefaultStore;
        store.AddDidChangeExternallyNotificationObserver(OnKeyStoreChanged);
        StartCoroutine(SetRandomValues(30f));
    }

    private void OnKeyStoreChanged(long arg1, string[] arg2)
    {
        Debug.Log(string.Format("Keystore has changed keys: ({0})", string.Join(",", arg2)));

        foreach(var str in arg2)
        {
            object val = null;

            if (str == "BoolKey")
                val = store.BoolForKey(str);
            else if (str == "DoubleKey")
                val = store.DoubleForKey(str);
            else if (str == "StringKey")
                val = store.StringForKey(str);

            Debug.Log(string.Format("{0}:{1}", str, val));
        }
    }

    private IEnumerator SetRandomValues(float timeDelay)
    {
        while(true)
        {
            Debug.Log("Setting new random values...");
            store.SetBool(UnityEngine.Random.Range(0, 1) > 0.5, "BoolKey");
            store.SetDouble(UnityEngine.Random.Range(0.0f, 1.0f), "DoubleKey");
            store.SetString(strings[UnityEngine.Random.Range(0, strings.Length)], "StringKey");
            store.Synchronize();

            Debug.Log("Waiting for " + timeDelay + "seconds...");
            yield return new WaitForSecondsRealtime(timeDelay);
            Debug.Log("done waiting");
        }
    }
}