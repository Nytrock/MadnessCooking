using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Component
{
    protected static T _instance;
    public static T Instance => _instance;

    protected virtual void Awake() => InitializeSingleton();

    protected virtual void InitializeSingleton() {
        if (!Application.isPlaying) return;

        if (_instance != null) {
            Destroy(gameObject);
            return;
        }

        if (this is not T)
            throw new ArgumentNullException($"Singletone don't have a type {typeof(T)}");

        _instance = this as T;
    }
}
