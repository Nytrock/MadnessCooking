using UnityEngine;

public class TutotialClientSpawn : TutorialPart {
    [SerializeField] private ClientsSpawner _spawner;

    public void SpawnClient() {
        _spawner.Spawn();
    }
}
