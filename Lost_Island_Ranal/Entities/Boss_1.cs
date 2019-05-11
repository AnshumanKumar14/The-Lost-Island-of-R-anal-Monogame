using Lost_Island_Ranal.ECS;
using NLua;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Lost_Island_Ranal.Graphics;
using Lost_Island_Ranal.ECS.Components;
using Lost_Island_Ranal.Graphics.Particle_Systems;


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
    /// Class to manage the animation of the boss, this utilises the AI file and the animation and particle class
    /// 
    /// 
    /// 
    /// local timer = 0

//require "Content/Lua/Utilities"

//local first = false

//local opening_events = 0
//local shoot_timer = 0

//local
//function Load_Ai(entity, engine)

//    local art = engine:Get_Component(entity, "Multipart_Animation");
//local body = engine:Get_Component(entity, "Body")

//	if (art == nil) then
//        print "cannot find the Multipart_Animation component!"
//	end

//    local eyes = art.Animation_Components["eyes"]
//    local head = art.Animation_Components["head"]
//    local mouth = art.Animation_Components["mouth"]

//    eyes.Playing = false
//	head.Playing = false

//    mouth.Playing = false


//    eyes.Current_Frame = 0

//    head.Current_Frame = 0

//    mouth.Current_Frame = 0


//    eyes.Animation_End = false

//    head.Animation_End = false

//    mouth.Animation_End = false


//    local mtimer = 0


//    opening_events = Eventing.new {
//		function(self, dt)
//			if engine:Entity_Within("Player", body.X, body.Y, 100) then
//                self:delayed_next(1);
//end
//end,


//        function(self, dt)
//			if self.done == false then
//                eyes.Playing = true
//				if eyes.Animation_End then

//                    eyes.Playing = false

//                    self:delayed_next(2);
//end
//end

//        end,

//		function(self, dt)

//            mouth.Playing = true
//			if mouth.Animation_End then

//                mouth.Playing = false
//				self:delayed_next(0.5);
//end
//end,


//        function(self, dt)

//            local physics = engine:Get_Component(entity, "Physics")

//            local emitter = engine:Get_Component(entity, "Entity_Particle_Emitter")

//            local health = engine:Get_Component(entity, "Health")

//            local body = engine:Get_Component(entity, "Body")

//			if emitter then

//                emitter.Active = true
//			end

//            mtimer = mtimer + dt

//            body.X = body.X + math.cos(mtimer) * 2
//			body.Y = body.Y + math.sin(mtimer*  4)

//			local player = engine:Get_With_Tag "Player"

//			if player then

//                local p_body = engine:Get_Component(player, "Body")


//                local dy = (p_body.Y - 40) - body.Y

//                local dx = (p_body.X) - body.X


//                body.X = body.X + dx* 0.008
//				body.Y = body.Y + dy* 0.01
//			end

//			if (health.Amount< 6) then

//                entity:Destroy()
//            end

//			-- self:next()

//        end,	
//	}
//end

//local
//function Ai(entity, engine)
//	if not first then
//        Load_Ai(entity, engine)

//        first = true
//	end


//    local dt = engine:Get_DT()

//    opening_events:update(dt)

//	--Animation_Components

//    local physics = engine:Get_Component(entity, "Physics")

//    local art = engine:Get_Component(entity, "Multipart_Animation");
//local body = engine:Get_Component(entity, "Body")

//    local health = engine:Get_Component(entity, "Health")


//    timer = timer + dt

//	if health.Should_Die then

//        entity:Destroy()
//		return
//	end

//	if physics.Other then
//		if physics.Other:Has_Tag "Player-Hit" then
//            local o_body = engine:Get_Component(physics.Other, "Body")

//			-- apply velocity

//            local side = (o_body.X > body.X) and -1 or 1

//			local power = 200

//            physics.Vel_X = (power) * side

//			-- local eyes = art.Animation_Components["eyes"]
//            -- local head = art.Animation_Components["head"]
//            -- local mouth = art.Animation_Components["mouth"]

//            -- if eyes and head and mouth then
//			-- 	eyes.Flash_Timer = 0.1
//			-- 	head.Flash_Timer = 0.1
//			-- 	mouth.Flash_Timer = 0.1
//			-- end

