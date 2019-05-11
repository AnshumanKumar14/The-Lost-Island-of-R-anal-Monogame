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
///Emitters are managed by a ParticleSystem and each emitter maintains a pointer back to its ‘parent’ ParticleSystem as well as a linked list of particles emitted that are still active.
///Each emitter can be set to emit a certain amount of particles for a certain amount of frames and to do this for a number of iterations with a defined pause between each iteration.

///An emitter remains active until all the particles it has emitted have expired.

///Each emitter has it’s own onscreen location relative to which its particles are drawn – this enables emitters to track another sprite’s movement which is often very useful.
/// 
/// 
/// Source: http://rbwhitaker.wikidot.com/2d-particle-engine-1 
/// </summary>
namespace Lost_Island_Ranal.Graphics
{

    /// <summary>
    /// ParticleSystem is an abstract class that provides the basic functionality to
    /// create a particle effect. Different subclasses will have different effects,
    /// such as fire, explosions, and plumes of smoke. To use these subclasses, 
    /// simply call AddParticles, and pass in where the particles should exist
    /// </summary>
    /// 


    //Source and inspiration using the below code: https://blog.bitbull.com/2016/06/20/building-an-optimised-particle-system-in-monogame/
    ////////////////namespace com.bitbull.particles
    ////////////////{

    ////////////////    public class ParticleEmitterState
    ////////////////    {
    ////////////////        /*
    ////////////////         Sets up the state object with some sensible default values 
    ////////////////         */
    ////////////////        public ParticleEmitterState();

    ////////////////        /*
    ////////////////         Called once the first time MaxParticleFrames, ParticleFrames or TweenValues is requested.

    ////////////////         This method allocates a float[] the size of MaxParticleFrames and pre-calculates a tween value from 1.0
    ////////////////         to 0.0 for each frame. These tween values could be thought of as the 'energy' of the particle and are often
    ////////////////         used to calculate a particle's opacity, rotation or scale.

    ////////////////         An exception will be thrown if either ParticleDuration or ParticleDurationDeviation are set after this has
    ////////////////         been called making these the only properties that are more-or-less 'immutable' once a state has been set up.
    ////////////////         */
    ////////////////        private void Initialize();

    ////////////////        /*
    ////////////////        Returns a float[] the size of MaxParticleFrames containing a tween value from 1.0 to 0.0 for each frame. These 
    ////////////////        tween values could be thought of as the 'energy' of the particle and are often used to calculate a particle's
    ////////////////        opacity, rotation or scale.

    ////////////////        If a TweenAlgorithm has not been set this method returns null and tweening is ignored for this particle.

    ////////////////        Read only.
    ////////////////        */
    ////////////////        public float[] TweenValues;

    ////////////////        /*
    ////////////////        The maximum lifespan of a particle in frames. Read only.
    ////////////////        */
    ////////////////        public int MaxParticleFrames;

    ////////////////        /*
    ////////////////        The minimum lifespan of a particle in frames. Read only.
    ////////////////        */
    ////////////////        public int ParticleFrames;

    ////////////////        #region particleproperties

    ////////////////        /*
    ////////////////         Start velocity of particles
    ////////////////         */
    ////////////////        public float Velocity;

    ////////////////        /*
    ////////////////         Amount of random deviation from start velocity per particle.
    ////////////////         Particle velocity will run from Velocity-VelocityDeviation/2 to velocity+VelocityDeviation/2 
    ////////////////         */
    ////////////////        public float VelocityDeviation;

    ////////////////        /*
    ////////////////         Amount of spawn deviation from emitter centre along the x-axis.
    ////////////////         Particles will spawn from 0-SpawnDeviationX/2 to 0+SpawnDeviationX/2 
    ////////////////         */
    ////////////////        public float SpawnDeviationX;

    ////////////////        /*
    ////////////////         Amount of spawn deviation from emitter centre along the y-axis.
    ////////////////         Particles will spawn from 0-SpawnDeviationY/2 to 0+SpawnDeviationY/2 
    ////////////////         */
    ////////////////        public float SpawnDeviationX;

    ////////////////        /*
    ////////////////         Distance of particle from emitter centre on the x-axis before deviation is applied
    ////////////////         */
    ////////////////        public float SpawnRadiusX;

    ////////////////        /*
    ////////////////         Distance of particle from emitter centre on the y-axis before deviation is applied
    ////////////////         */
    ////////////////        public float SpawnRadiusY;

    ////////////////        /*
    ////////////////         Whether to reverse velocity (implode) on the x axis 
    ////////////////         */
    ////////////////        public bool ReverseVelocityX;

    ////////////////        /*
    ////////////////         Whether to reverse velocity (implode) on the y axis
    ////////////////         */
    ////////////////        public bool ReverseVelocityY;

    ////////////////        /*
    ////////////////         Start angle of particle spread.
    ////////////////         */
    ////////////////        public float StartAngle;

    ////////////////        /*
    ////////////////         End angle of particle spread.
    ////////////////         */
    ////////////////        public float StopAngle;

    ////////////////        /*
    ////////////////         Colour of particle
    ////////////////         */
    ////////////////        public Color Tint;

