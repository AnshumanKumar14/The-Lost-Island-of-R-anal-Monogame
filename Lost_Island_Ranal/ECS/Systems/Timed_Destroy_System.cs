using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Lost_Island_Ranal.ECS.Component;
using Lost_Island_Ranal.ECS.Components;


//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------

    /// <summary>
    /// Removing entities from the world is easy.
    ///_world.DestroyEntity(entity);
    ///It should be noted that the actual entity creation and removal is deferred until the next update.This allows for some performance optimizations and batches events so that they can be handled more gracefully by systems.
    ///Note: When you're inside an EntitySystem there are helper methods for creating destroying entities so that you don't need to access the World instance each time.
    /// </summary>
    /// 

namespace Lost_Island_Ranal.ECS.Systems
{
    class Timed_Destroy_System : System
    {
        public Timed_Destroy_System() : base(Types.Timed_Destroy)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var timer = (Timed_Destroy) entity.Get(Types.Timed_Destroy);
            if ( timer.Time_Left <= 0 )
                entity.Destroy();
            else
                timer.Time_Left -= (float) time.ElapsedGameTime.TotalSeconds;
        }
    }
}
