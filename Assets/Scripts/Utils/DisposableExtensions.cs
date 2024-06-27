using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DisposableExtensions 
{
    public static void DisposeAndClear(this IList<IDisposable> disposables)
    {
        foreach (var disposable in disposables)
        {
            disposable?.Dispose();
        }
        disposables.Clear();
    }
}
