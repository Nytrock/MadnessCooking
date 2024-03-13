using UnityEngine;

public class PopularityXpAdder : MonoBehaviour
{
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private CriticSpawner _criticSpawner;
    [SerializeField] private int _minXp;
    [SerializeField] private int _maxXp;

    public void AddXp(ClientType clientType)
    {
        if (clientType == ClientType.Rich) {
            _popularityManager.AddXp(Random.Range(_minXp, _maxXp) * 20);
        } else if (clientType == ClientType.Critic) {
            _criticSpawner.WaitSuccess();
        } else {
            _popularityManager.AddXp(Random.Range(_minXp, _maxXp));
        }
    }

    public void RemoveXp(ClientType clientType)
    {
        if (clientType == ClientType.Rich) {
            _popularityManager.RemoveXp(Random.Range(_minXp, _maxXp) * 20);
        } else if (clientType == ClientType.Critic) {
            _criticSpawner.WaitFailure();
        } else {
            _popularityManager.RemoveXp(Random.Range(_minXp, _maxXp));
        }
    }
}
