using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------


/// <summary>
/// ParticleSystem is an abstract class that provides the basic functionality to
/// create a particle effect. Different subclasses will have different effects,
/// such as fire, explosions, and plumes of smoke. To use these subclasses, 
/// simply call AddParticles, and pass in where the particles should exist
/// </summary>
namespace Lost_Island_Ranal.Graphics
{
    class Particle_World
    {
        List<Particle_Emitter> emitters;
        public Particle_World()
        {
            emitters = new List<Particle_Emitter>();
        }

        public void Add(Particle_Emitter emitter)
        {
            emitters.Add(emitter);
        }

        public void Update(GameTime time)
        {
            for (int i = emitters.Count - 1; i >= 0; i--)
            {
                var emitter = emitters[i];
                if (emitter.Remove)
                {
                    emitters.Remove(emitter);
                }else
                {
                    emitter.Update(time);
                }
            }
        }

        public void Destroy() //Destroy effect 
        {
            emitters.Clear();
        }

        public void Draw(SpriteBatch batch)
        {
            emitters.ForEach(e => { if (e.BlendState == BlendState.NonPremultiplied) e.Draw(batch); });
        }


        // typically, particles that use additive blending should be drawn on top of
        // particles that use regular alpha blending. ParticleSystems should therefore
        // set their DrawOrder to the appropriate value in InitializeConstants, though
        // it is possible to use other values for more advanced effects.


        public void Additive_Draw(SpriteBatch batch)
        {
            emitters.ForEach(e => { if ( e.BlendState == BlendState.Additive ) {
                   
                    e.Draw(batch);
                } });
        }
    }
}
