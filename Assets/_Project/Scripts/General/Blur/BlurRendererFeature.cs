using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlurRendererFeature : ScriptableRendererFeature {
    [SerializeField] private BlurSettings _settings;
    [SerializeField] private Shader _shader;

    private Material _material;
    private BlurRenderPass _blurRenderPass;

    public override void Create() {
        if (_shader == null)
            return;

        _material = new(_shader);
        _blurRenderPass = new(_material, _settings) {
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing
        };
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData) {
        if (_blurRenderPass == null)
            return;

        if (renderingData.cameraData.cameraType == CameraType.Game)
            renderer.EnqueuePass(_blurRenderPass);
    }

    protected override void Dispose(bool disposing) {
        if (Application.isPlaying)
            Destroy(_material);
        else
            DestroyImmediate(_material);
    }
}