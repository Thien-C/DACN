using System;
using Hung.ObjectPoolSystem;
using UnityEngine;

namespace Hung.Interfaces
{
    public interface IObjectPoolItem
    {
        void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;

        void Release();
    }
}