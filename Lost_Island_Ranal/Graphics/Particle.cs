using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-----------------------------------------------------------------------------
// Particle.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------


/// <summary>
/// particles are the little bits that will make up an effect. each effect will
/// be comprised of many of these particles. They have basic physical properties,
/// such as position, velocity, acceleration, and rotation. They'll be drawn as
/// sprites, all layered on top of one another, and will be very pretty.
/// 
/// 
/// 
/// 
/// This class represents an individual particle. Typically you will end up with thousands of these being active at any one time so it’s very important that any code executed in the Update() and Draw() methods is as efficient as possible.
///Most particle properties are set by the emitter and therefore ‘baked in’ when a particle is emitted, however I maintain a pointer back from each individual particle to a ParticleState object for things like gravity and the array of tween values.
///To allow for the fact that there is deviation in the amount of frames each particle lives for I maintain a float value per particle for a ‘tween frame’ and the amount this ‘tween frame’ is incremented per frame (State.MaxParticleFrames/Particle.DurationFrames). 
///At each Draw() call the ‘tween frame’ float is casted to an int so that the appropriate value can be retrieved from State.TweenValues. 
///    I don’t like doing this cast every time but it’s the only method I can think of that ensures each particle can move smoothly from maximum to minimum pre-calculated tween values whatever its duration.
/// 
/// 
/// 
/// </summary>
/// Soiurce: https://github.com/CartBlanche/MonoGame-Samples/blob/master/ParticleSample/ParticleSystem.cs
/// 




namespace Lost_Island_Ranal.Graphics
{



    //Source and inspiration using the below code: https://blog.bitbull.com/2016/06/20/building-an-optimised-particle-system-in-monogame/





                    ////////////using System;

                    ////////////using com.bitbull.meat;
                    ////////////using com.maturus.multipacks.generic;
                    ////////////using com.maturus.genericarcade;

                    ////////////namespace com.bitbull.particles
                    ////////////{
                    ////////////    public class Particle
                    ////////////    {
                    ////////////        /*
                    ////////////        Creates a new particle
                    ////////////        */
                    ////////////        internal Particle();

                    ////////////        /*
                    ////////////        Sets this particle moving based on the supplied ParticleEmitterState
                    ////////////        */
                    ////////////        internal void Activate(ParticleEmitterState state);

                    ////////////        /*
                    ////////////        Draws the particle on the specified graphics object relative to the specified x and y values.

                    ////////////        This method should also perform any 'per frame' calculations that can be skipped if the particle
                    ////////////        is offscreen and therefore doesn't need to to be drawn, for example scaling an opacity tweening.  
                    ////////////        */
                    ////////////        public void draw(float x, float y, Graphics g);

                    ////////////        /*
                    ////////////        Updates the X and Y location of the particle based on its velocity and state gravity.

                    ////////////        Increments a frame counter and returns false if the particle has reached its allocated lifespan.

                    ////////////        Any 'per frame' calculations that need to be carried out whether or not the particle is 
                    ////////////        visible should also be carried out here, for example increasing or decreasing velocity based
                    ////////////        on acceleration. 
                    ////////////        */
                    ////////////        internal bool update();

                    ////////////        /*
                    ////////////        X Location of particle
                    ////////////        */
                    ////////////        public float X;

                    ////////////        /*
                    ////////////        Y Location of particle
                    ////////////        */
                    ////////////        public float Y;

                    ////////////        /*
                    ////////////        Horizontal velocity component of particle (single unit vector) 
                    ////////////        */
                    ////////////        public float VX;

                    ////////////        /*
                    ////////////        Vertical velocity component of particle (single unit vector) 
                    ////////////        */
                    ////////////        public float VY;

                    ////////////        /*
                    ////////////        Current velocity of particle (ie length of vector [VX,VY]) 
                    ////////////        */
                    ////////////        public float Velocity;

                    ////////////        /*
                    ////////////        Frames elapsed since particle was emitted
                    ////////////        */
                    ////////////        public int Frame;

                    ////////////        /*
                    ////////////        Particle lifespan in frames
                    ////////////        */
                    ////////////        public int DurationFrames;

                    ////////////        /*
                    ////////////        Initial scale of particle
                    ////////////        */
                    ////////////        public float Scale;

