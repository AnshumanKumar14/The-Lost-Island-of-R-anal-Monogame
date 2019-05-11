using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


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
    /// 
    /// 
    /// </summary>
namespace Lost_Island_Ranal.Graphics.Particle_Systems
{
    class Boss_1_Emitter : Particle_Emitter
    {
        public Boss_1_Emitter(Vector2 _position, bool spawn_right_off = false) : base(_position, spawn_right_off)
        {
            Spawn_Time_Max = 0.02f;
            this.BlendState = BlendState.Additive;
        }

        public override void Spawn()
        {
            var image = Assets.It.Get<Texture2D>("Boss_Texture");

            var rnd = new Random();

            Add(new Boss_1_Particle() {
                Position = this.Position,
                Image = image,
                Region = new Rectangle(104, 20, 24, 24),
                Life = 1f,
                Friction = Vector2.One,
                Velocity = new Vector2(0, 100),
                Color = new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble(), 1f)
            });
        }
    }
}
