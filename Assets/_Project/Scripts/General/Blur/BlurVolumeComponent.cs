using UnityEngine;
using UnityEngine.Rendering;

public class BlurVolumeComponent : VolumeComponent {
    [SerializeField] private ClampedFloatParameter _horizontalBlur = new(0.05f, 0, 0.5f);
    [SerializeField] private ClampedFloatParameter _verticalBlur = new(0.05f, 0, 0.5f);

    public ClampedFloatParameter HorizontalBlur => _horizontalBlur;
    public ClampedFloatParameter VerticalBlur => _verticalBlur;
}
