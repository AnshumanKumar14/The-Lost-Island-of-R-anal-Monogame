
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
    /// 
    /// 
    /// 
    /// 
    /// </summary>

namespace Lost_Island_Ranal.ECS.Systems
{
    class Health_System : System
    {
        public Health_System() : base(Types.Health)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var health = (Health)entity.Get(Types.Health);
            if (health.Shield_Timer >= 0)
            {
                health.Shield_Timer -= (float)time.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
