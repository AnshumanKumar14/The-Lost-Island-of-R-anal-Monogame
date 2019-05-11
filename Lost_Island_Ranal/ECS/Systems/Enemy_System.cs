using Lost_Island_Ranal.ECS.Components;
using NLua;
using System;
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
/// To create a new system, decide which base system to derive from and implement a new class.

///public class RenderSystem : EntityDrawSystem
///When you're creating entity systems the first thing you'll want to do is provide an Aspect to filter the system to only process the entities you're interested in.

///For example, a typical RenderSystem might want to process entities with a Sprite component and a Transform2 component.To provide an aspect you pass it into the base constructor.
/// 
/// 
/// http://docs.monogameextended.net/Features/Entities/
/// </summary>

namespace Lost_Island_Ranal.ECS.Systems
{
    class Enemy_System : System
    {
        public Enemy_System() : base(Types.Enemy)
        {
        }

        //protected override void Update(GameTime gameTime)
        //{
        //    _world.Update(gameTime);
        //    base.Update(gameTime);
        //}

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);

            //When you're inside an EntitySystem there are helper methods for creating destroying entities so that you don't need to access the World instance each time.

            var enemy = (Enemy) entity.Get(Types.Enemy);
            var body = (Body) entity.Get(Types.Body);

            var rnd = new Random();
            foreach(var item in enemy.Drop_Items )
            {
                int dx = -5 + rnd.Next() % 10;
                int dy = -5 + rnd.Next() % 10;

                var ent = World_Ref.Create_Entity(
                    Assets.It.Get<LuaTable>(item),
                    body.X + body.Width / 2 + dx,
                    body.Y + body.Height / 2 + dy
                    );

                var physics = (Physics)ent.Get(Types.Physics);
                if (physics != null )
                {
                    float angle = (float) rnd.Next() % 360;
                    
                    physics.Apply_Force(rnd.Next() % 150,-Physics.Deg_To_Rad(angle));
                }
            }
        }
    }
}