                    ////////////        /*
                    ////////////        Initial opacity of particle
                    ////////////        */
                    ////////////        public float Opacity;

                    ////////////        /*
                    ////////////        Initial rotation of particle
                    ////////////        */
                    ////////////        public float Rotation;
                    ////////////    }

                    ////////////}





    abstract class Particle
    {
        /// <summary>
        /// InitializeParticle randomizes some properties for a particle, then
        /// calls initialize on it. It can be overriden by subclasses if they 
        /// want to modify the way particles are created. For example, 
        /// SmokePlumeParticleSystem overrides this function make all particles
        /// accelerate to the right, simulating wind.
        /// </summary>
        /// <param name="p">the particle to initialize</param>
        /// <param name="where">the position on the screen that the particle should be
        /// </param>
        public float Life_Max   { get; set; } = 10f;
        public float Life       { get; set; } = 0f;

        public Color Color { get; set; } = Color.White;
        
        public bool     Remove { get; set; } = false;

        public SpriteEffects Flip { get; set; } = SpriteEffects.None;

        public Rectangle Region { get; set; }
        public Texture2D Image  { get; set; }

        public Vector2 Position { get; set; }
        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

        public Vector2 Size { get; set; }
        public float Width  { get { return Size.X; } set { Size = new Vector2(value, Size.Y); } }
        public float Height { get { return Size.Y; } set { Size = new Vector2(Size.X, value); } }

        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public float Vel_X { get { return Velocity.X; } set { Velocity = new Vector2(value, Velocity.Y); } }
        public float Vel_Y { get { return Velocity.Y; } set { Velocity = new Vector2(Velocity.X, value); } }

        public float Direction { get; set; } = 0f;
        public Vector2 Friction  { get; set; } = Vector2.One;

        public float Speed { get; set; } = 10f;
        public float Scale { get; set; } = 1f;
        public float Rotation { get; set; } = 0f;

        //Transparency effect on particles
        public float Transparency {
            get => this.Color.A / 255f;
            set => Color = new Color(Color.R, Color.G, Color.B, (byte) (255 * value));
        }

        public Particle()
        {
            Life = Life_Max;
        }

        public    void Destroy()                    => Remove = true;
        protected void Scale_Down(float by = 0.99f) => Scale *= by;
        protected void Apply_Friction()             => Velocity *= Friction;

        protected void Move_In_Direction() =>
            Velocity = new Vector2((float)Math.Cos(Direction), (float)Math.Sin(Direction)) * Speed;

        protected void Find_Direction() =>
            Direction = (float)Math.Atan2(Y - Vel_Y, X - Vel_X);

        protected void Apply_Velocity(GameTime time) =>
            Position += Velocity * (float)time.ElapsedGameTime.TotalSeconds;

        protected float Lerp(float a, float b, float time)  => (1f - time) * a + time * b;
        protected float Lerp2(float a, float b, float time) => a + (b - a) * time;

        public void Fade_Out()
        {
            if (Life < Life_Max / 2) return;
            var t = (1 / (Life / (Life_Max)) - 1);
            //Console.WriteLine(t);
            Transparency = Lerp(Transparency, 0, t);
        }

        public void Fade_In()
        {
            //if (Life > Life_Max / 2) return;
            Transparency = Lerp2(Transparency, 1, 0.16f);
        }
        // is this particle still alive? once TimeSinceStart becomes greater than
        // Lifetime, the particle should no longer be drawn or updated.
        protected void Countdown_Life(GameTime time)
        {
            Life -= (float)time.ElapsedGameTime.TotalSeconds;
        }

        public abstract void Update(GameTime time);


        /// <summary>
        /// overriden from DrawableGameComponent, Draw will use ParticleSampleGame's 
        /// sprite batch to render all of the active particles.
        /// </summary>
        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(
                Image, 
                Position + new Vector2(Region.Width/2, Region.Width / 2), 
                Region, 
                new Color (this.Color.R/255f, this.Color.G/255f, this.Color.B/255f, Transparency), 
                Rotation, 
                new Vector2(Region.Width / 2, Region.Height /2), Scale, 
                Flip, 
                0.4f
                );
        }
    }
}
