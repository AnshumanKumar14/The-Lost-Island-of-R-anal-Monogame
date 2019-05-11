﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLua;

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
    class World_Interaction : Component
    {
        public Func<Entity, Entity, bool> Update    { get; set; }
        public LuaFunction Lua_Update               { get; set; }

        public bool Constant_Update { get; set; } = false;
        public bool Is_Sensor { get; set; } = false;

        public enum Update_Type
        {
            LUA,
            LAMBDA
        }

        public Update_Type UType;

        public string ID { get; set; } = "";
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;

        public World_Interaction(Func<Entity, Entity, bool> update) : base(Types.World_Interaction)
        {
            Update      = update;
            UType = Update_Type.LAMBDA;
        }

        public World_Interaction(LuaFunction update): base(Types.World_Interaction)
        {
            Lua_Update = update;
            UType = Update_Type.LUA;
        }
    }
}
