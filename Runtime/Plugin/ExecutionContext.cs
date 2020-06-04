//
//  ExecutionContext.cs
//
//  Created by Jonathan Culp <jonathanculp@gmail.com> on 05/14/2020
//  Copyright © 2020 HovelHouseApps. All rights reserved.
//  Unauthorized copying of this file, via any medium is strictly prohibited
//  Proprietary and confidential
//

using System;
using System.Threading;

namespace HovelHouse.CloudKit
{
    public class ExecutionContext
    {
        public readonly SynchronizationContext synchronizationContext;
        public Action Callback;

        public ExecutionContext(Action callback)
        {
            synchronizationContext = SynchronizationContext.Current;
            Callback = callback;
        }

        public void Invoke()
		{
            synchronizationContext.Post((_) => Callback(), null);
		}
    }

    public class ExecutionContext<T1>
    {
        public readonly SynchronizationContext synchronizationContext;
        public Action<T1> Callback;

        public ExecutionContext(Action<T1> callback)
        {
            synchronizationContext = SynchronizationContext.Current;
            Callback = callback;
        }

        public void Invoke(T1 arg1)
        {
            synchronizationContext.Post((_) => Callback(arg1), null);
        }
    }

    public class ExecutionContext<T1,T2>
    {
        public readonly SynchronizationContext synchronizationContext;
        public Action<T1,T2> Callback;

        public ExecutionContext(Action<T1,T2> callback)
        {
            synchronizationContext = SynchronizationContext.Current;
            Callback = callback;
        }

        public void Invoke(T1 arg1, T2 arg2)
        {
            synchronizationContext.Post((_) => Callback(arg1, arg2), null);
        }
    }

    public class ExecutionContext<T1, T2, T3>
    {
        public readonly SynchronizationContext synchronizationContext;
        public Action<T1, T2, T3> Callback;

        public ExecutionContext(Action<T1, T2, T3> callback)
        {
            synchronizationContext = SynchronizationContext.Current;
            Callback = callback;
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            synchronizationContext.Post((_) => Callback(arg1, arg2, arg3), null);
        }
    }

    public class ExecutionContext<T1, T2, T3, T4>
    {
        public readonly SynchronizationContext synchronizationContext;
        public Action<T1, T2, T3, T4> Callback;

        public ExecutionContext(Action<T1, T2, T3, T4> callback)
        {
            synchronizationContext = SynchronizationContext.Current;
            Callback = callback;
        }

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            synchronizationContext.Post((_) => Callback(arg1, arg2, arg3, arg4), null);
        }
    }
}
