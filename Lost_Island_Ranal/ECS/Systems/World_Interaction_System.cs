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
/// The World is the entry point to the ECS. It holds your entities and systems and you'll use it later to create and destroy entities.
/// </summary>


namespace Lost_Island_Ranal.ECS.Systems
{

    //TODO: refactor the properties into a seperate derrived class
    class World_Interaction_System : System
    {
        public World_Interaction_System() : base(Types.Body, Types.World_Interaction, Types.Physics)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var wi = (World_Interaction)entity.Get(Types.World_Interaction);
            if (wi.Constant_Update)
            {
                var o_interaction = (World_Interaction)entity.Get(Types.World_Interaction);
                if (o_interaction.UType == World_Interaction.Update_Type.LAMBDA)
                    o_interaction?.Update(entity, entity);
                else
                    o_interaction?.Lua_Update.Call(entity, entity);
            }
        }
    }
}