//            local camera = engine:Get_Camera()

//            camera:Shake(5, 0.1);

//health:Hurt(1)

//            physics.Other:Destroy()

//        end
//    end

//	-- body.X = body.X + math.cos(timer / 2)

//	-- body.Y = body.Y + math.sin(timer) / 2	
//end

//return Ai

/// 
/// </summary>
namespace Lost_Island_Ranal.Entities
{
    class Boss_1
    {
        public static readonly int BOSS_HEALTH = 15; //Health of Boss

        public static Entity Create(Lua lua, World world, Particle_World particle_world, Vector2 position)
        {

            /// Call animation and where the animated parts are
        
            var entity = world.Create_Entity();

            Texture2D texture = Assets.It.Get<Texture2D>("Boss_Texture"); //Call boss texxture
            if (texture == null )
            {
                Console.WriteLine("ERROR::BOSS::1 requires a texture with the id Boss_Texture!"); //Error handling if boss texture not found
            }

            entity.Add(new Body(position, new Vector2(24, 24)));
            entity.Add(new Physics(Vector2.Zero, Physics.PType.DYNAMIC));

            // main container for the head
            var graphics = new Multipart_Animation();

            // the base sprite
            var base_head = new Animated_Sprite(texture, "head"); //Head of the boss
            base_head.Animations.Add("head", new Animation(
                new List<Animation_Frame> {
                    new Animation_Frame(new Vector2(0, 0), new Vector2(47, 52))
                }
                , "head"));
            base_head.Offset = new Vector2(0, -17);
            base_head.Layer_Offset = 0.2f;

            var eyes = new Animated_Sprite(texture, "eye-close"); //Eye animation of being closed
            eyes.Animations.Add("eye-close", new Animation(
                new List<Animation_Frame> {
                    new Animation_Frame(new Vector2(95, 0), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 5), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 10), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 15), new Vector2(33, 5)),
                },
                "eye-close"));
            eyes.Layer_Offset = 0.25f;
            eyes.Offset = new Vector2(0, -32 + 5);

            var mouth = new Animated_Sprite(texture, "mouth-open"); //mouth part startes to open
            mouth.Animations.Add("mouth-open", new Animation(
                new List<Animation_Frame> {
                    new Animation_Frame(new Vector2(0, 52), new Vector2(47, 17))        { Offset = Vector2.Zero },
                    new Animation_Frame(new Vector2(0, 52+17), new Vector2(47, 17)),
                    new Animation_Frame(new Vector2(0, 52+17*2), new Vector2(47, 17)),
                    new Animation_Frame(new Vector2(0, 52+17*3), new Vector2(47, 17)),
                    new Animation_Frame(new Vector2(48, 0), new Vector2(47, 18))        { Offset = new Vector2(0, 1) },
                    new Animation_Frame(new Vector2(48, 18), new Vector2(47, 23))       { Offset = new Vector2(0, 6) },
                    new Animation_Frame(new Vector2(48, 41), new Vector2(47, 23))       { Offset = new Vector2(0, 6) },
                    new Animation_Frame(new Vector2(48, 64), new Vector2(47, 23))       { Offset = new Vector2(1, 6) },
                },
                "mouth-open"));
            mouth.Layer_Offset = 0.25f;
            mouth.Offset = new Vector2(0, 0);
                

            //call the parts of the body
            graphics.Animation_Components.Add("mouth", mouth);
            graphics.Animation_Components.Add("eyes", eyes);
            graphics.Animation_Components.Add("head", base_head);
            entity.Add(graphics);

            entity.Add(new Entity_Particle_Emitter(
                new Boss_1_Emitter(position, false),
                particle_world,
                false
                ) {
                Offset = new Vector2(0, 4)
            });

            entity.Add(new Health(BOSS_HEALTH));

            entity.Add(new Lua_Function(lua.DoFile("Content/Lua/Boss_1_Ai.lua")[0] as LuaFunction, "Content/Lua/Boss_1_Ai.lua")); //File location of LUA file of boss with 

            return entity;
        }
    }
}
