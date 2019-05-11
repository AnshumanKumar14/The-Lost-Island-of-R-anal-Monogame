using NLua;
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
/// 
/// 
/// </summary>

namespace Lost_Island_Ranal.ECS.Components
{
    class Lua_Function : Component
    {
        public LuaFunction Function { get; set; }
        public LuaTable Table { get; set; }

        public string File_Name { get; set; } = "";
        public bool Auto_Reload { get; set; } = false;

        //public DateTime Current_Date_Time { get; set; }
        public DateTime Last_Date_Time { get; set; } = DateTime.MaxValue;

        public Lua_Function(LuaFunction _function, string file_name = "") : base(Types.Lua_Function)
        {
            Function = _function;
            File_Name = file_name;

            if ( file_name != "" )
                Auto_Reload = true;
        }
    }
}
