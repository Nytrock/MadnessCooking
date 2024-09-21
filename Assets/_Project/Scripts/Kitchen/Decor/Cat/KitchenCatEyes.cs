using UnityEngine;

public class KitchenCatEyes : MonoBehaviour {
    [SerializeField, Min(0)] private float _pettedScale;
    [SerializeField, Min(0)] private float _notPettedScale;
    private bool _isActive = true;

    private void Awake() {
        if (_pettedScale < _notPettedScale)
            (_pettedScale, _notPettedScale) = (_notPettedScale, _pettedScale);
    }

    public void UpdateScale(KitchenCatData data) {
        if (!_isActive)
            return;

        float scaleCoef = 1 - Mathf.InverseLerp(0, data.NeedTime, data.NowTime);
        float scale = Mathf.Lerp(_notPettedScale, _pettedScale, scaleCoef);
        transform.localScale = new(1, scale, 1);
    }

    public void ChangeState(bool newState) {
        _isActive = newState;
    }
}
