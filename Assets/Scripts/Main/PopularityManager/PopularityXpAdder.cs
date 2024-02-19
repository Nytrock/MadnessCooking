using UnityEngine;

public class PopularityXpAdder : MonoBehaviour
{
    [SerializeField] PopularityManager _popularityManager;
    [SerializeField] int _minXp;
    [SerializeField] int _maxXp;

    public void AddXp(ClientType clientType)
    {
        if (clientType == ClientType.Rich) {
            _popularityManager.AddXp(Random.Range(_minXp, _maxXp) * 20);
        } else if (clientType == ClientType.Critic) {
            _popularityManager.InstantLevel();
        } else {
            _popularityManager.AddXp(Random.Range(_minXp, _maxXp));
        }
    }

    public void RemoveXp(ClientType clientType)
    {
        if (clientType == ClientType.Rich) {
            _popularityManager.RemoveXp(Random.Range(_minXp, _maxXp) * 20);
        } else if (clientType == ClientType.Critic) {
            _popularityManager.PreviousLevel();
        } else {
            _popularityManager.RemoveXp(Random.Range(_minXp, _maxXp));
        }
    }
}
