using Lost_Island_Ranal.Graphics;
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
/// NPC this where its Calls from LUE file and game/player can interact with it
/// 
/// 
/// </summary>
/// 
namespace Lost_Island_Ranal.ECS
{
    class Npc : Component
    {
        public Dialogue Dialog;
        public Npc(LuaTable dialog_table) : base(Types.Npc)
        {
            this.Dialog = new Dialogue(dialog_table);
        }
    }
}
