using Microsoft.Xna.Framework;
using Penumbra;

//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------

/// <summary>
/// 
/// 
/// 
/// 
/// </summary>

namespace Lost_Island_Ranal.ECS
{
    class Light_Emitter : Component
    {
        public PointLight Light { get; set; }
        public Vector2 Offset { get; set; } = Vector2.Zero;

        public Light_Emitter(PenumbraComponent lighting, float scale = 300) : base(Types.Light_Emitter)
        {
            Light = new PointLight()
            {
                Scale = new Vector2(scale),
                Color = Color.AliceBlue,
                Intensity = 0.5f,
                Radius = 0.02f,
                ShadowType = ShadowType.Occluded
            };
            lighting.Lights.Add(Light);
        }
    }
}
