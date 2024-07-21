using UnityEngine;

public class PopularityXpAdder : MonoBehaviour {
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private CriticSpawner _criticSpawner;
    [SerializeField, Min(0)] private int _minXp;
    [SerializeField, Min(0)] private int _maxXp;
    [SerializeField, Min(0)] private float _minWaitCoef;
    [SerializeField, Min(0)] private float _richClientMultiplier = 20f;

    public void AddXp(ClientType clientType, float waitCoef) {
        if (waitCoef > _minWaitCoef)
            waitCoef = 1;

        if (clientType == ClientType.Rich) {
            _popularityManager.AddXp(Random.Range(_minXp, _maxXp) * _richClientMultiplier * waitCoef);
        } else if (clientType == ClientType.Critic) {
            _criticSpawner.WaitSuccess();
        } else {
            _popularityManager.AddXp(Random.Range(_minXp, _maxXp) * waitCoef);
        }
    }

    public void RemoveXp(ClientType clientType) {
        if (clientType == ClientType.Rich) {
            _popularityManager.RemoveXp(Random.Range(_minXp, _maxXp) * 20);
        } else if (clientType == ClientType.Critic) {
            _criticSpawner.WaitFailure();
        } else {
            _popularityManager.RemoveXp(Random.Range(_minXp, _maxXp));
        }
    }
}
