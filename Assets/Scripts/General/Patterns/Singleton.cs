using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Component
{
    protected static T instance;

    public static bool HaveIntstance => instance != null;
    public static T Instance {
        get {
            if (instance == null) {
                instance = FindFirstObjectByType<T>();
                if (instance == null) {
                    GameObject obj = new();
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake() => InitializeSingleton();

    protected virtual void InitializeSingleton() {
        if (!Application.isPlaying) return;

        if (instance != null)
            Destroy(gameObject);

        instance = this as T;
    }
}
