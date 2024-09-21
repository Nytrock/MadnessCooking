using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class CameraTiredAnimator : MonoBehaviour {
    [SerializeField] private RectTransform _panel;
    [SerializeField] private RangeFloat _panelPosition;
    [SerializeField] private RangeFloat _blurCoef;

    private DepthOfField _depth;

    private void Awake() {
        GetComponent<Volume>().profile.TryGet(out _depth);
    }

    private void Update() {
        float animationCoef = 1 - _panelPosition.InverseLerp(_panel.anchoredPosition.y);
        _depth.focalLength.value = _blurCoef.Lerp(animationCoef);
    }
}
