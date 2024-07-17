using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class CameraTiredAnimator : MonoBehaviour {
    [SerializeField] private RectTransform _panel;
    [SerializeField] private float _minPanelPosition;
    [SerializeField] private float _maxPanelPosition;
    [SerializeField] private float _minTiredBlur;
    [SerializeField] private float _maxTiredBlur;

    private DepthOfField _depth;

    private void Awake() {
        GetComponent<Volume>().profile.TryGet(out _depth);
    }

    private void Update() {
        float animationCoef = 1 - Mathf.InverseLerp(_minPanelPosition, _maxPanelPosition, _panel.anchoredPosition.y);
        _depth.focalLength.value = Mathf.Lerp(_minTiredBlur, _maxTiredBlur, animationCoef);
    }
}
