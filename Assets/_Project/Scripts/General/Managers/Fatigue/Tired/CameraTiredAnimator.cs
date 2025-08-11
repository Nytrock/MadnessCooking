using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(PostProcessVolume))]
public class CameraTiredAnimator : MonoBehaviour {
    [SerializeField] private RectTransform _panel;
    [SerializeField] private RangeFloat _panelPosition;
    [SerializeField] private RangeFloat _blurCoef;

    private DepthOfField _depth;

    private void Awake() {
        GetComponent<PostProcessVolume>().profile.TryGetSettings(out _depth);
    }

    private void Update() {
        float animationCoef = 1 - _panelPosition.InverseLerp(_panel.anchoredPosition.y);
        _depth.focalLength.value = _blurCoef.Lerp(animationCoef);
    }
}
