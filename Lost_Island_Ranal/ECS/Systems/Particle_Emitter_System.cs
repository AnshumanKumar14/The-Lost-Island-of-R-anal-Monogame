
using Lost_Island_Ranal.ECS.Components;
using Microsoft.Xna.Framework;
using static Lost_Island_Ranal.ECS.Component;

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
    /// 
    /// </summary>

namespace Lost_Island_Ranal.ECS.Systems
{
    class Particle_Emitter_System : System
    {
        public Particle_Emitter_System() : base(Types.Body, Types.Entity_Particle_Emitter)
        {
        }

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);

            var emitter_component = (Entity_Particle_Emitter) entity.Get(Types.Entity_Particle_Emitter);
            emitter_component.Emitter.Destroy();    
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var emitter_component = (Entity_Particle_Emitter) entity.Get(Types.Entity_Particle_Emitter);
            var body = (Body) entity.Get(Types.Body);

            emitter_component.Emitter.Position  = body.Position + emitter_component.Offset;
            emitter_component.Emitter.Active    = emitter_component.Active;
        }
    }
}