    ////////////////        /*
    ////////////////         Some kind of representation of the texture that is to be drawn for the particle - will 
    ////////////////         most likely be an encapsulation of an Image and a source rect for the area of the image
    ////////////////         that is to be drawn. 
    ////////////////         */
    ////////////////        public Drawable Texture;

    ////////////////        /*
    ////////////////         Overall velocity of particle is multiplied by this per frame. In most cases particles will
    ////////////////         deccelerate as they lose energy so this will be less than 1.0. 
    ////////////////         */
    ////////////////        public float Acceleration;

    ////////////////        /*
    ////////////////         Added to horizontal velocity of particle per frame - single unit vector component between 0 and 1

    ////////////////         In most particle systems this will be zero as gravity tends to pull straight down! 
    ////////////////         */
    ////////////////        public float GravityX;

    ////////////////        /*
    ////////////////         Added to vertical velocity of particle per frame - single unit vector component between 0 and 1

    ////////////////         In most particle systems this will be 1.0 as gravity tends to pull straight down!
    ////////////////         */
    ////////////////        public float GravityY;

    ////////////////        /*
    ////////////////         Amount of gravity added per frame, effectively the 'velocity' of the vector [GravityX,GravityY]
    ////////////////         */
    ////////////////        public float Gravity;

    ////////////////        /*
    ////////////////         Lifespan of each individual particle.
    ////////////////         Trying to set this property once Initialize() has been called will throw an exception.
    ////////////////         */
    ////////////////        public TimeSpan ParticleDuration;

    ////////////////        /*
    ////////////////        Random variance in lifespan of each individual particle.
    ////////////////        Particles will live from from 0-ParticleDuration/2 to 0+ParticleDuration/2 
    ////////////////        */
    ////////////////        public TimeSpan ParticleDurationDeviation;

    ////////////////        /*
    ////////////////        Some kind of representation of a tweening algorithm to be used for particle 'energy'. 
    ////////////////        */
    ////////////////        public Tween.TweenAlgorithm TweenAlgorithm;

    ////////////////        /*
    ////////////////        Base opacity of particle.
    ////////////////        */
    ////////////////        public float Opacity;

    ////////////////        /*
    ////////////////        Random variance in opacity of each individual particle.
    ////////////////        */
    ////////////////        public float OpacityDeviation;

    ////////////////        /*
    ////////////////        Change in particle's opacity based on the 'energy' of the particle.
    ////////////////        */
    ////////////////        public float OpacityTweenAmount;

    ////////////////        /*
    ////////////////        Base rotation of particle.
    ////////////////        */
    ////////////////        public float Rotation;

    ////////////////        /*
    ////////////////        Random variance in rotation of each individual particle.
    ////////////////        */
    ////////////////        public float RotationDeviation;

    ////////////////        /*
    ////////////////        Change in particle's rotation based on the 'energy' of the particle.
    ////////////////        */
    ////////////////        public float RotationTweenAmount;

    ////////////////        /*
    ////////////////        Base scale of particle.
    ////////////////        */
    ////////////////        public float Scale;

    ////////////////        /*
    ////////////////        Random variance in scale of each individual particle.
    ////////////////        */
    ////////////////        public float ScaleDeviation;

    ////////////////        /*
    ////////////////        Change in particle's scale based on the 'energy' of the particle.
    ////////////////        */
    ////////////////        public float ScaleTweenAmount;
    ////////////////    }

    ////////////////}


    abstract class Particle_Emitter
    {
        List<Particle> particles;


        // typically, particles that use additive blending should be drawn on top of
        // particles that use regular alpha blending. ParticleSystems should therefore
        // set their DrawOrder to the appropriate value in InitializeConstants, though
        // it is possible to use other values for more advanced effects.

        /// <summary>
        /// different effects can use different blend states. fire and explosions work
        /// well with additive blending, for example.
        /// </summary>
        public BlendState BlendState { get; set; } = BlendState.NonPremultiplied;


        // the origin when we're drawing textures. this will be the middle of the texture.
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

        public float Spawn_Time_Max { get; set; } = 0.5f;
        public float Spawn_Timer    { get; set; } = 0;

        public float Life_Max { get; set; } = -1;
        public float Life_Timer { get; set; } = 0;

        public bool Remove { get; set; } = false;
        public bool Active { get; set; } = true;

        public Particle_Emitter(Vector2 _position, bool spawn_right_off = false)
        {
            particles = new List<Particle>();
            Position = _position;
            if (spawn_right_off)
                Spawn();
        }

        public void Update(GameTime time)
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                var particle = particles[i];

                if (particle.Life <= 0)
                    particle.Destroy();

                if (particle.Remove) particles.Remove(particle);
                else
                    particle.Update(time);

                if (Life_Max > 0)
                {
                    Life_Timer += (float)time.ElapsedGameTime.TotalSeconds;
                    if (Life_Timer >= Spawn_Time_Max)
                        Remove = true;
                }
            }

            if ( Active )
            {
                Spawn_Timer += (float) time.ElapsedGameTime.TotalSeconds;
                if ( Spawn_Timer > Spawn_Time_Max )
                {
                    Spawn();
                    Spawn_Timer = 0;
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            particles.ForEach(p => p.Draw(batch));
        }

        public void Add(Particle p)
        {
            particles.Add(p);
        }

        public void Destroy()
        {
            particles.Clear();
            Remove = true;
        }

        public abstract void Spawn();
    }
}
