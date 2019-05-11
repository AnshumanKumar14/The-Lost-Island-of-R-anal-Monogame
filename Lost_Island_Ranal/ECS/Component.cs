using System;


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
/// A component is simply a class that holds some state about the entity. Typically, components are lightweight and don't contain any game logic.
/// It's common to have components with only a few properties or fields. Components can be more complex but inheritence is not encouraged
/// 
/// 
/// An Entity Component System (ECS) is a way to build and manage the entities (or game objects) in your game by composing their component parts together. An ECS consists of three main parts:
/// Components
/// Entities
/// Systems
/// A ComponentMapper provides a very fast way to access components within a system. When you're using a component mapper you're getting nearly direct access to the underlying arrays that hold the components under the hood.
/// 
/// 
/// source: http://docs.monogameextended.net/Features/Entities/
/// </summary>

namespace Lost_Island_Ranal.ECS
{
    abstract class Component {
        public enum Types //Create types for the Compenants all in classes ECS/Components
        {
            Player,
            Item,
            Physics,
            Sprite,
            Animation,
            Inventory,
            Body,
            AI,
            Npc,
            Equipment,
            Light_Emitter,
            Entity_Particle_Emitter,
            World_Interaction,
            Lua_Function,
            Health,
            Timed_Destroy,
            Enemy,
            Multipart_Animation,
            Advanced_Animation,
            Character,
            Spawner,
            Num_Of_Types,
        }

        public Types Type { get; protected set; }
        public Component(Types Type)
        {
            this.Type = Type;
        }
    }
}
