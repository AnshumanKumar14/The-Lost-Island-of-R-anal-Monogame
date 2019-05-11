using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// 
    /// 
    /// 
    /// 
    /// 
    /// </summary>
namespace Lost_Island_Ranal.Graphics.Particle_Systems
{
    class Boss_1_Particle : Particle
    {
        public override void Update(GameTime time)
        {
            Countdown_Life(time);
            Apply_Velocity(time);
            this.Scale_Down(0.99f);
        }
    }
}
