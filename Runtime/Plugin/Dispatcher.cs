using System;
using System.Collections.Generic;
using UnityEngine;

namespace HovelHouse.CloudKit
{
    public class Dispatcher : MonoBehaviour
    {
        private static Dispatcher _instance;
        private Queue<Action> actionQueue = new Queue<Action>();
        private Action[] currentActions;

        public static Dispatcher Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("CloudKitMainThreadDispatcher")
                    {
                        hideFlags = HideFlags.DontSave
                    };
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<Dispatcher>();
                }

                return _instance;
            }
        }

        public void EnqueueOnMainThread(Action action)
        {
            if (!Application.isPlaying)
                return;

            lock (actionQueue)
            {
                actionQueue.Enqueue(action);
            }
        }

        // Update is called once per frame
        private void Update()
        {
            if (!Application.isPlaying)
                return;

            // Failsafe if 
            // User somehow created another instance of this class...
            if (_instance != this)
            {
                DestroyImmediate(this.gameObject);
                return;
            }

            lock (actionQueue)
            {
                currentActions = actionQueue.ToArray();
                actionQueue.Clear();
            }

            // Q - what happens when action throws?
            // Is this component disabled?

            foreach (var action in currentActions)
            {
                // We have to catch all exceptions in these callbacks
                // if the user makes a mistake, it'll keep our dispatcher
                // from running after it
                //

                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    //if (CloudKit.ExceptionHandler != null)
                    //    CloudKit.ExeptionHandler(ex);
                    //else
                    Debug.LogError(ex);
                }
            }
        }
    }
}