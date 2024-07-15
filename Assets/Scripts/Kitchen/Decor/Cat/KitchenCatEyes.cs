using UnityEngine;

public class KitchenCatEyes : MonoBehaviour {
    [SerializeField, Min(0)] private float _pettedScale;
    [SerializeField, Min(0)] private float _notPettedScale;

    private void Awake() {
        if (_pettedScale < _notPettedScale)
            (_pettedScale, _notPettedScale) = (_notPettedScale, _pettedScale);
    }

    public void UpdateScale(KitchenCatData data) {
        float scaleCoef = 1 - Mathf.InverseLerp(0, data.NeedTime, data.NowTime);
        float scale = Mathf.Lerp(_notPettedScale, _pettedScale, scaleCoef);
        transform.localScale = new(1, scale, 1);
    }
}
