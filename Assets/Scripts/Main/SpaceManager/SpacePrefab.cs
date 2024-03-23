using UnityEngine;

public class SpacePrefab : MonoBehaviour
{
    [SerializeField] private float _size;

    public float GetSpaceSize()
    {
        return _size;
    }
}
