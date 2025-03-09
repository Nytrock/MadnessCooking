using UnityEngine;

public class ClientSpawnPoint : MonoBehaviour {
    [SerializeField] private CafeSpaceManager _spaceManager;

    public Vector2 Position => transform.position;

    private void Awake() {
        _spaceManager.SpaceAdded += Move;
    }

    private void Move() {
        transform.position += new Vector3(_spaceManager.SpaceSize, 0, 0);
    }
}
