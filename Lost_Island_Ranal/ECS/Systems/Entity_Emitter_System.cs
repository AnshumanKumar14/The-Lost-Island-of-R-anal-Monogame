
using Microsoft.Xna.Framework;
using Lost_Island_Ranal.ECS.Components;
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
/// Usually when you create an entity you'll want to attach some components to it immediately. This is not required though, as components can be added and removed anytime by systems.

///var entity = _world.CreateEntity();
///entity.Attach(new Transform2(position));
///entity.Attach(new Sprite(textureRegion));
///Any standard class can be used as a component but typically you'll want to keep your components lightweight and specific.

///Note: An entity can only have one instance of each component type.
/// 
/// http://docs.monogameextended.net/Features/Entities/
/// </summary>

namespace Lost_Island_Ranal.ECS.Systems
{
    class Entity_Emitter_System : System
    {
        public Entity_Emitter_System() : base(Types.Entity_Particle_Emitter, Types.Body)
        {
        }

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);

            //_world.DestroyEntity(entity);

            var emitter = (Entity_Particle_Emitter) entity.Get(Types.Entity_Particle_Emitter);
            emitter.Emitter.Destroy();

            //var entity = _world.CreateEntity();
            //entity.Attach(new Transform2(position));
            //entity.Attach(new Sprite(textureRegion));
        }

        //It should be noted that the actual entity creation and removal is deferred until the next update.This allows for some performance optimizations and batches events so that they can be handled more gracefully by systems.



        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var emitter = (Entity_Particle_Emitter)entity.Get(Types.Entity_Particle_Emitter);
            var body    = (Body)entity.Get(Types.Body);

            emitter.Emitter.Position = body.Position + emitter.Offset;

            if (emitter.Active)
                emitter.Emitter.Update(time);
        }
    }
}
