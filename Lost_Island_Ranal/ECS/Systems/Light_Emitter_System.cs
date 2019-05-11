
using Microsoft.Xna.Framework;

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
    /// 
    /// </summary>
namespace Lost_Island_Ranal.ECS.Systems
{
    class Light_Emitter_System : System
    {
        public Light_Emitter_System() : base(Component.Types.Body, Component.Types.Light_Emitter)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var body = (Body)(entity.Get(Component.Types.Body));
            var light = (Light_Emitter)(entity.Get(Component.Types.Light_Emitter));

            light.Light.Position = body.Center + light.Offset;
        }
    }
}
