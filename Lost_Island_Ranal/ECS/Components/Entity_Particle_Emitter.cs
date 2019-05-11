using Lost_Island_Ranal.Graphics;
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
/// </summary>

namespace Lost_Island_Ranal.ECS.Components
{
    class Entity_Particle_Emitter : Component
    {
        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Particle_Emitter Emitter { get; set; }

        public float Offset_X { get => Offset.X; }
        public float Offset_Y { get => Offset.Y; }

        public Particle_World World { get; set; }

        public bool Active { get; set; } = true;

        public Entity_Particle_Emitter(Particle_Emitter emitter, Particle_World world, bool active = true) : base(Types.Entity_Particle_Emitter)
        {
            Emitter = emitter;
            World = world;
            Active = active;

            world.Add(emitter);
        }
    }
}
