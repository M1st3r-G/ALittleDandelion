using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PostProcessing
{
    public class OilPaintingRenderFeature: ScriptableRendererFeature
    {
        private OilPaintingPass _oilPaintingPass;
        
        public override void Create()
        {
            _oilPaintingPass = new OilPaintingPass();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(_oilPaintingPass);
        }


        private class OilPaintingPass : ScriptableRenderPass
        {
            private Material _mat;
            private int _oilPaintingId = Shader.PropertyToID("_Temp");
            private RenderTargetIdentifier _src, _painted;
            
            public OilPaintingPass()
            {
                if (!_mat)
                {
                    _mat = CoreUtils.CreateEngineMaterial("CustomPost/OilPainting");
                }
                
                renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            }

            public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
            {
                RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
                _src = renderingData.cameraData.renderer.cameraColorTargetHandle;
                cmd.GetTemporaryRT(_oilPaintingId, desc, FilterMode.Bilinear);
                _painted = new RenderTargetIdentifier(_oilPaintingId);
            }
            
            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                CommandBuffer cmdB = CommandBufferPool.Get("OilPaintingRenderFeature");
                VolumeStack volumes = VolumeManager.instance.stack;
                CustomPostProcessingOilPaintingEffect oilPaintingData =
                    volumes.GetComponent<CustomPostProcessingOilPaintingEffect>();

                if (oilPaintingData.IsActive())
                {
                    _mat.SetColor("_OverlayColor", (Color)oilPaintingData.tintColor);
                    _mat.SetFloat("_Intensity", (float)oilPaintingData.intensity);
                    
                    Blit(cmdB, _src, _painted, _mat, 0);
                    Blit(cmdB, _painted, _src);
                }
                
                context.ExecuteCommandBuffer(cmdB);
                CommandBufferPool.Release(cmdB);
            }
            
            public override void OnCameraCleanup(CommandBuffer cmd)
            {
                cmd.ReleaseTemporaryRT(_oilPaintingId);
            }
        }
    }
}
