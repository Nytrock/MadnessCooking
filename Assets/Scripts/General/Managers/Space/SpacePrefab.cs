using UnityEngine;

public class SpacePrefab : MonoBehaviour {
    [SerializeField, Min(0)] private float _size;

    public float Size => _size;
}
