using UnityEngine;

public class TechnicHolderRenderer : MonoBehaviour {
    [SerializeField] private GameObject _standardVisual;
    [SerializeField] private VisualChanger[] _stateVisuals;
    [SerializeField] private VisualChanger[] _brokennessStages;
    [SerializeField] private GameObject _brokenVisual;
    private TechnicHolderData _data;

    public void SetData(TechnicHolderData data) {
        _data = data;
    }

    public virtual void ChangeState(bool newState) {
        gameObject.SetActive(newState);
        foreach (var visual in _stateVisuals)
            visual.ChangeState(newState);
    }

    public void UpdateVisual() {
        float brokennessDegree = 1 - (_data.NowStrength / _data.Technic.Strength);
        float needBrokennessDegree = 1 / (_brokennessStages.Length + 1);
        for (int i = 1; i <= _brokennessStages.Length; i++) {
            bool isShow = needBrokennessDegree * i <= brokennessDegree;
            _brokennessStages[i - 1].ChangeState(isShow);
        }

        _standardVisual.SetActive(brokennessDegree != 1);
        _brokenVisual.SetActive(brokennessDegree == 1);
    }
}
