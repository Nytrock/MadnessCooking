using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

public class BlurRenderPass : ScriptableRenderPass {
    private static readonly int _horizontalBlurId = Shader.PropertyToID("_Sigma");
    private static readonly int _verticalBlurId = Shader.PropertyToID("_VerticalBlur");
    private const string _blurTextureName = "_BlurTexture";
    private const string _verticalPassName = "VerticalBlurRenderPass";
    private const string _horizontalPassName = "HorizontalBlurRenderPass";

    private readonly BlurSettings _defaultSettings;
    private readonly Material _material;
    private RenderTextureDescriptor _blurTextureDescriptor;

    public BlurRenderPass(Material material, BlurSettings defaultSettings) {
        _material = material;
        _defaultSettings = defaultSettings;
        _blurTextureDescriptor = new RenderTextureDescriptor(Screen.width, Screen.height, RenderTextureFormat.Default, 0);
    }

    private void UpdateBlurSettings() {
        if (_material == null) return;

        var volumeComponent = VolumeManager.instance.stack.GetComponent<BlurVolumeComponent>();
        float horizontalBlur = volumeComponent.HorizontalBlur.overrideState ?
            volumeComponent.HorizontalBlur.value : _defaultSettings.HorizontalBlur;
        float verticalBlur = volumeComponent.VerticalBlur.overrideState ?
            volumeComponent.VerticalBlur.value : _defaultSettings.VerticalBlur;

        _material.SetFloat(_horizontalBlurId, horizontalBlur);
        _material.SetFloat(_verticalBlurId, verticalBlur);
    }

    public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData) {
        UniversalResourceData resourceData = frameData.Get<UniversalResourceData>();
        UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();

        if (resourceData.isActiveTargetBackBuffer)
            return;

        _blurTextureDescriptor.width = cameraData.cameraTargetDescriptor.width;
        _blurTextureDescriptor.height = cameraData.cameraTargetDescriptor.height;
        _blurTextureDescriptor.depthBufferBits = 0;

        TextureHandle source = resourceData.activeColorTexture;
        TextureHandle destination = UniversalRenderer.CreateRenderGraphTexture(renderGraph,
            _blurTextureDescriptor, _blurTextureName, false);

        UpdateBlurSettings();

        if (!source.IsValid() || !destination.IsValid())
            return;

        RenderGraphUtils.BlitMaterialParameters param = new(source, destination, _material, 0);
        renderGraph.AddBlitPass(param, _verticalPassName);

        RenderGraphUtils.BlitMaterialParameters param1 = new(destination, source, _material, 1);
        renderGraph.AddBlitPass(param1, _horizontalPassName);
    }
}