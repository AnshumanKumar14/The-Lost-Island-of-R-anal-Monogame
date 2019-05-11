using NLua;
using System;
using System.Collections.Generic;



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
/// </summary>
namespace Lost_Island_Ranal.ECS.Components
{
    class Spawner : Component
    {
        public List<string> Entities { get; set; }
        private World world;

        public Spawner(List<string> _entities, World _world) : base(Types.Spawner)
        {
            Entities = _entities;
            world = _world;
        }
        
        public void Do_Spawn(float X, float Y) {
            var rnd = new Random();
            foreach (var item in Entities)
            {
                int dx = -5 + rnd.Next() % 10;
                int dy = -5 + rnd.Next() % 10;

                var ent = world.Create_Entity(Assets.It.Get<LuaTable>(item));

                if (ent.Has(Types.Body))
                {
                    var body = (Body)ent.Get(Types.Body);
                    body.X = X;
                    body.Y = Y;
                }

                var physics = (Physics)ent.Get(Types.Physics);
                if (physics != null)
                {
                    float angle = (float)rnd.Next() % 360;

                    physics.Apply_Force(rnd.Next() % 150, -Physics.Deg_To_Rad(angle));
                }
            }
        }
    }
}
