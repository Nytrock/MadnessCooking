using UnityEngine;
using Random = UnityEngine.Random;

public class CriticSpawner : MonoBehaviour, IBindable<CafeData> {
    [SerializeField] private GameTimeManager _timeManager;
    [SerializeField] private PopularityManager _popularityManager;
    [SerializeField] private ClientsSpawner _clientSpawner;
    [SerializeField] private CriticUI _criticUI;

    private CriticSpawnerData _data;


    private void Awake() {
        _timeManager.DaytimeChanged += CheckDaytime;
    }

    private void Update() {
        _data.Update();
    }

    private void CheckDaytime(Daytime daytime) {
        if (daytime == Daytime.Night && _data.IsCriticCanSpawn)
            WaitFailure();

        if (daytime == Daytime.Morning)
            if (_popularityManager.CheckLevelWaitCritic())
                ActivateCriticWait();
    }

    private void ActivateCriticWait() {
        DaytimeStart morging = _timeManager.GetDaytimeStartInfo(Daytime.Morning);
        DaytimeStart night = _timeManager.GetDaytimeStartInfo(Daytime.Night);

        int minutes = Random.Range(morging.Hour * 60 + morging.Minute, night.Hour * 60 + night.Minute);
        _data.StartWait(minutes * 60);

        _criticUI.SetMessage(CriticMessageType.Start);
    }

    private void DisableCriticSpawn() {
        _data.ChangeCriticSpawn(false);
    }

    public void WaitSuccess() {
        DisableCriticSpawn();
        _popularityManager.NextLevel();
        _criticUI.SetMessage(CriticMessageType.Success);
    }

    public void WaitFailure() {
        DisableCriticSpawn();
        _popularityManager.CriticFailure();
        _criticUI.SetMessage(CriticMessageType.Failure);
    }

    public void LateStart() { }

    public void Bind(CafeData data) {
        data.CriticSpawner ??= new();
        _data = data.CriticSpawner;
    }
}
