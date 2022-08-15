using System;
using System.Threading;
using UnityEngine;

public class MinimumSeconds : IDisposable
{
    private DateTime _EndTime;

    public MinimumSeconds(double seconds)
    {
        _EndTime = DateTime.Now.AddSeconds(seconds);
    }

    public void Dispose()
    {
        while (DateTime.Now < _EndTime)
        {
            Debug.Log("Still Sleeping");
            Thread.Sleep(100);
        }
    }
}