using UnityEngine;

public class PopularityXpAdder : MonoBehaviour {
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private CriticSpawner _criticSpawner;
    [SerializeField] private RangeFloat _givingXp;
    [SerializeField, Min(0)] private float _richClientMultiplier = 5f;

    public void AddXp(ClientType clientType) {
        if (clientType == ClientType.Rich) {
            _popularityManager.AddXp(_givingXp.RandomValue * _richClientMultiplier);
        } else if (clientType == ClientType.Critic) {
            _criticSpawner.WaitSuccess();
        } else {
            _popularityManager.AddXp(_givingXp.RandomValue);
        }
    }

    public void RemoveXp(ClientType clientType) {
        if (clientType == ClientType.Rich) {
            _popularityManager.RemoveXp(_givingXp.RandomValue * _richClientMultiplier);
        } else if (clientType == ClientType.Critic) {
            _criticSpawner.WaitFailure();
        } else {
            _popularityManager.RemoveXp(_givingXp.RandomValue);
        }
    }
}
