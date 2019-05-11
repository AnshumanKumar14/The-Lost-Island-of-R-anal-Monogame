using System;
using System.Collections.Generic;
using System.Linq;
using static Lost_Island_Ranal.ECS.Component;
//-----------------------------------------------------------------------------
// Created by: Ayran Olckers AKA The Geekiest One
// -2019-
// -Game Development Project-
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.
//-----------------------------------------------------------------------------


/// <summary>
/// An Entity Component System (ECS) is a way to build and manage the entities (or game objects) in your game by composing their component parts together. 
/// An entity is a composition of components identified by an ID. Often you only need the ID of the entity to work with it. For performance reasons, 
/// and entity ID is only valid while the entity is alive. Once the entity is destroyed, it's ID may be recycled.
/// 
/// Source: http://docs.monogameextended.net/Features/Entities/
/// </summary>
namespace Lost_Island_Ranal.ECS
{
    class Entity
    {

        //private Dictionary<Component.Types, Component> components;
        private Dictionary<Component.Types, Component> components;
        private static int uuid_generater = 0;

        public bool Loaded { get; set; } = false;
        public bool Persistant { get; set; } = false;

        public List<string> Tags { get; set; }
        public Func<Entity, bool> Update { get; set; }

        public bool Has_Tag(string tag)
        {
            return Tags.Contains(tag);
        }

        public int UUID { get; private set; } = uuid_generater++;
        public bool Remove { get; private set; }

        public void Revive() { this.Remove = false; }

        public Entity()
        {
            components = new Dictionary<Types, Component>();
            Tags = new List<string>();
        }

        public void Destroy() {
            Remove = true;
        }

        public bool Has(Types name) {
            return components.ContainsKey(name);
        }

        public bool Has(string name){
            bool worked = Enum.TryParse(name, out Types component_type);
            if (worked)
            {
                return this.Has(component_type);
            }
            else
            {
                Console.WriteLine("Unknown component: " + name); //Error handling
            }
            return false;
        }

        public List<Component> Get_Components_List() {
            return components.Values.ToList();
        }

        public List<Types> Get_Component_Types_List() {
            return components.Keys.ToList();
        }

        public Component Add(Component component){
            if (components.ContainsKey(component.Type) == false){
                components.Add(component.Type, component);
            }
            return component;
        }

        public Component Get(Component.Types id)
        {
            if (components.ContainsKey(id))
                return (components[id]);
            return null;
        }

        public Component Get(string id)
        {
            bool worked = Enum.TryParse(id, out Types component_type);
            if (worked)
            {
                return this.Get(component_type);
            }
            else
            {
                Console.WriteLine("Unknown component: " + id); //Error handling
            }
            return null;
        }
    }
}
