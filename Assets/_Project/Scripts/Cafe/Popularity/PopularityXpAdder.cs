using UnityEngine;

public class PopularityXpAdder : MonoBehaviour {
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private CriticSpawner _criticSpawner;
    [SerializeField] private RangeFloat _givingXp;
    [SerializeField, Min(0)] private float _savedTimeCoef;
    [SerializeField, Min(0)] private float _richClientMultiplier = 20f;

    public void AddXp(ClientType clientType, float waitCoef) {
        float savedTimeCoef = 1 - waitCoef;
        if (savedTimeCoef > _savedTimeCoef)
            savedTimeCoef = 1;

        if (clientType == ClientType.Rich) {
            _popularityManager.AddXp(_givingXp.RandomValue * _richClientMultiplier * savedTimeCoef);
        } else if (clientType == ClientType.Critic) {
            _criticSpawner.WaitSuccess();
        } else {
            _popularityManager.AddXp(_givingXp.RandomValue * savedTimeCoef);
        }
    }

    public void RemoveXp(ClientType clientType) {
        if (clientType == ClientType.Rich) {
            _popularityManager.RemoveXp(_givingXp.RandomValue * 20);
        } else if (clientType == ClientType.Critic) {
            _criticSpawner.WaitFailure();
        } else {
            _popularityManager.RemoveXp(_givingXp.RandomValue);
        }
    }
}
