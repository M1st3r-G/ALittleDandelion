using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace PostProcessing
{
    [Serializable, VolumeComponentMenuForRenderPipeline("Testing/OilPaintingEffect", typeof(UniversalRenderPipeline))]
    public class CustomPostProcessingOilPaintingEffect : VolumeComponent, IPostProcessComponent
    {
        //Parameters
        public FloatParameter intensity = new(1);
        public ColorParameter tintColor = new(Color.white);
        
        public bool IsActive() => true;
        public bool IsTileCompatible() => true;
    }
}
