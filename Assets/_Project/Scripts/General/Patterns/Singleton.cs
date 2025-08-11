using System;
using UnityEngine;

public class Singleton<TObject> : MonoBehaviour
    where TObject : Component {

    protected static TObject _instance;
    public static TObject Instance => _instance;

    protected virtual void Awake() => InitializeSingleton();

#if !UNITY_WEBGL
    [RuntimeInitializeOnLoadMethod]
    private static void InitializeOnLoad() {
        _instance = null;
    }
#endif

    protected virtual void InitializeSingleton() {
        if (!Application.isPlaying) return;

        if (_instance != null) {
            Destroy(gameObject);
            return;
        }

        if (this is not TObject)
            throw new ArgumentNullException($"Singletone don't have a type {typeof(TObject)}");

        _instance = this as TObject;
    }
}
