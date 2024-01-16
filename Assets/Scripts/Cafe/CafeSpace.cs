using UnityEngine;

public class CafeSpace : MonoBehaviour
{
    [SerializeField] private float _size;

    public float GetSpaceSize()
    {
        return _size;
    }
}
