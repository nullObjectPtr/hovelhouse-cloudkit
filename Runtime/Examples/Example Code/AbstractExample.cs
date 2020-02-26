using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractExample : MonoBehaviour
{
    private Queue<Action> actions = new Queue<Action>();    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        while (actions.Count > 0)
        {
            var act = actions.Dequeue();
            act();
        }
    }

    protected void EnqueueOnMainThread(Action act)
    {
        if (act == null)
            throw new ArgumentNullException(nameof(act));

        actions.Enqueue(act);
    }
}
