using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class Item_System : System
    {
        public Item_System() : base(Types.Item)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var item = (Item)entity.Get(Types.Item);

            if (item.Collect_Timer > 0)
            {
                item.Can_Collect = false;
                item.Collect_Timer -= (float)time.ElapsedGameTime.TotalSeconds;

                Physics physics = (Physics)entity.Get(Types.Physics);
                if (physics != null)
                {
                    physics.Handle_Collisions = false;
                }

            }
            else
            {
                Physics physics = (Physics)entity.Get(Types.Physics);
                if (physics != null)
                {
                    physics.Handle_Collisions = true;
                }
                item.Can_Collect = true;
            }
        }
    }
}
